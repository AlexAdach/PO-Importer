using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PO_Importer
{
    public class Datum
    {
        public object id { get; set; }
        public string name { get; set; }
        public string accessLevel { get; set; }
        public string permalink { get; set; }
        public DateTime createdAt { get; set; }
        public DateTime modifiedAt { get; set; }
    }

    public class SheetsGlobal
    {
        public int pageNumber { get; set; }
        public int pageSize { get; set; }
        public int totalPages { get; set; }
        public int totalCount { get; set; }
        public List<Datum> data { get; set; }
    }

}
