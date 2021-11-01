using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json.Serialization;

namespace Ediux.HomeSystem.Models.SimpleUpload
{

    public class SimpleUploadResponse
    {
        [JsonPropertyName("url")]
        public string url { get; set; }
    }
}
