(function () {
    'use strict';

    angular
    .module('userModule')
    .component('userList', {
        bindings: {
            thisUser: '<',
            onEdit: '&'
        },
        templateUrl: 'modules/user/userList.html',
        controller: userListController
    });

    userListController.$inject = ['$scope', '$timeout', 'userContext', 'toaster'];

    function userListController($scope, $timeout, userContext, toaster) {
        this.list = [];

        this.ui = {
            toggles: '[data-hook="toggles"]'
        };

        var populateList = function () {
            userContext.getUsers().then(
                function (response) {
                    if (response.data.success) {
                        this.list = response.data.responseData;
                        bindToggleButton();
                    }
                    else {
                        this.list = [];
                        toaster.error("Manpower App", response.data.errors[0]);
                    }
                }.bind(this)
                , function (response) {
                    toaster.error("Manpower App", "API call failed.");
                }
            );
        }.bind(this);

        var bindToggleButton = function () {
            $timeout(function () {
                $(this.ui.toggles)
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
                        toggleActive($(this).attr('data-id'), $(this).prop('checked'));
                    });
            }.bind(this), 0, false);
        }.bind(this);

        this.$onChanges = function () {
            if (this.thisRUser !== undefined) {
                this.list.push(this.thisRUser);
                bindToggleButton();
            }
        }.bind(this);

        $scope.$on('user-added', function ($event) {
            $('[data-hook="addEditModal"]').modal('hide');
            populateList();
        }.bind(this));

        $scope.$on('user-updated', function ($event, user) {
            $('[data-hook="addEditModal"]').modal('hide');
            populateList();
        }.bind(this));

        this.$onInit = function () {
            populateList();
        }.bind(this);

        var toggleActive = function (id, value) {
            var user = _.find(this.list, function (item) {
                return item.id == id;
            });
            if (user) {
                user.isActive = value;
                userContext.toggleActive(user).then(
                    function (response) {
                        if (response.data.success) {
                            toaster.success("Manpower App", user.email + (user.isActive ? ' is activated.' : ' is deactivated.'));
                        }
                        else {
                            this.list = [];
                            toaster.error("Manpower App", response.data.errors[0]);
                        }
                    }.bind(this)
                    , function (response) {
                        toaster.error("Manpower App", "API call failed.");
                    }
                );
            }
        }.bind(this);

        this.edit = function (editUser) {
            var user = {
                LastName: editUser.lastname,
                FirstName: editUser.firstname,
                Email: editUser.email,
                Id: editUser.id,
                ContactNumber: editUser.contactNumber,
                RoleId: editUser.roleId
            }
            this.onEdit({ user: user });
        }.bind(this);
    }
})();