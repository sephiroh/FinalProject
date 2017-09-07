(function () {
    'use strict';

    angular
        .module('requestFormApp')
        .factory('requestFormContext', requestFormContext)

    requestFormContext.$inject = ['xhr', 'appSettings'];

    function requestFormContext(xhr, appSettings) {

        return {
            submitRequest: submitRequest
        };

        function submitRequest(info) {
            var url = appSettings.serverPath + 'api/request/submit';

            return xhr.post(url, info);
        }
    }

})();