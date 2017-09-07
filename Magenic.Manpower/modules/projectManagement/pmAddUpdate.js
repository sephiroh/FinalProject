(function () {
    'use strict';

    angular
        .module('projectManagementModule')
        .component('projectAddUpdate',
        {
            require: 'ngModel',
            bindings: {
                event: '<',
                thisProject: '<',
                onSave: '&',
                onCancel: '&',
                onValidate: '&'
            },
            templateUrl: 'modules/projectManagement/projecManagementAddUpdate.html',
            controller: projectAddUpdateCtrl
        });

    projectAddUpdateCtrl.$inject = ['$scope', 'lookup', 'toaster'];

    function projectAddUpdateCtrl($scope, lookup, toaster) {
        var pmctrl = this;
        var errorMessage = '';

        pmctrl.save = save;
        pmctrl.cancel = cancel;
        pmctrl.$onChanges = onChanges;
        pmctrl.project = {};

        pmctrl.ui = {
            addEditModal: '[data-hook="pmAddEditModal"]'
        };
        ///////////////////////////////////////////////////

        function onChanges() {
            prepare();
        };

        function reset() {
            pmctrl.project.id = 0;
            pmctrl.project.name = '';
            pmctrl.project.description = '';
            pmctrl.project.isActive = true;
            errorMessage = '';
        };

        function prepare() {
            if (pmctrl.event === 'project-updating') {
                pmctrl.project = angular.copy(pmctrl.thisProject);
                pmctrl.addEditModalLabel = 'Update Project';
                pmctrl.addEditModalSave = 'Update';
            }else {
                pmctrl.addEditModalLabel = 'New Project';
                pmctrl.addEditModalSave = 'Save';
                reset();
            }
        };

        function isValid() {
            // validate project name
            if (pmctrl.project.name.trim() == '' || pmctrl.project.description.trim() == '') {
                return false;
            }

            var valid = '';

            switch (pmctrl.event) {
            case 'project-adding':
                return true;
            case 'project-updating':

                // validate if name is changed
                if (pmctrl.thisProject.name.trim() !== pmctrl.project.name.trim()) {
                    valid = valid + 'true|'; // valid
                } else {
                    valid = valid + 'false|';
                }

                // validate if description is changed
                if (pmctrl.thisProject.description.trim() !== pmctrl.project.description.trim()) {
                    valid = valid + 'true|'; // valid
                } else {
                    valid = valid + 'false|';
                }
                break;
            }

            if (valid.indexOf('true') >= 0) {
                return true;
            }

            errorMessage = 'No changes found!';
            return false;
        }


        function save() {
            if (pmctrl.project.name.length > 0 && pmctrl.project.description.length > 0) {

                if (!isValid()) {
                    toaster.error("Manpower App", errorMessage);
                    return;
                }

                $(pmctrl.ui.addEditModal).modal('hide');
                pmctrl.onSave({ proj: angular.copy(pmctrl.project) });
            }
        };

        function cancel() {
            reset();
            pmctrl.onCancel();
        };

        //$scope.$on('proj-add-modal', function ($event) {
        //    $(pmctrl.ui.addEditModal).modal('show');
        //});

        //$scope.$on('proj-edit-modal', function ($event) {
        //    $(pmctrl.ui.addEditModal).modal('show');
        //});

    }
})();