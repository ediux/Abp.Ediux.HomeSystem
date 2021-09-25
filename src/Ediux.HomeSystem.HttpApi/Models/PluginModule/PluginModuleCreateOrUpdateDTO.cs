using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;

using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Ediux.HomeSystem.Models.DTOs.PluginModule
{
    public class PluginModuleCreateOrUpdateDTO : PluginModuleDTO
    {
        public IFormFile AssemblyFile { get; set; }

        public override bool Disabled
        {
            get => base.Disabled; set
            {
                base.Disabled = value;
                ScanAndMoveFile();
            }
        }

        [Newtonsoft.Json.JsonIgnore]
        [AutoMapper.IgnoreMap]
        public IWebHostEnvironment HostEnvironment { get; set; }

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

                if (File.Exists(PluginPath) == false)
                {
                    byte[] fileBuffer = AssemblyFile.GetAllBytes();

                    using (FileStream fs = File.Create(PluginPath))
                    {
                        fs.Write(fileBuffer, 0, fileBuffer.Length);
                        fs.Close();
                    }
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
                    File.Move(this.PluginPath, destPath);
                }
                else
                {
                    File.Delete(this.PluginPath);
                }
                this.PluginPath = destPath;
            }
            else
            {
                string destPath = Path.Combine(pluginFolderPath_Enabled, Path.GetFileName(this.PluginPath));

                if (File.Exists(destPath) == false)
                {
                    File.Move(this.PluginPath, destPath);
                }
                else
                {
                    File.Delete(this.PluginPath);
                }

                this.PluginPath = destPath;
            }
        }
        
    }
}
