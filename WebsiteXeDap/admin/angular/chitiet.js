 var app = angular.module('XeDap', []);
app.controller("Product", function ($scope, $http) {
    $scope.LoadSanPhambyID = function () { 
		var key = 'id';
		var value = window.location.search.substring(window.location.search.indexOf(key)+key.length+1);		 

        $http({
            method: 'GET', 
            url: current_url + '/api/Products/get-by-id?ProID='+ value,
        }).then(function (response) { 
            console.log(response.data);
            $scope.product = response.data;
			makeScript('js/main.js')
        });
    };  
    $scope.LoadSanPhambyID();

    $scope.GetImageByProID = function () { 
		var key = 'id';
		var value = window.location.search.substring(window.location.search.indexOf(key)+key.length+1);		 

        $http({
            method: 'GET', 
            url: current_url + '/api/Products/getlistimgbyproid?proId='+ value,
        }).then(function (response) { 
            
            $scope.images = response.data;
			makeScript('js/main.js')
        });
    };  
    $scope.GetImageByProID();

    $scope.LoadlistCategory = function (){
        $http({
            method: 'GET', 
            url: current_url + '/api/Categories/get-all-parent-categories',
        }).then(function (response) {
            console.log(response.data);
            $scope.tendm = response.data;
			makeScript('js/main.js')
        });
    }
    $scope.LoadlistCategory();
});

