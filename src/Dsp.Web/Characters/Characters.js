(function () {
    var dspweb = angular.module('dspweb');

    dspweb.config(['$routeProvider', '$locationProvider', function ($routeProvider, $locationProvider) {
        $routeProvider
        .when(
            '/Characters/:id',
        {
            templateUrl: 'Characters/Character.min.html'
        });
    }]);

    dspweb.controller('CharacterController', ['$scope', '$routeParams', 'Characters', function ($scope, $routeParams, Characters) {
        $scope.character = {
        };
        $scope.busy = false;

        var refresh = function () {
            Characters.get({ id: $routeParams.id }, function (response) {
                $scope.character = response;
            });
        };
        refresh();

        $scope.unstuck = function (character) {
            $scope.busy = true;
            Characters.unstuck({
                id: character.CharacterId,
                Type: 1
            },
            function (response) {
                $scope.busy = false;
                refresh();
            },
            function (response) {
                $scope.busy = false;
                if (response.status == 403) {
                    alert('Unable to Unstuck Character.');
                }
            });
        };
    }]);

    dspweb.factory('Characters', ['$resource', function ($resource) {
        return $resource('/api/Characters/:id',
            { id: '@id' },
            {
                getMy: { method: 'GET', params: { id: 'My' }, isArray: true },
                getOnline: { method: 'GET', params: { id: 'Online' }, isArray: true },
                unstuck: {
                    method: 'PUT', params: { id: '@id' }, url: '/api/Characters/:id/Location'
                }
            });
    }]);
})();