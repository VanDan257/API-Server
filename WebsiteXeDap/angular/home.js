var app = angular.module('app', []);

app.controller("ListBike", function ($scope, $http, $window) {
    $scope.products;
    $scope.listSanPhamMoi;  
    $scope.listCategory;
    $scope.LoadListBike = function () {		 
        $http({
            method: 'GET', 
            url: current_url + '/api-nguoidung/Product/getall',
        }).then(function (response) {
            $scope.products = response.data;
			makeScript('js/main.js')
        });
    };  
    $scope.LoadListBike();

    $scope.NewProducts = function () {		 
        $http({
            method: 'GET', 
            url: current_url + '/api-nguoidung/Product/get-new-products',
        }).then(function (response) {	
            $scope.newproducts = response.data;
			makeScript('js/main.js')
        });
    };
    $scope.NewProducts();  

    $scope.BestSellerProducts = function () {		 
        $http({
            method: 'GET', 
            url: current_url + '/api-nguoidung/Product/get-best-seller-products',
        }).then(function (response) {	
            $scope.listSanPhamMoi = response.data;
			makeScript('js/main.js')
        });
    };
    $scope.BestSellerProducts();  

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
    
    $scope.GetXeDapByCategory = function() {
        let search = {};
        search.page = 1;
        search.pageSize = 8;
        search.CateID = 1;
        $http.post(current_url + '/api-nguoidung/Product/search', search).then(function (response) {
            $scope.xedapbycategories = response.data;
            makeScript('js/main.js')
        });
    }
    $scope.GetXeDapByCategory();

    $scope.GetPhuTungByCategory = function() {
        let search = {};
        search.page = 1;
        search.pageSize = 8;
        search.CateID = 2;
        $http.post(current_url + '/api-nguoidung/Product/search', search).then(function (response) {
            // console.log(response.data);
            $scope.phutungbycategories = response.data;
            makeScript('js/main.js')
        });
    }
    $scope.GetPhuTungByCategory();

    $scope.GetPhuKienByCategory = function() {
        let search = {};
        search.page = 1;
        search.pageSize = 8;
        search.CateID = 3;
        $http.post(current_url + '/api-nguoidung/Product/search', search).then(function (response) {
            // console.log(response.data);
            $scope.phukienbycategories = response.data;
            makeScript('js/main.js')
        });
    }
    $scope.GetPhuKienByCategory();

    $scope.GetBoTruyenDongByCategory = function() {
        let search = {};
        search.page = 1;
        search.pageSize = 8;
        search.CateID = 4;
        $http.post(current_url + '/api-nguoidung/Product/search', search).then(function (response) {
            // console.log(response.data);
            $scope.botruyendongbycategories = response.data;
            makeScript('js/main.js')
        });
    }
    $scope.GetBoTruyenDongByCategory();

    $scope.GetKhungSuonByCategory = function() {
        let search = {};
        search.page = 1;
        search.pageSize = 8;
        search.CateID = 5;
        $http.post(current_url + '/api-nguoidung/Product/search', search).then(function (response) {
            // console.log(response.data);
            $scope.khungsuonbycategories = response.data;
            makeScript('js/main.js')
        });
    }
    $scope.GetKhungSuonByCategory();

    // let spsearch = []

    $scope.Search = function(page){
        var key = 'id';
        var value = window.location.search.substring(window.location.search.indexOf(key)+key.length+1);	

        var vietnameseString = decodeURIComponent(value.replace(/\+/g, '%20'));
        // console.log(vietnameseString);

        let search = {};
        $scope.listpage = [];
        // var pageIndex = $scope.page;
        if(page == undefined){
            page = 1;
        }
        search.page = page;
        search.pageSize = 12;
        search.FromPrice = $scope.fromPrice;
        search.ToPrice = $scope.toPrice;
        // search.Title = vietnameseString;
        search.CateID = value
        $http.post(current_url + '/api-nguoidung/Product/search', search).then(function (response) {
            $scope.spsearch = response.data;
            var pages = Math.ceil(response.data.totalItems/response.data.pageSize);
            for (let index = 1; index <= pages; index++) {
                $scope.listpage[index-1] = index;
            }
            // console.log($scope.listpage);
            makeScript('js/main.js')
        });
    }
    $scope.Search();

    $scope.handlerPageIndex= function(index) {
        $scope.Search(index);
    }

    $scope.addToCart= function(product){
        // console.log(product);
        $window.addToCart(product);
    }

});





