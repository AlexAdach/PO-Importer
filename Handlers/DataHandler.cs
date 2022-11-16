using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PO_Importer
{
    public static class DataHandler
    {
        public static EquipmentSheet EquipmentSheet { get; set; }
        public static  PurchaseOrder PurchaseOrder{ get; set; }

        public static void MakeComparison()
        {
            for(var i = 0; i < PurchaseOrder.lineitem.Count(); i++) //goes down the PO 
            {
                var present = IsThisItemOnTheEquipmentSHeet(PurchaseOrder.lineitem[i].part);
                if (present >= 0)
                {

                    //Console.WriteLine($"{PurchaseOrder.lineitem[i].part} found on the Equipment list");
                    var areTheSame = AreTheQuantitiesTheSame(PurchaseOrder.lineitem[i], EquipmentSheet.lineitem[present]);
/*                    if(!areTheSame)
                    {
                        Console.WriteLine($"Y to Confirm change");
                        Console.WriteLine("Equipment List:");
                        WriteLineItemToConsole(EquipmentSheet.lineitem[present]);
                        Console.WriteLine("Purchase Order:");
                        WriteLineItemToConsole(PurchaseOrder.lineitem[i]);
                        var confirm = Console.ReadLine().ToUpper();
                        if(confirm == "Y")
                        {
                            UpdateEquipmentLineItem(PurchaseOrder.lineitem[i], present);
                            Console.WriteLine("Updated Line Item:");
                            WriteLineItemToConsole(EquipmentSheet.lineitem[present]);
                        }


                    }*/
                    if(!areTheSame)
                    {
                        UpdateEquipmentLineItem(PurchaseOrder.lineitem[i], present);
                        
                        WriteLineItemToConsole(EquipmentSheet.lineitem[present]);

                    }
                    else
                    {
                        Console.WriteLine($"{PurchaseOrder.lineitem[i].part} found on the Equipment list but is up to date");
                    }

                }
                else
                {
                    Console.WriteLine($"{PurchaseOrder.lineitem[i].part} NOT on the Equipment list");
                    PurchaseOrder.lineitem[i].isChanged = true;
                    EquipmentSheet.lineitem.Add(PurchaseOrder.lineitem[i]);

                }

            }


        }



        private static void UpdateEquipmentLineItem(Lineitem POitem, int equipIndex)
        {
            EquipmentSheet.lineitem[equipIndex].quantity = POitem.quantity;
            EquipmentSheet.lineitem[equipIndex].onorder = POitem.onorder;
            EquipmentSheet.lineitem[equipIndex].backorder = POitem.backorder;
            EquipmentSheet.lineitem[equipIndex].allocated = POitem.allocated;
            EquipmentSheet.lineitem[equipIndex].shipped = POitem.shipped;

            if(EquipmentSheet.lineitem[equipIndex].po != POitem.po && POitem.po != "")
            {
                EquipmentSheet.lineitem[equipIndex].po += POitem.po;
            }
            if (EquipmentSheet.lineitem[equipIndex].location != POitem.location && POitem.location != "")
            {
                EquipmentSheet.lineitem[equipIndex].location += POitem.location;
            }
            EquipmentSheet.lineitem[equipIndex].isChanged = true;


        }

        private static bool AreTheQuantitiesTheSame(Lineitem PO, Lineitem equip)
        {
            if(PO.quantity != equip.quantity)
            {
                return false;
            }

            else if(PO.onorder != equip.onorder)
            {
                return false;
            }
            else if(PO.backorder != equip.backorder)
            {
                return false;
            }
            else if(PO.allocated != equip.allocated)
            {
                return false;
            }
            else if(PO.shipped != equip.shipped)
            {
                return false;
            }
            else if(PO.location != equip.location)
            {
                return false;
            }
            else if (PO.po != equip.po)
            {
                return false;
            }
            else
            {
                return true;
            }


        }

        private static int IsThisItemOnTheEquipmentSHeet(string part)
        {
            for (var i = 0; i < EquipmentSheet.lineitem.Count(); i++) //goes down the PO and checks to see if it's on the PO
            {
                if(EquipmentSheet.lineitem[i].part == part)
                {
                    return i;
                }
            }
            return -1;
        }

        public static void ReviewAllChanges()
        {
            Console.WriteLine("Here are the confirmed changes.");
            for (var i = 0; i < EquipmentSheet.lineitem.Count(); i++)
            {
                if(EquipmentSheet.lineitem[i].isChanged)
                {
                    WriteLineItemToConsole(EquipmentSheet.lineitem[i]);
                }
            }
        }

        private static void WriteLineItemToConsole(Lineitem lineitem)
        { 
             Console.WriteLine($"Manufacturer:{lineitem.manufacturer}, " +
                        $"Part: {lineitem.part}, " +
                        $"Description: {lineitem.description}, " +
                        $"PurchaseOrder {lineitem.po}, " +
                        $"Quantity: {lineitem.quantity}, " +
                        $"On Order: {lineitem.onorder}, " +
                        $"Backorder: {lineitem.backorder}, " +
                        $"Allocated: {lineitem.allocated}, " +
                        $"Shipped: {lineitem.shipped}, " +
                        $"Locations: {lineitem.location}");        
        }

        //PurchaseOrder.lineitem related Methods
        public static void WritePOToConsole()
        {
            if (PurchaseOrder.lineitem != null)
            {
                for (var i = 0; i < PurchaseOrder.lineitem.Count; i++)
                {
                    Console.WriteLine($"LineItem {i} - " +
                        $"Manufacturer:{PurchaseOrder.lineitem[i].manufacturer}, " +
                        $"Part: {PurchaseOrder.lineitem[i].part}, " +
                        $"Description: {PurchaseOrder.lineitem[i].description}, " +
                        $"PurchaseOrder.lineitem: {PurchaseOrder.lineitem[i].po}, " +
                        $"Quantity: {PurchaseOrder.lineitem[i].quantity}, " +
                        $"On Order: {PurchaseOrder.lineitem[i].onorder}, " +
                        $"Backorder: {PurchaseOrder.lineitem[i].backorder}, " +
                        $"Allocated: {PurchaseOrder.lineitem[i].allocated}, " +
                        $"Locations: {PurchaseOrder.lineitem[i].location}");

                }
            }
        }
    }
}

