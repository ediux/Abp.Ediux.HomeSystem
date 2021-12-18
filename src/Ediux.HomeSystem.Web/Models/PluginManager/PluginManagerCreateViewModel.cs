using AutoMapper;
using Ediux.HomeSystem.Localization;
using Ediux.HomeSystem.Models.DTOs.PluginModule;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.IO;
using Volo.Abp.ObjectExtending;

namespace Ediux.HomeSystem.Web.Models.PluginManager
{
    [AutoMap(typeof(PluginModuleCreateOrUpdateDTO), ReverseMap = true)]
    public class PluginManagerCreateViewModel : ExtensibleObject
    {
        [Required]
        [MaxLength(50)]
        [StringLength(50)]
        [DisplayName(HomeSystemResource.Features.PluginsManager.DTFX.Columns.Name)]
        public string Name { get; set; }

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
        public virtual bool Disabled { get; set; }

        public IFormFile AssemblyFile { get; set; }
        [Newtonsoft.Json.JsonIgnore]
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

            if (Disabled)
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
