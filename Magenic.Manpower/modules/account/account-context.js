(function () {
    'use strict';

    angular
        .module('accountModule')
        .factory('accountContext', accountContext)

    accountContext.$inject = ['xhr', 'appSettings'];

    function accountContext(xhr, appSettings) {

        return {
            getMyAccount: getMyAccount,
            updatePassword: updatePassword
        };

        function getMyAccount(userId) {
            var url = appSettings.serverPath + 'api/user/getByUserId/' + userId;
            return xhr.get(url);
        }

        function updatePassword(updatePwdDto) {
            var url = appSettings.serverPath + 'api/user/updatePassword';
            return xhr.put(url, updatePwdDto);
        }
    }
})();