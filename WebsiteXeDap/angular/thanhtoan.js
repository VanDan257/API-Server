var app = angular.module('app', []);

app.controller("ListBike", function ($scope, $http, $window) {
    let cart = [];
    let Order = {};

    $scope.Checkout = function(){

        // objectjson_customer.cusName = $scope.tenkh;
        // objectjson_customer.phone = $scope.sdtkh;
        // objectjson_customer.email = $scope.emailkh;
        // objectjson_customer.address = $scope.diachikh;

        // orderDate = new Date();
        // Order.orderDate = formatDate(orderDate);
        Order.address = $scope.diachikh;
        Order.status = 'Chờ xác nhận';
        Order.phone = $scope.sdtkh;
        Order.email = $scope.emailkh;
        Order.cusName = $scope.tenkh;
        Order.note = $scope.notekh;

        
        $scope.myData = $window.localStorage.getItem('cart');
        cart = JSON.parse($scope.myData);
        // console.log(cart);
        var totalPay = 0;

        for(var i=0; i<cart.length; i++){
            totalPay += cart[i].quantity*cart[i].price;
        }

        Order.totalPay = totalPay;
        // Order.objectjson_customer = objectjson_customer;
        Order.listjson_detail = cart;

        $http.post(current_url + '/api-nguoidung/Order/create-donhang', Order).then(function (response) {

            $window.localStorage.removeItem('cart');
            $window.location.href = 'TrangChu.html';
            makeScript('js/main.js')
        }, function (error) {
            // alert("Thanh toán không thành công");
            $window.location.href = 'TrangChu.html';
        });

    }

});