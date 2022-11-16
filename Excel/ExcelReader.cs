using ExcelDataReader;

using System;
using System.Collections.Generic;
using System.IO;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;

namespace PO_Importer
{
    class ExcelReader
    {
        private string _path;

        private static PurchaseOrder PO = new PurchaseOrder();
        
        
        
        public ExcelReader(string Path)
        {
            _path = Path;


        }

        public void ReadExcelFile()
        {
            
            var stream = File.Open(_path, FileMode.Open, FileAccess.Read);
            {
                // Auto-detect format, supports:
                //  - Binary Excel files (2.0-2003 format; *.xls)
                //  - OpenXml Excel files (2007 format; *.xlsx, *.xlsb)
                var reader = ExcelReaderFactory.CreateReader(stream);
                        var conf = new ExcelDataSetConfiguration
                        {
                            ConfigureDataTable = _ => new ExcelDataTableConfiguration
                            {
                                UseHeaderRow = true
                            }
                        };

                var dataSet = reader.AsDataSet(conf);

                var dataTable = dataSet.Tables[0];
                for(var i = 0; i < dataTable.Rows.Count; i++) //Goes Row by Row
                {
     
                    if(dataTable.Rows[i][0].ToString() == "P") //This means it's a new line item.
                    {
                        var newline = CreateNewLineItem(dataTable.Rows[i]);
                        PO.lineitem.Add(newline);
                    }
                    else if(dataTable.Rows[i][0].ToString() != "") //this means there are locations in this row
                    {
                        for (var x = 0; x < dataTable.Columns.Count; x++) //go through the row and check for locations.
                        {
                            if(dataTable.Rows[i][x].ToString() != "" && dataTable.Rows[i][x].ToString() != "A")
                            {
                                var tempindex = PO.lineitem.Count() - 1;
                                PO.lineitem[tempindex].location = PO.lineitem[tempindex].location + dataTable.Rows[i][x].ToString();
                                //Console.WriteLine(dataTable.Rows[i][x].ToString());

                            }
                        }
                    }                                        
                }
                reader.Dispose();
                stream.Dispose();
                DataHandler.PurchaseOrder = PO;

                
            }
        }

        private static Lineitem CreateNewLineItem(DataRow row)
        {
            Lineitem lineitem = new Lineitem();

            lineitem.manufacturer = row[1].ToString();
            lineitem.part = row[2].ToString();
            lineitem.description = row[3].ToString();
            lineitem.po = row[4].ToString();
            if (row[5] != DBNull.Value)
            {
                lineitem.quantity = row.Field<double>(5);
            }
            if (row[6] != DBNull.Value)
            {
                lineitem.onorder = row.Field<double>(6);
            }
            if (row[7] != DBNull.Value)
            {
                lineitem.backorder = row.Field<double>(7);
            }
            if (row[8] != DBNull.Value)
            {
                lineitem.allocated = row.Field<double>(8);
            }
            if (row[9] != DBNull.Value)
            {
                lineitem.shipped = row.Field<double>(9);
            }

            /*            lineitem.quantity = Convert.ToInt32(row[5]);
                        lineitem.onorder = Convert.ToInt32(row[6]);
                        lineitem.backorder = Convert.ToInt32(row[7]);
                        lineitem.allocated = Convert.ToInt32(row[8]);*/

            return lineitem;
        }





    }
}
