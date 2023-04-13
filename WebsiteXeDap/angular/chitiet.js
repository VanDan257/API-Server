var app = angular.module('app', ['ngSanitize']);
app.controller("ChitietCtrl", function ($scope, $http, $window) {
    $scope.sanpham;  
    $scope.LoadSanPhambyID = function () { 
		var key = 'id';
		var value = window.location.search.substring(window.location.search.indexOf(key)+key.length+1);		 

        $http({
            method: 'GET', 
            url: current_url + '/api/Product/get-by-id/'+ value,
        }).then(function (response) { 
            console.log(response.data);
            $scope.sanpham = response.data;
			makeScript('js/main.js')
        });
    };  
    $scope.LoadSanPhambyID();

    $scope.GetImageByProID = function () { 
		var key = 'id';
		var value = window.location.search.substring(window.location.search.indexOf(key)+key.length+1);		 

        $http({
            method: 'GET', 
            url: current_url + '/api/Product/getlistimgbyproid?proId='+ value,
        }).then(function (response) { 
            // console.log(response.data);
            $scope.images = response.data;
			makeScript('js/main.js')
        });
    };  
    $scope.GetImageByProID();

    $scope.GetSpecificationsByProID = function () { 
		var key = 'id';
		var value = window.location.search.substring(window.location.search.indexOf(key)+key.length+1);		 

        $http({
            method: 'GET', 
            url: current_url + '/api/Product/getlistspecificationsbyproid?proId='+ value,
        }).then(function (response) { 
            console.log(response.data);
            $scope.specifications = response.data;
			makeScript('js/main.js')
        });
    };  
    $scope.GetSpecificationsByProID();

    $scope.LoadlistCategory = function (){
        $http({
            method: 'GET', 
            url: current_url + '/api/Categories/get-all-parent-categories',
        }).then(function (response) {
            $scope.categories = response.data;
			makeScript('js/main.js')
        });
    }
    $scope.LoadlistCategory();

    $scope.addToCart= function(product){
        $window.addToCart(product);
    }
});

