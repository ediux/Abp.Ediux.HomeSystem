using System;
using System.Collections.Generic;
using System.Text;

namespace Ediux.HomeSystem.Models.DTOs.Firebase
{
    public class PushRequestInfoDTO
    {
        public List<string> registration_ids { get; set; }
        
        public string collapse_key { get; set; }

        public string priority { get; set; }

        public Dictionary<string, string> data { get; set; }

        public PushRequestDataDTO notification { get; set; }
        public PushRequestInfoDTO()
        {
            collapse_key = null;
            registration_ids = new List<string>();
            data = new Dictionary<string, string>();
            notification = new PushRequestDataDTO();
        }

    }

    public class PushRequestDataDTO
    {
        public string title { get; set; }
        public string body { get; set; }
        public string icon { get; set; }
    }
}
