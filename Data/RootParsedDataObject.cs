using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PO_Importer
{
    public class Lineitem
    {
        public string manufacturer { get; set; }
        public string part { get; set; }
        public string description { get; set; }
        public string po { get; set; }
        public double quantity { get; set; }
        public double onorder { get; set; }
        public double backorder { get; set; }
        public double allocated { get; set; }
        public double shipped { get; set; }
        public decimal itemcost { get; set; }
        public decimal actualcost { get; set; }
        public string location { get; set; }
        public bool isChanged { get; set; }



    }

    public class RootParsedDataObject
    {
        public List<Lineitem> lineitem { get; set; }
    }

    public class PurchaseOrder : RootParsedDataObject
    {
        public PurchaseOrder()
        {
            lineitem = new List<Lineitem>();
        }

    }

    public class EquipmentSheet : RootParsedDataObject
    {
        public EquipmentSheet()
        {
            lineitem = new List<Lineitem>();
        }
    }
}
