(function () {

    var dspweb = angular.module('dspweb', ['ngAnimate', 'ngResource', 'ngRoute']);

    dspweb.factory('User', function () {
        var self = {
            userName: ''
        };

        return self;
    });

    dspweb.config(['$routeProvider', '$locationProvider', function ($routeProvider, $locationProvider) {
        $routeProvider
        .when(
          '/SignIn',
        {
            templateUrl: 'SignIn.min.html',
            controller: 'SignInController'
        });
    }]);

    dspweb.controller('MainController', ['$scope', '$location', '$route', '$routeParams', function ($scope, $location) {
        $location.path('/SignIn');
    }]);

    dspweb.controller('SignInController', ['$scope', '$location', '$http', 'User', 'Menu', function ($scope, $location, $http, User, Menu) {
        $scope.password = '';
        $scope.userName = '';
        $scope.busy = false;

        $scope.signIn = function () {
            if ($scope.userName && $scope.password) {
                var request = {
                    method: 'POST',
                    url: '/Token',
                    headers: {
                        'Accept': 'application/json',
                        'Content-Type': 'application/x-www-form-urlencoded'
                    },
                    data: 'grant_type=password'
                        + '&username=' + $scope.userName
                        + '&password=' + $scope.password
                };

                $scope.busy = true;

                $http(request)
                .success(function (data) {
                    $scope.busy = false;
                    $scope.password = '';
                    var authorization = data.token_type + ' ' + data.access_token;
                    $http.defaults.headers.common.Authorization = authorization;
                    User.userName = $scope.userName;
                    $location.path('/Dashboard');
                    Menu.load();
                })
                .error(function () {
                    $scope.busy = false;
                    $scope.password = '';
                    alert('error');
                });
            }
        };

    }]);
})();
