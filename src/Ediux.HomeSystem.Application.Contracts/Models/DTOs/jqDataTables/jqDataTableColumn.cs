namespace Ediux.HomeSystem.Models.jqDataTables
{

    public class jqDataTableColumn
    {
        /// <summary>
        /// Columns datasource
        /// </summary>
        public string data { get; set; }
        /// <summary>
        /// Columns name
        /// </summary>
        public string name { get; set; }

        public string title { get; set; }
        /// <summary>
        /// Can be searchable
        /// </summary>
        public bool searchable { get; set; }
        /// <summary>
        /// Can be sorting
        /// </summary>
        public bool orderable { get; set; }

        public bool visible { get; set; }
        /// <summary>
        /// Search value which applies to this column
        /// </summary>
        //[BindProperty(Name = "search", SupportsGet = true)]
        public jqDataTableSearch search { get; set; }

        public jqDataTableColumn()
        {
            search = new jqDataTableSearch();
        }
    }
}
