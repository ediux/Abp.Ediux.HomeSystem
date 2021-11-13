using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ediux.HomeSystem.Models.DTOs.FCM
{
    public class FCMSettingsDTO
    {
        public string serviceKey { get; set; }

        public string apiKey { get; set; }

        public string authDomain { get; set; }

        public string projectId { get; set; }

        public string storageBucket { get; set; }

        public string messagingSenderId { get; set; }

        public string appId { get; set; }

        public string measurementId { get; set; }

        public string FCMVersion { get; set; }
    }
}
