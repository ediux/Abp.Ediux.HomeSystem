using Volo.Abp.Application.Dtos;

namespace Ediux.HomeSystem.Models.jqDataTables
{
    public class jqDataTableRequest: IEntityDto
    {
        /// <summary>
        /// Draw counter which is used by data tables to ensure ajax requests are drawn in sequence
        /// </summary>
        public int draw { get; set; }

        /// <summary>
        /// Paging first record indicator, the start point of the current data
        /// </summary>
        public int? start { get; set; }
        /// <summary>
        /// Number of records the table is set to display for the current draw
        /// </summary>
        public int? length { get; set; }
        /// <summary>
        /// Columns to which ordering has been applied
        /// </summary>
        public jqDataTableOrder[] order { get; set; }

        /// <summary>
        /// List of columns and data sources
        /// </summary>
        public jqDataTableColumn[] columns { get; set; }
        /// <summary>
        /// Global search value, applied to all columns which are searchable: true
        /// </summary>
        public jqDataTableSearch search { get; set; }

        public jqDataTableRequest()
        {
            order = new jqDataTableOrder[] { };
            columns = new jqDataTableColumn[] { };
            search = new jqDataTableSearch();
        }
    }
}
