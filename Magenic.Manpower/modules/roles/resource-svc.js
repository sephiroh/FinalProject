(function () {
    'use strict';

    angular
        .module('common', []);

    angular
        .module('common')
        .factory('resourceSvc', resourceSvc)

    resourceSvc.$inject = [];

    function resourceSvc(appSettings) {

        return {
            getString: getString
        };

        function getString(jsonUrl, key) {
            return _.find(appSettings.resource, function (rsx) {
                return rsx.key == key;
            });
        }
    }
})();
