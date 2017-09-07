(function () {
    'use strict';

    angular
        .module('rolesModule')
        .factory('rolesContext', rolesContext);

    rolesContext.$inject = ['$q', 'xhr', 'appSettings'];

    function rolesContext($q, xhr, appSettings) {

        return {
            getRoles: getRoles,
            addRole: addRole,
            updateRole: updateRole,
            toggleActive: toggleActive
        };

        function getRoles() {
            var url = appSettings.serverPath + 'api/roles';
            return xhr.get(url);
        }

        function addRole(newRole) {
            var url = appSettings.serverPath + 'api/roles';
            return xhr.post(url, newRole);
        }

        function updateRole(role) {
            var url = appSettings.serverPath + 'api/roles/' + role.id;

            return xhr.put(url, role);
        }

        function toggleActive(role) {
            var url = appSettings.serverPath + 'api/roles/' + role.id;

            return xhr.delete(url, role);
        }
    }
})();