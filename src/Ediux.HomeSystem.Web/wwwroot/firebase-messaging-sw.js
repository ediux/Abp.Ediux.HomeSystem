importScripts('https://www.gstatic.com/firebasejs/9.4.1/firebase-app-compat.js');
importScripts('https://www.gstatic.com/firebasejs/9.4.1/firebase-messaging-compat.js');

const firebaseConfig = {
    apiKey: "AIzaSyDSD7bp6UTIY0Qm4gAPG8o2WyoXMrVHJPE",
    authDomain: "my-home-information-system.firebaseapp.com",
    projectId: "my-home-information-system",
    storageBucket: "my-home-information-system.appspot.com",
    messagingSenderId: "971007962832",
    appId: "1:971007962832:web:cd25c2c3f47fc7d0b678d5",
    measurementId: "G-ZYEMRGY1NF"
};

firebase.initializeApp(firebaseConfig);
const messaging = firebase.messaging();

messaging.onBackgroundMessage(function (payload) {
    console.log('[firebase-messaging-sw.js] Received background message ', payload);
    // Customize notification here
    const notificationTitle = 'Background Message Title';
    const notificationOptions = {
        body: 'Background Message body.',
        icon: '/firebase-logo.png'
    };

    self.registration.showNotification(notificationTitle,
        notificationOptions);
});

