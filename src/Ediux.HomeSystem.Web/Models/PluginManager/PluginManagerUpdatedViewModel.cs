using AutoMapper;

using Ediux.HomeSystem.Localization;
using Ediux.HomeSystem.Models.DTOs.PluginModule;

using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Text.Json.Serialization;

using Volo.Abp.ObjectExtending;

namespace Ediux.HomeSystem.Web.Models.PluginManager
{
    [AutoMap(typeof(PluginModuleDTO), ReverseMap = true)]
    public class PluginManagerUpdatedViewModel : ExtensibleObject
    {
        /// <summary>
        /// 資料識別碼
        /// </summary>
        [Key]
        [Required]
        [HiddenInput]
        public Guid Id { get; set; }

        /// <summary>
        /// 模組名稱
        /// </summary>
        [MaxLength(50)]
        [StringLength(50)]
        [DisplayName(HomeSystemResource.Features.PluginsManager.DTFX.Columns.Name)]
        [ReadOnly(true)]
        public string Name { get; set; }

        /// <summary>
        /// 模組路徑
        /// </summary>
        [HiddenInput]
        [MaxLength(2048)]
        [StringLength(2048)]
        [DisplayName(HomeSystemResource.Features.PluginsManager.DTFX.Columns.AssemblyPath)]
        [ReadOnly(true)]
        public string PluginPath { get; set; }

        /// <summary>
        /// Indicates whether the plugin module is disabled and will not be loaded.
        /// </summary>
        /// <value>bool</value>
        [Required]
        [DisplayName(HomeSystemResource.Features.PluginsManager.DTFX.Columns.Disabled)]
        public virtual string Disabled { get; set; }

        /// <summary>
        /// 模組組件路徑(上傳)
        /// </summary>
        [JsonIgnore]
        [IgnoreMap]
        public IFormFile AssemblyFile { get; set; }

        /// <summary>
        /// 系統環境
        /// </summary>
        [JsonIgnore]
        [IgnoreMap]
        public IWebHostEnvironment HostEnvironment { get; set; }

        public PluginManagerUpdatedViewModel()
        {
            Disabled= bool.FalseString;

        }

        public void ScanAndMoveFile()
        {
            string pluginFolderPath_Enabled = Path.Combine(HostEnvironment.ContentRootPath, "Plugins");
            string pluginFolderPath_Disabled = Path.Combine(HostEnvironment.ContentRootPath, "DisabledPlugins");

            if (Directory.Exists(pluginFolderPath_Enabled) == false)
            {
                Directory.CreateDirectory(pluginFolderPath_Enabled);
            }

            if (Directory.Exists(pluginFolderPath_Disabled) == false)
            {
                Directory.CreateDirectory(pluginFolderPath_Disabled);
            }

            if (Disabled==bool.TrueString)
            {
                string destPath = Path.Combine(pluginFolderPath_Disabled, Path.GetFileName(this.PluginPath));

                if (File.Exists(destPath) == false)
                {
                    if (File.Exists(this.PluginPath))
                    {
                        File.Move(this.PluginPath, destPath);
                    }
                    else
                    {
                        return;
                    }
                }
                else
                {
                    if (File.Exists(this.PluginPath))
                    {
                        File.Delete(this.PluginPath);
                    }
                }

                if (Path.GetExtension(this.PluginPath).ToUpperInvariant() == ".ZIP")
                {
                    string pluginsFolderPath = Path.Combine(Path.GetDirectoryName(this.PluginPath), Path.GetFileNameWithoutExtension(this.PluginPath));

                    if (Directory.Exists(pluginsFolderPath))
                    {
                        Directory.Delete(pluginsFolderPath, true);
                    }
                }

                this.PluginPath = destPath;
            }
            else
            {
                string destPath = Path.Combine(pluginFolderPath_Enabled, Path.GetFileName(this.PluginPath));

                if (File.Exists(destPath) == false)
                {
                    if (File.Exists(this.PluginPath))
                    {
                        File.Move(this.PluginPath, destPath);
                    }
                    else
                    {
                        return;
                    }
                }
                else
                {
                    if (File.Exists(this.PluginPath))
                    {
                        File.Delete(this.PluginPath);
                    }
                }

                this.PluginPath = destPath;
            }
        }
    }
}
