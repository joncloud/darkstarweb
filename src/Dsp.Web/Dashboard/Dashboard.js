(function () {
    var dspweb = angular.module('dspweb');

    dspweb.config(['$routeProvider', '$locationProvider', function ($routeProvider, $locationProvider) {
        $routeProvider
        .when(
          '/Dashboard',
        {
            templateUrl: 'Dashboard/Dashboard.html'
        });
    }]);

    dspweb.controller('DashboardController', ['$scope', 'Characters', 'AuctionHouseItems', function ($scope, Characters, AuctionHouseItems) {
        $scope.characters = [];
        Characters.getMy(function (response) {
            angular.forEach(response, function (item) {
                $scope.characters.push(item);
            });
        });

        $scope.items = [];
        AuctionHouseItems.getMy(function (response) {
            angular.forEach(response, function (item) {
                $scope.items.push(item);
            });
        });

        $scope.onlinePlayers = [];
        Characters.getOnline(function (response) {
            angular.forEach(response, function (item) {
                $scope.onlinePlayers.push(item);
            });
        });
    }]);
})();