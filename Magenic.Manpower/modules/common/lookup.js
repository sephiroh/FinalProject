(function () {
    'use strict';

    var app = angular
        .module('manpowerApp')
        .factory('lookup', lookupCtrl);

    lookupCtrl.$inject = ['$q', 'xhr', 'appSettings', '$window'];

    function lookupCtrl($q, xhr, appSettings, $window) {
        var lookup = {
            initLists: initLists,
            getPermissions: function () {
                return JSON.parse(getLookup('permissions'));
            },
            getRegions: function () {
                return JSON.parse(getLookup('regions'));
            },
            getLevels: function () {
                return JSON.parse(getLookup('levels'));
            },
            getReasons: function () {
                return JSON.parse(getLookup('reasons'));
            }
        }

        var initPermissions = function () {
            var url = appSettings.serverPath + 'api/lookup/permissions';
            xhr.get(url).then(function (response) {
                $window.localStorage.setItem('permissions', JSON.stringify(response.data.responseData));
            });
        }

        var initRegions = function () {
            var url = appSettings.serverPath + 'api/lookup/regions';
            xhr.get(url).then(function (response) {
                $window.localStorage.setItem('regions', JSON.stringify(response.data.responseData));
            });
        }

        var initLevels = function () {
            var url = appSettings.serverPath + 'api/lookup/levels';
            xhr.get(url).then(function (response) {
                $window.localStorage.setItem('levels', JSON.stringify(response.data.responseData));
            });
        }

        var initReasonsOfRequest = function () {
            var selectionItems = {reasons: ['Replacement', 'Additional resource', 'Change request']};
            $window.localStorage.setItem('reasons', JSON.stringify(selectionItems));
        }

        function getLookup(key) {
            return $window.localStorage.getItem(key);
        }

        function initLists() {
            initPermissions();
            initRegions();
            initLevels();
            initReasonsOfRequest();
        }

        return lookup;
    }

    app.run(['lookup', function (lookup) {
        lookup.initLists();
    }]);
})();