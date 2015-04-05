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

    dspweb.controller('DashboardController', ['$scope', 'Characters', 'AuctionHouseItems', 'LinkParser', function ($scope, Characters, AuctionHouseItems, LinkParser) {
        $scope.characters = [];
        Characters.getMy(function (response) {
            angular.forEach(response, function (item) {
                $scope.characters.push(item);
            });
        });

        $scope.items = [];
        AuctionHouseItems.getMy(function (response, headers) {
            var links = LinkParser.parse(headers);
            angular.forEach(response, function (item) {
                $scope.items.push(item);
            });
        });

        $scope.onlinePlayers = [];
        $scope.refreshOnline = function (pageNumber) {
            Characters.getOnline({ pageNumber: pageNumber }, function (response, headers) {
                $scope.onlinePlayerLinks = LinkParser.parse(headers);
                $scope.onlinePlayers = response;
            });
        };
        $scope.refreshOnline();
    }]);
})();