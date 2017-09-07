(function () {
    'use strict';

    angular
        .module('requestFormApp')
        .factory('requestFormService', requestFormService)

    requestFormService.$inject = ['$timeout', '$http', 'requestFormContext'];

    function requestFormService($timeout, $http, requestFormContext) {
        return {
            SubmitForm: SubmitForm
        };

        function SubmitForm(info) {
            $timeout(function () {
                requestFormContext.SubmitForm(info)
                .then(function () {

                });
            });
        }
    }
})();