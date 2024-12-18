window.JsFunctions = {
    GetUserRole: function () {
        let userRole = localStorage.getItem("UserRole");
        return userRole?.toLowerCase();
    }
}