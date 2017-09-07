(function () {
    'use strict';

    angular
    .module('rolesModule')
    .component('rolesList', {
        bindings: {
            eventName: '<',
            onEdit: '&'
        },
        templateUrl: 'modules/roles/rolesList.html',
        controller: rolesListCtrl
    });

    rolesListCtrl.$inject = ['$scope', '$timeout', 'rolesContext', 'toaster'];

    function rolesListCtrl($scope, $timeout, rolesContext, toaster) {
        //var ctrl = this; we dont have to use this approach, it opens the current instance to be messed with anywhere in the code
        //use var fxn = function(){}.bind(this); to bind the current instance and be accessed inside a callback
        this.list = [];

        this.ui = {
            toggles: '[data-hook="toggles"]'
        };

        var populateList = function () {
            rolesContext.getRoles().then(
                function (response) {
                    if (response.data.success) {
                        this.list = response.data.responseData;
                        bindToggleButton();
                    }
                    else {
                        this.list = [];
                        toaster.error("Manpower App", response.data.errors[0]);
                    }
                }.bind(this),
                function () {
                    toaster.error("Manpower App", "API call failed.");
                }
            );
        }.bind(this);

        var bindToggleButton = function (id) {
            $(id ? '[data-id="role-' + id + '"]' : this.ui.toggles).bootstrapToggle('destroy');//clear first
            $timeout(function () {
                $(id ? '[data-id="role-' + id + '"]' : this.ui.toggles)
                    .bootstrapToggle({
                        on: 'Enabled',
                        off: 'Disabled',
                        size: 'normal',
                        onstyle: 'success',
                        offstyle: 'danger',
                        width: 80,
                        height: 23
                    })
                    .change(function () {
                        toggleActive($(this).attr('data-id'));
                    });
            }.bind(this), 10, false);
        }.bind(this);

        this.$onChanges = function () {
            if (this.eventName === 'role-added' || this.eventName === 'role-updated' || this.eventName.indexOf('role-refresh') >= 0) {
                populateList();
            }
        }.bind(this);

        this.$onInit = function () {
            populateList();
        }.bind(this);

        var toggleActive = function (id) {
            var role = _.find(this.list, function (item) {
                return 'role-' + item.id == id;
            });

            if (role) {
                rolesContext.toggleActive(role).then(
                    function (response) {
                        if (response.data.success) {
                            role.isActive = response.data.responseData.isActive;
                            toaster.success("Manpower App", role.name + (role.isActive ? ' is activated.' : ' is deactivated.'));
                        }
                        else {
                            this.list = [];
                            toaster.error("Manpower App", response.data.errors[0]);
                        }
                    }.bind(this)
                    , function () {
                        toaster.error("Manpower App", "API call failed.");
                    }
                );
            }
        }.bind(this);

        this.edit = function (editRole) {
            this.onEdit({ role: editRole });
        }.bind(this);
    }
})();