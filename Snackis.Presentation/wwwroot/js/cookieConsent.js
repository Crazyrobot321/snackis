window.cookieConsent = {
    get: function () {
        return document.cookie.includes("cookie-consent=true");
        console.log("get!")
    },
    accept: function () {
        const d = new Date();
        d.setFullYear(d.getFullYear() + 1);
        document.cookie = "cookie-consent=true; expires=" + d.toUTCString() + "; path=/";
        console.log("accept!")
    }
};