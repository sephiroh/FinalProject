(function () {
    'use strict';

    angular
        .module('projectManagementModule')
        .controller('projectManagementCtrl', projectManagementCtrl);

    projectManagementCtrl.$inject = ['projectManagementContext', '$scope', 'toaster', 'lookup'];

    function projectManagementCtrl(projectManagementContext, $scope, toaster, lookup) {

        var pmCtrl = this;
        var pmContext = projectManagementContext;
        
        pmCtrl.create = createProject;
        pmCtrl.edit = editProject;
        pmCtrl.save = saveProject;
        pmCtrl.cancel = cancelProject;

        //properties
        pmCtrl.thisProject = {};
        pmCtrl.event = '';

        pmCtrl.ui = {
            addEditModal: '[data-hook="pmAddEditModal"]'
        };

        function createProject() {
            pmCtrl.event = 'project-adding';
            $(pmCtrl.ui.addEditModal).modal('show');
        };

        function editProject(proj) {
            pmCtrl.event = 'project-updating';
            pmCtrl.thisProject = proj;
            $(pmCtrl.ui.addEditModal).modal('show');
        };

        function saveProject(proj) {
            if (pmCtrl.event === 'project-adding') {
                 add(proj);
            } else {
                 update(proj);
            }
        };

        function cancelProject() {
            pmCtrl.event = 'project-canceling';
            $(pmCtrl.ui.addEditModal).modal('hide');
        };

        function add(proj) {
            pmContext.addProject(proj)
                .then(function (response) {
                    var ret = helper.evaluateResponse(response);
                    if (ret.hasOwnProperty('id')) {
                        pmCtrl.event = 'project-added';
                        toaster.success("Manpower App", "Project is added.");
                    } else {
                        pmCtrl.event = 'project-canceling';
                        toaster.error("Manpower App", "Invalid Project.");
                    }
                }.bind(pmCtrl),
                function () {
                    pmCtrl.event = 'project-canceling';
                    toaster.error("Manpower App", "Adding the project fails.</br>");
                });
        };

        function update(role) {
            pmContext.updateProject(role)
                .then(function (response) {
                    var ret = helper.evaluateResponse(response);
                    if (ret.hasOwnProperty('id')) {
                        pmCtrl.event = 'project-updated';
                        toaster.success("Manpower App", "Project is updated.");
                    } else {
                        pmCtrl.event = 'project-canceling';
                        toaster.error("Manpower App", "Invalid Project.");
                    }
                },
                function() {
                    pmCtrl.event = 'project-canceling';
                    toaster.error("Manpower App", "Updating the project fails.");
                });
        };

    }
})();