(function () {
    'use strict';

    angular
        .module('loginApp')
        .factory('loginContext', loginContext)

    loginContext.$inject = ['xhr', 'appSettings'];

    function loginContext(xhr, appSettings) {
        return {
            sendCredential: sendCredential
        };

        function sendCredential(credential) {
            var url = appSettings.serverPath + 'api/login/';

            return xhr.post(url, credential);
        }
    }
})();