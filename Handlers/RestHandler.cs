using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PO_Importer
{
    public static class RestHandler
    {

/*        private static SmartSheetHome smartSheetHome;
        private static SheetRoot sheetRoot;*/
        public static SmartSheetHome SmartSheetHome { get; set; }
        public static SheetRoot SheetRoot { get; set; }
        public static SmartsheetRest smartsheetRest;
        public static int EquipmentWorkspaceID { get; set; }
        public static string EquipmentSheetID{ get; set; }
        private static bool verbose = false;


        public static void Init(bool verb) //Finds the equipment workspace and prompts for which sheet to use.
        {
            if(verb)
            {
                verbose = true;
            }

            bool loop = true;

            smartsheetRest = new SmartsheetRest();

            //pulls all sheets/workspaces etc you have access to
            SmartSheetHome = smartsheetRest.GetHome().Result;
            FindEquipmentWorkspace();

            string sheetSelection = "";
            while (loop)
            {
                ListWorkspaceSheets(RestHandler.EquipmentWorkspaceID);

                Console.WriteLine("Type the number corrisponding to the sheet you wish to update.");
                sheetSelection = Console.ReadLine();

                Console.WriteLine(SmartSheetHome.workspaces[EquipmentWorkspaceID].sheets[Convert.ToInt32(sheetSelection)].name); //display the name of the selected sheet.

                
                
                Console.WriteLine("Are you sure? Y/N");
                
                string user = Console.ReadLine().ToUpper();
                if(user == "Y")
                {
                    loop = false;
                }
                else
                {
                    Console.Clear();
                }

            }

            EquipmentSheetID = GetSheetIDFromWorkspace(EquipmentWorkspaceID, Convert.ToInt32(sheetSelection));
            SheetRoot = smartsheetRest.GetSheet(EquipmentSheetID).Result;

            if (verbose)
            {
                ListWholeSheetRaw();
            }

            ConvertToEquipmentSheet();

        }

        public static void CommitUpdate()
        {
            for (var i = 0; i < DataHandler.EquipmentSheet.lineitem.Count(); i++)
            {
                if (DataHandler.EquipmentSheet.lineitem[i].isChanged)
                {
                    RequestRow newRow = CreateRowUpdateObject(DataHandler.EquipmentSheet.lineitem[i]);
                    var update = smartsheetRest.UpdateRow(newRow, EquipmentSheetID);
                    update.Wait();

                }



            }
            

            

        }

        //SmartSheetHomeRelatedFunctions
        public static void ListAllWorkspaces()
        {
            foreach (var data in SmartSheetHome.workspaces)
            {
                Console.WriteLine(data.name);
                //Console.WriteLine(data.id);
            }
        }
        public static void FindEquipmentWorkspace()
        {
            EquipmentWorkspaceID = SmartSheetHome.workspaces.FindIndex(x => x.name == "* Boston Equipment Tracking *");
        }


        public static string GetSheetIDFromWorkspace(int work, int sheet)
        {
            
            return SmartSheetHome.workspaces[work].sheets[sheet].id.ToString();
        }

        public static void ListWorkspaceSheets(int index)
        {
            for (var i = 0; i < SmartSheetHome.workspaces[index].sheets.Count(); i++)
            {
                Console.WriteLine($"{i} - {SmartSheetHome.workspaces[index].sheets[i].name}");

            }
        }

        //SheetRoot Related Methods
        public static void ListRowContent(int index)
        {
            foreach (var data in SheetRoot.rows[index].cells)
            {
                Console.WriteLine(data.value);
            }

        }

        public static void ListWholeSheetRaw()
        {
            for (var i = 0; i < SheetRoot.rows.Count(); i++)
            {
                string tempstring = "";
                foreach (var data in SheetRoot.rows[i].cells)
                {

                    if (data.value != null)
                    {
                        tempstring = tempstring + " " + data.value.ToString();
                        Console.WriteLine(data.value.GetType());
                    }
                }
                Console.WriteLine($"Line {i + 1} - " + tempstring);
            }
        }

        public static void ConvertToEquipmentSheet()
        {
            EquipmentSheet newSheet = new EquipmentSheet();

            for (var i = 0; i < SheetRoot.rows.Count(); i++) //goes doesn the rows
            {
                Lineitem lineitem = new Lineitem();

                if (SheetRoot.rows[i].cells[0].value != null)
                {
                    lineitem.manufacturer = SheetRoot.rows[i].cells[0].value.ToString();
                }

                if (SheetRoot.rows[i].cells[1].value != null)
                {
                    lineitem.part = SheetRoot.rows[i].cells[1].value.ToString();
                }

                if (SheetRoot.rows[i].cells[2].value != null)
                {
                    lineitem.description = SheetRoot.rows[i].cells[2].value.ToString();
                }

                if (SheetRoot.rows[i].cells[3].value != null)
                { 
                    lineitem.po = SheetRoot.rows[i].cells[3].value.ToString();
                }


                lineitem.quantity = Convert.ToInt32(SheetRoot.rows[i].cells[4].value);
                lineitem.onorder = Convert.ToInt32(SheetRoot.rows[i].cells[5].value);
                lineitem.backorder = Convert.ToInt32(SheetRoot.rows[i].cells[6].value);
                lineitem.allocated = Convert.ToInt32(SheetRoot.rows[i].cells[7].value);
                lineitem.shipped = Convert.ToInt32(SheetRoot.rows[i].cells[8].value);
                if(SheetRoot.rows[i].cells[10].value != null)
                {
                    lineitem.location = SheetRoot.rows[i].cells[10].value.ToString();
                }

                newSheet.lineitem.Add(lineitem);
                
               
                
            }
            DataHandler.EquipmentSheet = newSheet;

            

            Console.WriteLine("Converstion Complete.");
        }

        public static RequestRow CreateRowUpdateObject(Lineitem lineitem)
        {
            RequestRow newRow = new RequestRow();
            List<RequestCell> newCells = new List<RequestCell>();

            newRow.id = FindRowID(lineitem.part);


            newCells.Add(CreateNewCell(lineitem.manufacturer, 0));
            newCells.Add(CreateNewCell(lineitem.part, 1));
            newCells.Add(CreateNewCell(lineitem.description, 2));
            newCells.Add(CreateNewCell(lineitem.po, 3));
            newCells.Add(CreateNewCell(lineitem.quantity, 4));
            newCells.Add(CreateNewCell(lineitem.onorder, 5));
            newCells.Add(CreateNewCell(lineitem.backorder, 6));
            newCells.Add(CreateNewCell(lineitem.allocated, 7));
            newCells.Add(CreateNewCell(lineitem.shipped, 8));
            newCells.Add(CreateNewCell(lineitem.location, 10));

            newRow.cells = newCells;


            return newRow;


        }

        private static RequestCell CreateNewCell(object value, int columnNumber)
        {
            RequestCell newCell = new RequestCell();

            newCell.columnId = SheetRoot.columns[columnNumber].id.ToString();
            if (value != null)
            { 
                newCell.value = value.ToString();
            }
            else
            {
                newCell.value = "";
            }
            return newCell;
        }

        public static string FindRowID(string part)
        {
            string _id = null;
            foreach(var row in SheetRoot.rows)
            {

                if(row.cells[1].value.ToString() == part)
                {
                    _id = row.id.ToString();
                }

            }
            return _id;


        }
    }
}
