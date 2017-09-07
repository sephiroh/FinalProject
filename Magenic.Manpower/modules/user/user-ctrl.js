(function () {
    'use strict';

    angular
        .module('userModule')
        .controller('UserController', UserController)

    UserController.$inject = ['rolesContext', 'userContext', '$scope', '$rootScope', 'toaster', 'session'];

    function UserController(rolesContext, userContext, $scope, $rootScope, toaster, session) {
        this.thisUser = {};
        this.mode = '';
        this.ui = {};
        this.rolesLov = [];

        this.$onInit = function () {
            this.rolesLov = [];
            populateRoles();
        }.bind(this);

        this.create = function () {            
            this.isNew = true;
            this.mode = 'add';
            populateRoles();
            $rootScope.$broadcast('user-add-modal');
        };

        this.edit = function (user) {
            this.isNew = false;
            this.mode = 'edit';
            this.thisUser = user;
            populateRoles();
            $rootScope.$broadcast('user-edit-modal');
        }.bind(this);

        var add = function (user) {
            userContext.createUser(user).then(
                function (response) {
                    if (response.data.success) {
                        $rootScope.$broadcast('user-added');
                        toaster.success("Manpower App", "User is added.");
                    }
                    else {
                        toaster.error("Manpower App", "Adding user failed.");
                    }
                }
            );
        }.bind(this);

        var update = function (user) {
            userContext.updateUser(user).then(
                function (response) {
                    if (response.data.success) {
                        $rootScope.$broadcast('user-updated');
                        toaster.success("Manpower App", "User is updated.");
                    }
                    else {
                        toaster.error("Manpower App", "Updating user failed.");
                    }
                }
            );
        }.bind(this);

        var populateRoles = function () {
            rolesContext.getRoles().then(
                function (response) {
                    this.rolesLov = response.data.responseData;
                }.bind(this)
                , function (response) {
                    this.rolesLov = [];
                    toaster.error("Manpower App", "Getting the roles list fails.");
                }.bind(this));
        }.bind(this);

        this.save = function (user) {
            if (this.mode === 'add') {
                add(user);
            }
            else {
                update(user);
            }
        }
    }
})();