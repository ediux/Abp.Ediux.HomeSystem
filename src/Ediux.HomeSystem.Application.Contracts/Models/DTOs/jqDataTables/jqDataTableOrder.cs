using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ediux.HomeSystem.Models.jqDataTables
{
    public class jqDataTableOrder
    {
        /// <summary>
        /// Column index to order
        /// </summary>
        public int column { get; set; }
        /// <summary>
        /// Ordering direction
        /// </summary>
        public string dir { get; set; }

        public string columnName { get; set; }
    }
}
