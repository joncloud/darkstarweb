(function () {
    var dspweb = angular.module('dspweb');

    dspweb.config(['$routeProvider', '$locationProvider', function ($routeProvider, $locationProvider) {
        $routeProvider
        .when(
          '/Accounting/Activities',
        {
            templateUrl: 'Accounting/Activities.html'
        })
        .when(
            '/Accounting/ChangePassword',
        {
            templateUrl: 'Accounting/ChangePassword.html'
        });
    }]);

    dspweb.controller('ActivitiesController', ['$scope', 'Activities', function ($scope, Activities) {
        $scope.items = [];
        Activities.query(function (response) {
            angular.forEach(response, function (item) {
                $scope.items.push(item);
            });
        });
    }]);

    dspweb.controller('ChangePasswordController', ['$scope', 'Account', function ($scope, Account) {
        $scope.confirmPassword = '';
        $scope.currentPassword = '';
        $scope.newPassword = '';

        $scope.changePassword = function () {
            if ($scope.newPassword != $scope.confirmPassword) {
                return;
            }

            $scope.busy = true;
            Account.changePassword({
                CurrentPassword: $scope.currentPassword,
                NewPassword: $scope.newPassword,
                ConfirmPassword: $scope.confirmPassword
            },
            function (response) {
                $scope.busy = false;
                $scope.confirmPassword = '';
                $scope.currentPassword = '';
                $scope.newPassword = '';
            },
            function (response) {
                $scope.busy = false;
                if (response.status == 401) {
                    alert('Unable to Change Password.');
                }
            });
        };
    }]);

    dspweb.factory('Activities', ['$resource', function ($resource) {
        return $resource('/api/Activities');
    }]);

    dspweb.factory('Account', ['$resource', function ($resource) {
        return $resource('/api/Account',
            {},
            {
                changePassword: { method: 'PUT', url: '/api/Account/Password' }
            });
    }]);
})();