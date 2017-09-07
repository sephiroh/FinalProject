(function () {
    'use strict';

    angular
        .module('sampleModule')
        .factory('sampleContext', sampleContext)

    sampleContext.$inject = ['xhr', 'appSettings'];

    function sampleContext(xhr, appSettings) {

        return {
            getList: getList,
            getItem: getItem
        };

        function getList() {
            var url = appSettings.serverPath + 'api/sample/list';
            return xhr.get(url);
        }

        function getItem() {
            var url = appSettings.serverPath + 'api/sample/item';
            return xhr.post(url, info);
        }
    }

})();