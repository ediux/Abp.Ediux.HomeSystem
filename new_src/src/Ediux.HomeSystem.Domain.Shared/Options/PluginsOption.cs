using Ediux.HomeSystem.SystemManagement;

using JetBrains.Annotations;

using System;
using System.Collections.Generic;
using System.Text;

namespace Ediux.HomeSystem.Options
{
    public class PluginsOption
    {
        /// <summary>
        /// 取得或設定擴充模組存放根目錄路徑
        /// </summary>
        [NotNull]
        public string RootPath { get; set; }

        public string ConfigurationFileName { get; set; }

        public PluginsDataDTO[] Plugins { get; set; }

        public PluginsOption()
        {
            RootPath = Environment.CurrentDirectory;
            ConfigurationFileName = "plugins.json";
            Plugins = new PluginsDataDTO[] { };
        }
    }
}
