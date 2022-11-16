using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PO_Importer
{
    // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
    public class Cell
    {
        public object columnId { get; set; }
        public object value { get; set; }
        public string displayValue { get; set; }
    }

    public class Column
    {
        public object id { get; set; }
        public int version { get; set; }
        public int index { get; set; }
        public string title { get; set; }
        public string type { get; set; }
        public bool primary { get; set; }
        public bool validation { get; set; }
        public int width { get; set; }
        public List<string> options { get; set; }
    }

    public class SheetRoot
    {
        public long id { get; set; }
        public string name { get; set; }
        public int version { get; set; }
        public int totalRowCount { get; set; }
        public string accessLevel { get; set; }
        public List<string> effectiveAttachmentOptions { get; set; }
        public bool ganttEnabled { get; set; }
        public bool dependenciesEnabled { get; set; }
        public bool resourceManagementEnabled { get; set; }
        public string resourceManagementType { get; set; }
        public bool cellImageUploadEnabled { get; set; }
        public UserSettings userSettings { get; set; }
        public UserPermissions userPermissions { get; set; }
        public SheetWorkspace workspace { get; set; }
        public bool hasSummaryFields { get; set; }
        public string permalink { get; set; }
        public DateTime createdAt { get; set; }
        public DateTime modifiedAt { get; set; }
        public bool isMultiPicklistEnabled { get; set; }
        public List<Column> columns { get; set; }
        public List<Row> rows { get; set; }

       
    }

    public class Row
    {
        public object id { get; set; }
        public int rowNumber { get; set; }
        public bool expanded { get; set; }
        public DateTime createdAt { get; set; }
        public DateTime modifiedAt { get; set; }
        public List<Cell> cells { get; set; }
        public long? siblingId { get; set; }
    }

    public class UserPermissions
    {
        public string summaryPermissions { get; set; }
    }

    public class UserSettings
    {
        public bool criticalPathEnabled { get; set; }
        public bool displaySummaryTasks { get; set; }
    }

    public class SheetWorkspace
    {
        public long id { get; set; }
        public string name { get; set; }
    }

    public class RequestRow
    {
        [JsonProperty("id")]
        public string id { get; set; }
        [JsonProperty("cells")]
        public List<RequestCell> cells { get; set; }


    }

    public class RequestCell
    {
        [JsonProperty("columnId")]
        public string columnId { get; set; }
        [JsonProperty("value")]
        public string value { get; set; }
        
    }

}
