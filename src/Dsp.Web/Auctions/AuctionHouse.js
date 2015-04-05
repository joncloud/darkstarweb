(function () {
    var dspweb = angular.module('dspweb');

    dspweb.config(['$routeProvider', '$locationProvider', function ($routeProvider, $locationProvider) {
        $routeProvider
        .when(
          '/AuctionHouse',
        {
            templateUrl: 'Auctions/AuctionHouse.html'
        });
    }]);

    dspweb.controller('AuctionHouseController', ['$scope', '$location', 'AuctionHouseItems', function ($scope, $location, AuctionHouseItems) {
        $scope.refreshItems = function (pageNumber) {
            AuctionHouseItems.query({ pageNumber: pageNumber }, function (response, headers) {
                $scope.itemLinks = LinkParser.parse(headers);
                $scope.items = response;
            });
        };
        $scope.refreshItems();
    }]);

    dspweb.factory('AuctionHouseItems', ['$resource', function ($resource) {
        return $resource('/api/AuctionHouseItems',
            { id: '@id' },
            {
                getMy: { method: 'GET', params: { id: 'My' }, isArray: true }
            });
    }]);
})();