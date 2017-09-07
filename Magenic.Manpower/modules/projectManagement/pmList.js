(function () {
    'use strict';

    angular
    .module('projectManagementModule')
    .component('projectList', {
        bindings: {
            eventName: '<',
            onEdit: '&'
        },
        templateUrl: 'modules/projectManagement/projecManagementList.html',
        controller: projectListCtrl
    });

    projectListCtrl.$inject = ['$scope', '$timeout', 'projectManagementContext', 'toaster'];

    function projectListCtrl($scope, $timeout, projectManagementContext, toaster) {
        
        var pmList = this;
        var pmContext = projectManagementContext;

        pmList.edit = editProject;
        pmList.list = [];

        pmList.ui = {
            toggles: '[data-hook="toggles"]'
        };

        pmList.$onChanges = function () {
            if (pmList.eventName === 'project-added' || pmList.eventName === 'project-updated' || pmList.eventName.indexOf('project-refresh') >= 0) {
                populateList();
            }
        };

        pmList.$onInit = function () {
            populateList();
        };

        function populateList() {
            pmContext.getProjectList().then(
                function (response) {
                    if (response.data.success) {
                        pmList.list = response.data.responseData;
                        bindToggleButton();
                    }
                    else {
                        pmList.list = [];
                        toaster.error("Manpower App", response.data.errors[0]);
                    }
                },
                function () {
                    toaster.error("Manpower App", "API call failed.");
                }
            );
        };

        function bindToggleButton(id) {
            $(id ? '[data-id="project-' + id + '"]' : pmList.ui.toggles).bootstrapToggle('destroy');//clear first
            $timeout(function () {
                $(id ? '[data-id="project-' + id + '"]' : pmList.ui.toggles)
                    .bootstrapToggle({
                        on: 'Enabled',
                        off: 'Disabled',
                        size: 'normal',
                        onstyle: 'success',
                        offstyle: 'danger',
                        width: 80,
                        height: 23
                    })
                    .change(function (me) {
                        toggleActive($(this).attr('data-id'));
                    });
            }, 10, false);
        };

        function toggleActive(id) {
            var proj = _.find(pmList.list, function (item) {
                return 'project-' + item.id == id;
            });

            if (proj) {
                pmContext.toggleActive(proj).then(
                    function (response) {
                        if (response.data.success) {
                            proj.isActive = response.data.responseData.isActive;
                            toaster.success("Manpower App", proj.name + (proj.isActive ? ' is activated.' : ' is deactivated.'));
                        }
                        else {
                            pmList.list = [];
                            toaster.error("Manpower App", response.data.errors[0]);
                        }
                    }
                    , function () {
                        toaster.error("Manpower App", "API call failed.");
                    }
                );
            }
        };

        function editProject(editProj) {
            pmList.onEdit({ proj: editProj });
        };
    }
})();