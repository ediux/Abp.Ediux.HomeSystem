using System;
using System.Collections.Generic;
using System.Text;

namespace Ediux.HomeSystem
{
    /// <summary>
    /// 定義家用入口系統的JavaScript和CSS的全域名稱
    /// </summary>
    public class HomeSystemBundles
    {
        public const string Prefix = nameof(HomeSystemBundles);

        public static class Styles
        {
            public const string Global = Prefix + ".Styles.Global";

            public const string jqDT_BS4 = Prefix + ".Styles.datatables.Bootsrtap4";
           
        }
        public static class Scripts
        {
            public const string Global = Prefix + ".Scripts.Global";

            public const string jqDT = Prefix + ".Scripts.datatables";

            public const string jqDT_BS4 = Prefix + ".Scripts.datatables.Bootsrtap4";

            public const string DataGrid = Prefix + ".Scripts.datatables.plugins.datagrid";
        }
    }
}
