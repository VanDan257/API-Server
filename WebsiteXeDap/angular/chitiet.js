var app = angular.module('app', ['ngSanitize']);
app.controller("ChitietCtrl", function ($scope, $http, $window) {
    $scope.sanpham;  
    $scope.LoadSanPhambyID = function () { 
		var key = 'id';
		var value = window.location.search.substring(window.location.search.indexOf(key)+key.length+1);		 

        $http({
            method: 'GET', 
            url: current_url + '/api-nguoidung/Product/get-by-id/'+ value,
        }).then(function (response) { 
            $scope.sanpham = response.data;

            let search = {};
            search.page = 1;
            search.pageSize = 6;
            search.CateID = $scope.sanpham.cateID
            $http.post(current_url + '/api-nguoidung/Product/search', search).then(function (response) {
                $scope.spsearch = response.data;
                makeScript('js/main.js')
            });

			makeScript('js/main.js')
        });
    };  
    $scope.LoadSanPhambyID();

    $scope.GetImageByProID = function () { 
		var key = 'id';
		var value = window.location.search.substring(window.location.search.indexOf(key)+key.length+1);		 

        $http({
            method: 'GET', 
            url: current_url + '/api-nguoidung/Product/getlistimgbyproid?proId='+ value,
        }).then(function (response) { 
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
            url: current_url + '/api-nguoidung/Product/getlistspecificationsbyproid?proId='+ value,
        }).then(function (response) { 
            // console.log(response.data);
            $scope.specifications = response.data;
			makeScript('js/main.js')
        });
    };  
    $scope.GetSpecificationsByProID();

    $scope.LoadlistCategory = function (){
        $http({
            method: 'GET', 
            url: current_url + '/api-nguoidung/Categories/get-all-parent-categories',
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

