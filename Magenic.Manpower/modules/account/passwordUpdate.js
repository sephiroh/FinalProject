(function () {
    'use strict';

    angular
        .module('accountModule')
        .component('passwordUpdate', {
            bindings: {
                onSave: '&'
            },
            templateUrl: 'modules/account/passwordUpdate.html',
            controller: passwordUpdateCtrl,
            controllerAs: 'pu'
        });

    passwordUpdateCtrl.$inject = ['$scope', 'accountContext', 'session', 'toaster', '$rootScope'];

    function passwordUpdateCtrl($scope, accountContext, session, toaster, $rootScope) {
        var pu = this;
        this.updatePwdDto = {};
        this.updatePwdDto.currentPassword = '';
        this.updatePwdDto.newPassword = '';
        this.updatePwdDto.confirmNewPassword = '';
        this.form = {};

        this.updatePasswordModalLabel = 'Change My Password';
        this.username = session.data.email;

        var reset = function (form) {
            this.currentPassword = '';
            this.newPassword = '';
            this.confirmNewPassword = '';
            if (form !== undefined) {
                form.$setPristine();
            }
        }.bind(this);

        this.ui = {
            updatePasswordModal: '[data-hook="updatePasswordModal"]'
        };

        this.updatePassword = function (form) {
            this.form = form;

            if (this.newPassword === this.currentPassword) {
                $scope.$broadcast('change-password-has-error', "New Password should not be the same as the Current one.", form);
                return;
            } else if (this.newPassword !== this.confirmNewPassword) {
                // todo: udpate UI validations when pwds dont match
                $scope.$broadcast('change-password-has-error', "Passwords don't match.", form);
                return;
            }

            var updatePwdDto = {
                Username: angular.copy(this.username),
                CurrentPassword: angular.copy(this.currentPassword),
                NewPassword: angular.copy(this.newPassword)
            };
            this.onSave({ updatePwdDto: updatePwdDto, form: form });

        }.bind(this);

        $scope.$on('change-password', function ($event) {
            $(this.ui.updatePasswordModal).modal('show');
        }.bind(this));

        $scope.$on('change-password-has-error', function ($event, error, form) {
            toaster.error("Manpower App", error);
            reset(form);
        }.bind(this));

        $scope.$on('change-password-completed', function ($event, form) {
            toaster.success("Manpower App", "Password was updated Successfully.");
            $(this.ui.updatePasswordModal).modal('hide');
            reset(form);
            setTimeout(function () {
                $rootScope.$broadcast('logout');
            }, 500);

        }.bind(this));

    }
})();