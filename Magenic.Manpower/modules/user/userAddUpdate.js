(function () {
    'use strict';

    angular
        .module('userModule')
        .component('userAddUpdate', {
            bindings: {
                isNew: '<',
                rolesLov: '<',
                thisUser: '<',
                onSave: '&'
            },
            templateUrl: 'modules/user/userAddUpdate.html',
            controller: userAddUpdateCtrl,
            controllerAs: 'ran'
        });

    userAddUpdateCtrl.$inject = ['$scope', 'rolesContext'];

    function userAddUpdateCtrl($scope, rolesContext) {
        this.user = {};

        var reset = function (form) {            
            this.user.Id = '';
            this.user.FirstName = '';
            this.user.LastName = '';
            this.user.Email = '';
            this.user.RoleId = '';
            this.user.ContactNumber = '';
            form.$setPristine();
            this.roles = angular.copy(this.rolesLov);
        }.bind(this);

        var prepareEditMode = function () {
            this.roles = angular.copy(this.rolesLov);
            this.user = angular.copy(this.thisUser);
            this.addEditModalLabel = 'Update User';
            this.addEditModelSave = 'Update';
        }.bind(this);

        this.ui = {
            addEditModal: '[data-hook="addEditModal"]'
        };

        this.$onChanges = function () {
            switch (this.isNew) {
                case true:
                    this.addEditModalLabel = 'New User';
                    this.addEditModalSave = 'Save';
                    this.roles = angular.copy(this.rolesLov);
                    break;
                case false:
                    prepareEditMode();
                    break;
            };
        }.bind(this);

        this.onAddUpdate = function (form) {
            var newUser = angular.copy(this.user);

            this.onSave({ user: newUser });
            reset(form);
        }.bind(this);

        //this.onCancel = function () {
        //    reset();
        //}.bind(this);

        $scope.$on('user-add-modal', function ($event) {
            $(this.ui.addEditModal).modal('show');
        }.bind(this));

        $scope.$on('user-edit-modal', function ($event) {
            $(this.ui.addEditModal).modal('show');
        }.bind(this));
    }    
})();