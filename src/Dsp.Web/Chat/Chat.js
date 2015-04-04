(function () {
    var dspweb = angular.module('dspweb');

    dspweb.config(['$routeProvider', '$locationProvider', function ($routeProvider, $locationProvider) {
        $routeProvider
        .when(
            '/Chat/Linkshells/:id',
        {
            templateUrl: 'Chat/Linkshell.html'
        }).when(
            '/Chat/Tells/:id',
        {
            templateUrl: 'Chat/Tell.html'
        });
    }]);

    dspweb.controller('LinkshellController', ['$scope', '$routeParams', 'Chat', function ($scope, $routeParams, Chat) {
        $scope.items = [];
        $scope.name = $routeParams.id;
        Chat.getByLinkshell({ id: $routeParams.id }, function (response) {
            $scope.items = response;
        });
    }]);

    dspweb.controller('TellController', ['$scope', '$routeParams', 'Chat', 'Characters', function ($scope, $routeParams, Chat, Characters) {
        $scope.items = [];
        $scope.character = {};
        Characters.get({ id: $routeParams.id }, function (response) {
            $scope.character = response;
        });

        Chat.getByCharacterTell({ id: $routeParams.id }, function (response) {
            $scope.items = response;
        });
    }]);

    dspweb.factory('Chat', ['$resource', function ($resource) {
        return $resource('/api/Chat',
            { id: '@id' },
            {
                getByLinkshell: {
                    method: 'GET', params: { id: '@id' }, isArray: true, url: '/api/Chat/Linkshells/:id'
                },
                getByCharacterTell: {
                    method: 'GET', params: { id: '@id' }, isArray: true, url: '/api/Chat/Tells/:id'
                }
            });
    }]);
})();