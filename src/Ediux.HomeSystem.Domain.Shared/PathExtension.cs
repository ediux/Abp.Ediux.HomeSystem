using System;
using System.IO;
using System.Linq;

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
            Uri baseUri = new Uri(basePath);
            Uri targetUri = new Uri(targetPath);
            
            return baseUri.MakeRelativeUri(targetUri).ToString().Replace('/', Path.DirectorySeparatorChar);
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

            return Path.GetFullPath(Path.Combine(basePath, relativePath));

        }
    }
}
