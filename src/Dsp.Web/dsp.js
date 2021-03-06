﻿(function () {

    var dspweb = angular.module('dspweb', ['ngAnimate', 'ngResource', 'ngRoute']);

    dspweb.factory('LinkParser', function () {
        var self = {
            /**
             * Parses any links from headers.
             */
            parse: function (headers) {
                var linkText = headers().link;
                if (linkText) {
                    var links = [];

                    var linkParts = linkText.split(', ');
                    for (var i = 0; i < linkParts.length; i++) {
                        var linkPart = linkParts[i].split(' ');

                        // Parse out the URL to the related page, and how it is related.
                        var href = linkPart[0].substring(1, linkPart[0].length - 1);
                        var rel = linkPart[1].split('"')[1];
                        var link = {
                            href: href,
                            rel: rel
                        };

                        // Parse the parameters.
                        var params = href.split('?')[1].split('&');
                        for (var j = 0; j < params.length; j++) {
                            var paramParts = params[j].split('=');
                            link[paramParts[0]] = paramParts[1];
                        }

                        links.push(link);
                    }
                    return links;
                }
                return null;
            }
        };
        return self;
    });

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
            templateUrl: 'SignIn.html',
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
        $scope.canSaveToken = (typeof (Storage) !== "undefined");
        $scope.rememberMe = false;

        /**
         * Stores current session authorization
         */
        var setAuthorization = function (authorization, userName) {
            $http.defaults.headers.common.Authorization = authorization;
            User.userName = userName;
        };

        /**
         * Redirects the user to the dashboard.
         */
        var redirectToDashboard = function () {
            $location.path('/Dashboard');
            Menu.load();
        };

        // If the token was saved, then attempt to use it and log back in.
        if ($scope.canSaveToken) {
            var tokenText = localStorage.getItem('AuthorizationToken');
            if (tokenText) {
                var token = JSON.parse(tokenText);
                setAuthorization(token.authorization, token.userName);
                $http.get('/api/Test')
                .success(function (response) {
                    // Redirect to the dashboard on success.
                    redirectToDashboard();
                });
            }
        }

        $scope.signIn = function () {
            if ($scope.userName && $scope.password) {
                // Clear any existing authorization.
                setAuthorization('', '');

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

                    // Remember the authorization token if necessary.
                    if ($scope.rememberMe) {
                        localStorage.setItem(
                            'AuthorizationToken',
                            JSON.stringify({
                                authorization: authorization,
                                userName: $scope.userName
                            }));
                    }

                    setAuthorization(authorization, $scope.userName);
                    redirectToDashboard();
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
