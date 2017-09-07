(function () {
    'use strict';

    angular
        .module('loginApp')
        .controller('loginController', loginController)

    loginController.$inject = ['$timeout', '$window', '$state', 'loginContext', 'session', 'access'];

    function loginController($timeout, $window, $state, loginContext, session, access) {
        var vm = this;
        vm.hasErrors = false;
        vm.errorMessage = '';
        vm.login = login;

        function login(event) {
            vm.dataLoading = true;
            var credential = {
                Username: vm.username,
                Password: vm.password
            };

            $timeout(function () {
                loginContext.sendCredential(credential)
                    .then(function (res) {
                        if (res.data.success) {
                            var info = res.data.responseData;
                            if (info.permissions.length === 0)
                                error('No permissions found.');
                            else {
                                session.create(info.id, info.firstname, info.lastname, info.email, info.role, info.permissions);
                                if (info.role == 'HR') {
                                    $window.location.href ='http://localhost:3000/hrdashboard';
                                }
                                access.loadAuthorizedPages();
                            }
                        }
                        else {
                            error(res.data.errors[0]);
                        }
                    })
                .catch(function (res) {
                    error(res);
                });
            });
        }

        function error(msg) {
            vm.hasErrors = true;
            vm.errorMessage = msg;
            vm.dataLoading = false;
        }
    }
})();