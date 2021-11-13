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

        [HttpGet]
        [Route("/firebase-messaging-sw.js")]
        public async Task<FileContentResult> GenerateSWAsync()
        {
            string FCMVersion = await _settingManager.GetOrNullGlobalAsync("HomeSystem.FCM.FCMVersion");
            string swJS = $@"importScripts('https://www.gstatic.com/firebasejs/{FCMVersion}/firebase-app-compat.js');
importScripts('https://www.gstatic.com/firebasejs/{FCMVersion}/firebase-messaging-compat.js');

const firebaseConfig = {{
    apiKey: ""{await _settingManager.GetOrNullGlobalAsync("HomeSystem.FCM.ApiKey")}"",
    authDomain: ""{await _settingManager.GetOrNullGlobalAsync("HomeSystem.FCM.AuthDomain")}"",
    projectId: ""{await _settingManager.GetOrNullGlobalAsync("HomeSystem.FCM.ProjectId")}"",
    storageBucket: ""{await _settingManager.GetOrNullGlobalAsync("HomeSystem.FCM.StorageBucket")}"",
    messagingSenderId: ""{await _settingManager.GetOrNullGlobalAsync("HomeSystem.FCM.MessagingSenderId")}"",
    appId: ""{await _settingManager.GetOrNullGlobalAsync("HomeSystem.FCM.AppId")}"",
    measurementId: ""{await _settingManager.GetOrNullGlobalAsync("HomeSystem.FCM.MeasurementId")}""
}};

firebase.initializeApp(firebaseConfig);
const messaging = firebase.messaging();

messaging.onBackgroundMessage(function (payload) {{
    console.log('[firebase-messaging-sw.js] Received background message ', payload);
    // Customize notification here
    const notifyMsg = payload.notification;
    const notificationTitle = notifyMsg.title;
    const notificationOptions = {{
        body: notifyMsg.body,
        icon: notifyMsg.icon
    }};
        
    self.registration.showNotification(notificationTitle,
    notificationOptions);
}});";

            return File(Encoding.UTF8.GetBytes(swJS), "application/javascript", "firebase-messaging-sw.js");
        }
    }
}
