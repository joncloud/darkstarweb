(function () {
    var dspweb = angular.module('dspweb');

    dspweb.factory('Menu', ['$resource', function ($resource) {
        var self = {
            menus: [],
            load: function () {

                $resource('api/Menus').query(function (response) {
                    self.menuItems = [];

                    angular.forEach(response, function (item) {
                        self.menuItems.push(item);
                    });
                });
            }
        };

        return self;
    }]);

    dspweb.controller('MenuController', ['$scope', '$location', '$http', 'Menu', 'User', function ($scope, $location, $http, Menu, User) {
        $scope.menu = Menu;
        $scope.user = User;

        $scope.isVisible = function () {
            return Menu.menus.length;
        };

        $scope.signOut = function () {
            $http.defaults.headers.common.Authorization = '';
            User.userName = '';
            Menu.menus = [];
            $location.path('/SignIn');
        };
    }]);
})();