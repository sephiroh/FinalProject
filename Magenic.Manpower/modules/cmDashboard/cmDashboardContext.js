(function () {
    'use strict';

    angular
        .module('cmDashboardModule')
        .factory('cmDashboardContext', cmDashboardContext)

    cmDashboardContext.$inject = ['xhr', 'appSettings'];

    function cmDashboardContext(xhr, appSettings) {

        function getManpowerRequest() {
            var url = appSettings.serverPath + 'api/user';

            return xhr.get(url);
        }

        return {
            getManpowerRequest: getManpowerRequest
        };

    }
})();