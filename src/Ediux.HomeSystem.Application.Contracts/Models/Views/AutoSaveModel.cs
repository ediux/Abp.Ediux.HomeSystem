using System;
using System.Collections.Generic;
using System.Text;

namespace Ediux.HomeSystem.Models.Views
{
    public class AutoSaveModel
    {
        public string entityType { get; set; }

        public string id { get; set; }

        public string title { get; set; }

        public string slug { get; set; }

        public string script { get; set;}

        public string style { get; set; }

        public string data { get; set; }

        public string shortDescription { get; set; }

        public string coverImageMediaId { get; set; }

        public string refid { get; set; }
    }
}
