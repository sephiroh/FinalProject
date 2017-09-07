(function () {
    'use strict';

    angular
        .module('sampleModule')
        .factory('sampleService', sampleService)

    sampleService.$inject = ['$timeout', '$http', 'sampleContext'];

    function sampleService($timeout, $http, LoginContext) {
        return {
            Login: Login,
            Register: RegisterUser
        };

        function Login(username, password) {
            //$timeout(function () {
            //    LoginContext.GetByUsername(username)
            //    .then(function () {

            //    });
            //});
        }

        function RegisterUser(info) {
            //LoginContext.RegisterUser(info);
        }

    }

})();