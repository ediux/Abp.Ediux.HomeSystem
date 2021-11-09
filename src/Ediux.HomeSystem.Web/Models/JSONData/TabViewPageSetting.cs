

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

using Volo.CmsKit.Public.Pages;

namespace Ediux.HomeSystem.Web.Models.JSONData
{
    public class TabViewPageSetting
    {        
        [JsonPropertyName("slug")]
        public string Slug { get; set; }
        [JsonPropertyName("tabTitle")]
        public string TabTitle { get; set; }
        [JsonPropertyName("order")]
        public int Order { get; set; }
        [System.Text.Json.Serialization.JsonIgnore]
        public PageDto Page { get; set; }
    }
}
