using AutoMapper;

using Ediux.HomeSystem.Localization;
using Ediux.HomeSystem.Models.DTOs.PluginModule;

using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Text.Json.Serialization;

using Volo.Abp.ObjectExtending;

namespace Ediux.HomeSystem.Web.Models.PluginManager
{
    [AutoMap(typeof(PluginModuleDTO), ReverseMap = true)]
    public class PluginManagerCreateViewModel : ExtensibleObject
    {
        /// <summary>
        /// 模組名稱
        /// </summary>
        [Required]
        [MaxLength(50)]
        [StringLength(50)]
        [DisplayName(HomeSystemResource.Features.PluginsManager.DTFX.Columns.Name)]
        public string Name { get; set; }

        /// <summary>
        /// 模組路徑
        /// </summary>
        [HiddenInput]
        [MaxLength(2048)]
        [StringLength(2048)]
        [DisplayName(HomeSystemResource.Features.PluginsManager.DTFX.Columns.AssemblyPath)]
        public string PluginPath { get; set; }

        /// <summary>
        /// Indicates whether the plugin module is disabled and will not be loaded.
        /// </summary>
        /// <value>bool</value>
        [Required]
        [HiddenInput]
        [DisplayName(HomeSystemResource.Features.PluginsManager.DTFX.Columns.Disabled)]
        public virtual bool Disabled { get; set; }

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

        public PluginManagerCreateViewModel()
        {
            Disabled = false;
        }

        public void SaveToDisk()
        {
            string pluginFolderPath = Disabled ? Path.Combine(HostEnvironment.ContentRootPath, "DisabledPlugins") : Path.Combine(HostEnvironment.ContentRootPath, "Plugins");

            if (Directory.Exists(pluginFolderPath) == false)
            {
                Directory.CreateDirectory(pluginFolderPath);
            }

            if (AssemblyFile != null)
            {
                this.PluginPath = Path.Combine(pluginFolderPath, (AssemblyFile.FileName ?? AssemblyFile.Name) ?? Path.GetRandomFileName());
                this.Name = Path.GetFileNameWithoutExtension(this.PluginPath);

                if (File.Exists(PluginPath))
                {
                    File.Delete(PluginPath);
                }

                byte[] fileBuffer = AssemblyFile.GetAllBytes();

                using (FileStream fs = File.Create(PluginPath))
                {
                    fs.Write(fileBuffer, 0, fileBuffer.Length);
                    fs.Close();
                }
            }
        }



       
    }
}
