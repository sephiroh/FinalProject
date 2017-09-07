(function () {
    'use strict';

    angular
        .module('projectManagementModule')
        .factory('projectManagementContext', projectManagementContext);

    projectManagementContext.$inject = ['$q', 'xhr', 'appSettings'];

    function projectManagementContext($q, xhr, appSettings) {

        return {
            getProjectList: getProjectList,
            addProject: addProject,
            updateProject: updateProject,
            toggleActive: toggleActive
        };

        function getProjectList() {
            var url = appSettings.serverPath + 'api/projectManagement';
            return xhr.get(url);
        }

        function addProject(proj) {
            var url = appSettings.serverPath + 'api/projectManagement';
            return xhr.post(url, proj);
        }

        function updateProject(proj) {
            var url = appSettings.serverPath + 'api/projectManagement/' + proj.id;

            return xhr.put(url, proj);
        }

        function toggleActive(proj) {
            var url = appSettings.serverPath + 'api/projectManagement/toggle/' + proj.id;

            return xhr.put(url, proj);
        }
    }
})();