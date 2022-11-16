using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PO_Importer
{
    // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
    public class Folder
    {
        public object id { get; set; }
        public string name { get; set; }
        public string permalink { get; set; }
        public Source source { get; set; }
        public List<Sheet> sheets { get; set; }
        public List<Folder> folders { get; set; }
        public List<Report> reports { get; set; }
        public List<Template> templates { get; set; }
    }

    public class Report
    {
        public object id { get; set; }
        public string name { get; set; }
        public string accessLevel { get; set; }
        public string permalink { get; set; }
        public Source source { get; set; }
        public DateTime createdAt { get; set; }
        public DateTime modifiedAt { get; set; }
    }

    public class SmartSheetHome
    {
        public List<Sheet> sheets { get; set; }
        public List<object> folders { get; set; }
        public List<Report> reports { get; set; }
        public List<Workspace> workspaces { get; set; }        

    }

    public class Sheet
    {
        public object id { get; set; }
        public string name { get; set; }
        public string accessLevel { get; set; }
        public string permalink { get; set; }
        public DateTime createdAt { get; set; }
        public DateTime modifiedAt { get; set; }
        public Source source { get; set; }
    }

    public class Sight
    {
        public object id { get; set; }
        public string name { get; set; }
        public string accessLevel { get; set; }
        public string permalink { get; set; }
        public DateTime createdAt { get; set; }
        public DateTime modifiedAt { get; set; }
    }

    public class Source
    {
        public object id { get; set; }
        public string type { get; set; }
    }

    public class Template
    {
        public object id { get; set; }
        public string name { get; set; }
        public string accessLevel { get; set; }
    }

    public class Workspace
    {
        public object id { get; set; }
        public string name { get; set; }
        public string accessLevel { get; set; }
        public string permalink { get; set; }
        public List<Sheet> sheets { get; set; }
        public List<Folder> folders { get; set; }
        public List<Report> reports { get; set; }
        public List<Template> templates { get; set; }
        public List<Sight> sights { get; set; }
    }



}
