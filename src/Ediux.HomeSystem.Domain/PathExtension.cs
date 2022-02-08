using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ediux.HomeSystem
{
    public static class PathExtension
    {
        /// <summary>  
        /// 絕對路徑轉相對路徑  
        /// </summary>  
        /// <param name="strUrl"></param>  
        /// <returns></returns>  
        public static string urlConvertor(this string basePath, string targetPath)
        {
            if (string.IsNullOrWhiteSpace(basePath))
                basePath = Environment.CurrentDirectory;

            if (basePath.Any(a => a == '\\') && Environment.OSVersion.Platform == PlatformID.Unix)
                basePath = basePath.Replace('\\', Path.DirectorySeparatorChar);

            if (targetPath.Any(a => a == '\\') && Environment.OSVersion.Platform == PlatformID.Unix)
                targetPath = targetPath.Replace('\\', Path.DirectorySeparatorChar);

            basePath = basePath.Replace(Path.DirectorySeparatorChar, '/');
            //Uri baseUri = new Uri(basePath);
            //Uri targetUri = new Uri(targetPath);
            //return baseUri.MakeRelativeUri(targetUri).ToString().Replace('/', Path.DirectorySeparatorChar);
            return Path.GetRelativePath(basePath,targetPath);
        }

        /// <summary>  
        /// 相對路徑轉絕對路徑  
        /// </summary>  
        /// <param name="strUrl"></param>  
        /// <returns></returns>  
        public static string urlConvertorLocal(this string basePath, string relativePath)
        { 
            if (string.IsNullOrWhiteSpace(basePath))
                basePath = Environment.CurrentDirectory;

            if (basePath.Any(a => a == '\\') && Environment.OSVersion.Platform == PlatformID.Unix)
                basePath = basePath.Replace('\\', Path.DirectorySeparatorChar);

            if (relativePath.Any(a => a == '\\') && Environment.OSVersion.Platform == PlatformID.Unix)
                relativePath = relativePath.Replace('\\', Path.DirectorySeparatorChar);

            string urlPath = Path.GetFullPath(relativePath, basePath); //轉換成絕對路徑  
            return urlPath;
        }
    }
}
