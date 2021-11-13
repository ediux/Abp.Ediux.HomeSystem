using AutoMapper;
using Ediux.HomeSystem.Models.DTOs.FCM;
using System.ComponentModel;

namespace Ediux.HomeSystem.Web.Models.FCMSettings
{
    [AutoMap(typeof(FCMSettingsDTO), ReverseMap = true)]
    public class FCMSettingsViewModel
    {        
        public string serviceKey { get; set; }

        public string apiKey { get; set; }

        public string authDomain { get; set; }

        public string projectId { get; set; }

        public string storageBucket { get; set; }

        public string messagingSenderId { get; set; }

        public string appId { get; set; }

        public string measurementId { get; set; }
    }
}
