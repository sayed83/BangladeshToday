var model = { user: "IDB" };
var app = angular.module("BangladeshToday", []);

app.service('MyService', function ($http, $q) {
    this.Service = function (method, url1, data1) {
        var d = $q.defer();
        $http({
            method: method,
            url: url1,
            data: data1,
        }).then(function successCallback(response) {
            res = response.data;
            d.resolve(res);
        }, function errorCallback(response) {
            d.reject(response);
        });
        res = d.promise;
        return res;
    }
});

app.controller("TestController", function ($scope, $http, MyService) {
    b = MyService.Service("GET", '/api/Newsinfoesapi/', '')
    b.then(function (r) {
        $scope.allNews = r;
    });
    b = MyService.Service("GET", '/api/Newsinfoesapi/GetCategories', '')
    b.then(function (r) {
        alert(r);
        $scope.Categories = r;
    });

    //initial values
    $scope.allNews = [];
    $scope.Categories = [];
});