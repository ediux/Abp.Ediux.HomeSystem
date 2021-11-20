using Ediux.HomeSystem.Settings;
using Microsoft.AspNetCore.Mvc;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.SettingManagement;

namespace Ediux.HomeSystem.Controllers
{
    [ApiController]
    public class MiscellaneousController : HomeSystemController
    {
        private readonly ISettingManager _settingManager;   

        public MiscellaneousController(ISettingManager settingManager)
        {
            _settingManager = settingManager;
        }

        /// <summary>
        /// 動態產生Firebase SDK下Service Worker的JavaScript檔案
        /// </summary>
        /// <returns>產生的JavaScript</returns>
        [HttpGet]
        [Route("/firebase-messaging-sw.js")]
        public async Task<FileContentResult> GenerateSWAsync()
        {
            string FCMVersion = await _settingManager.GetOrNullGlobalAsync(HomeSystemSettings.FCMSettings.FCMVersion);
            string swJS = $@"importScripts('https://www.gstatic.com/firebasejs/{FCMVersion}/firebase-app-compat.js');
importScripts('https://www.gstatic.com/firebasejs/{FCMVersion}/firebase-messaging-compat.js');

const firebaseConfig = {{
    apiKey: ""{await _settingManager.GetOrNullGlobalAsync(HomeSystemSettings.FCMSettings.ApiKey)}"",
    authDomain: ""{await _settingManager.GetOrNullGlobalAsync(HomeSystemSettings.FCMSettings.AuthDomain)}"",
    projectId: ""{await _settingManager.GetOrNullGlobalAsync(HomeSystemSettings.FCMSettings.ProjectId)}"",
    storageBucket: ""{await _settingManager.GetOrNullGlobalAsync(HomeSystemSettings.FCMSettings.StorageBucket)}"",
    messagingSenderId: ""{await _settingManager.GetOrNullGlobalAsync(HomeSystemSettings.FCMSettings.MessagingSenderId)}"",
    appId: ""{await _settingManager.GetOrNullGlobalAsync(HomeSystemSettings.FCMSettings.AppId)}"",
    measurementId: ""{await _settingManager.GetOrNullGlobalAsync(HomeSystemSettings.FCMSettings.MeasurementId)}""
}};

firebase.initializeApp(firebaseConfig);
const messaging = firebase.messaging();

messaging.onBackgroundMessage(function (payload) {{
    console.log('[firebase-messaging-sw.js] Received background message ', payload);
}});";

            return File(Encoding.UTF8.GetBytes(swJS), "application/javascript", "firebase-messaging-sw.js");
        }
    }
}
