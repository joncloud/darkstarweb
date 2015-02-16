(function () {
    var dspweb = angular.module('dspweb');

    dspweb.config(['$routeProvider', '$locationProvider', function ($routeProvider, $locationProvider) {
        $routeProvider
        .when(
          '/AuctionHouse',
        {
            templateUrl: 'Auctions/AuctionHouse.min.html'
        });
    }]);

    dspweb.controller('AuctionHouseController', ['$scope', '$location', 'AuctionHouseItems', function ($scope, $location, AuctionHouseItems) {
        $scope.items = [];
        AuctionHouseItems.query(function (response) {
            angular.forEach(response, function (item) {
                $scope.items.push(item);
            });
        });
    }]);

    dspweb.factory('AuctionHouseItems', ['$resource', function ($resource) {
        return $resource('/api/AuctionHouseItems',
            { id: '@id' },
            {
                getMy: { method: 'GET', params: { id: 'My' }, isArray: true }
            });
    }]);
})();