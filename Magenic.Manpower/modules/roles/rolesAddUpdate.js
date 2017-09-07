(function () {
    'use strict';

    angular
        .module('rolesModule')
        .component('rolesAddUpdate',
        {
            require: 'ngModel',
            bindings: {
                event: '<',
                thisRole: '<',
                onSave: '&',
                onValidate: '&'
            },
            templateUrl: 'modules/roles/rolesAddUpdate.html',
            controller: rolesAddUpdateCtrl
        });

    rolesAddUpdateCtrl.$inject = ['$scope', 'lookup', 'toaster'];

    function rolesAddUpdateCtrl($scope, lookup, toaster) {
        this.role = {};

        this.ui = {
            addEditModal: '[data-hook="addEditModal"]'
        };

        var reset = function () {
            this.role.id = 0;
            this.role.name = '';
            this.role.permissions = lookup.getPermissions();
            this.role.isActive = true;

            if (this.role.permissions == undefined ||
                this.role.permissions.length == undefined) return;

            if (this.role.permissions.length > 0) {
                angular.forEach(this.role.permissions,
                    function (obj) {
                        obj.selected = false;
                    });
            }
        }.bind(this);

        var prepareMode = function () {
            if (this.event === 'role-updating') {
                this.role = angular.copy(this.thisRole);
                this.role.permissions = lookup.getPermissions();

                angular.forEach(this.role.permissions,
                    function (a) {
                        var currentSelected = _.find(this.thisRole.permissions,
                            function (b) {
                                return b.id === a.id;
                            }.bind(this));

                        if (currentSelected !== undefined) {
                            a.selected = true;
                        } else {
                            a.selected = false;
                        }
                    }.bind(this));

                this.addEditModalLabel = 'Update Role';
                this.addEditModalSave = 'Update';
            }
            else {
                this.addEditModalLabel = 'New Role';
                this.addEditModalSave = 'Save';
                reset();
            }
        }.bind(this);

        var isValidRole = function () {
            var isValid = true;
            // validate role name
            if (this.role.name.trim() == '') {
                return false;
            }

            // get new selection
            var selectedPermissions = this.role.permissions.filter(function (permission) {
                return permission.selected;
            });

            switch (this.event) {
                case 'role-adding':

                    // validate selected permissions
                    if (selectedPermissions.length !== undefined && selectedPermissions.length > 0) {
                        isValid = true;
                        break;
                    }

                    isValid = false;
                    break;
                case 'role-updating':

                    // validate if name is changed
                    if (this.thisRole.name.trim() !== this.role.name.trim()) {
                        isValid = true;
                        break;
                    }

                    // get previous selection
                    var currentPermissions = angular.copy(this.thisRole.permissions);
                       

                    // validate if number of permissions selected changed
                    if (selectedPermissions.length !== currentPermissions.length) {
                        isValid = true;
                        break;
                    }

                    // validate if there are changes on permission selected
                    angular.forEach(selectedPermissions,
                        function(permission) {
                            var isFound = currentPermissions.filter(function (newP) {
                                return newP.id == permission.id;
                            });

                            if (isFound !== null) {
                                isValid = false;
                            } else {
                                isValid = true;
                                return;
                            }
                        });

                    break;
                default:
                    isValid =  false;
            }

            return isValid;
        }.bind(this);

        this.$onChanges = function () {
            prepareMode();
        }.bind(this);

        this.save = function () {
            if (this.role.name.length > 0 && this.role.permissions.length > 0) {

                if (!isValidRole()) {
                    toaster.error("Manpower App", "Invalid Role.");
                    return;
                }

                var selectedPermissions = this.role.permissions.filter(function (permission) {
                    return permission.selected;
                });

                if (selectedPermissions.length !== undefined && selectedPermissions.length > 0) {
                    this.role.permissions = selectedPermissions;
                } else {
                    toaster.error("Manpower App", "Invalid Role.");
                    return;
                }
                
                $(this.ui.addEditModal).modal('hide');
                this.onSave({ role: angular.copy(this.role) });
            }
        }.bind(this);

        this.cancel = function () {
            reset();
        }.bind(this);

        $scope.$on('role-add-modal', function ($event) {
            $(this.ui.addEditModal).modal('show');
        }.bind(this));

        $scope.$on('role-edit-modal', function ($event) {
            $(this.ui.addEditModal).modal('show');
        }.bind(this));

    }
})();