var app = angular.module('XeDap', []);
app.controller("Product", function ($scope, $http, $window) {

    function Authentication(){
        var user = JSON.parse(localStorage.getItem('user'));
        if (!user) {
            $window.location.href = "/admin/login.html";
        }
    }

    Authentication();

});





