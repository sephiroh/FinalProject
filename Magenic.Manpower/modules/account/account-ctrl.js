(function () {
    'use strict';

    angular
        .module('accountModule')
        .controller('accountController', accountController)

    accountController.$inject = ['accountContext', '$scope', '$rootScope', 'toaster', 'session', '$state'];

    function accountController(accountContext, $scope, $rootScope, toaster, session, $state) {
        var ac = this;
        this.username = session.data.email;
        this.userId = session.data.userId;
        this.model = {};
        this.header = 'My Account';
        this.updatePwdForm = {};

        $rootScope.$on('my-account', function () {
            ac.populateMyAccount();
        });

        this.$onInit = function () {
            ac.populateMyAccount();
        }.bind(this);

        this.populateMyAccount = function () {
            //todo: change this to use email instead
            accountContext.getMyAccount(this.userId).then(function (response) {
                if (response.data.success) {
                    ac.model = response.data.responseData;
                }
                else {
                    toaster.error("Manpower App", "Getting user info failed.");
                }
            });
        }.bind(this);
                
        this.changePassword = function (updatePwdDto, form) {
            this.updatePwdForm = form;
            accountContext.updatePassword(updatePwdDto).then(function (response) {
                if (response.data.success) {
                    $rootScope.$broadcast('change-password-completed', ac.updatePwdForm);
                }
                else {
                    var err = response.data.errors;
                    for (var i = 0; i < err.length; i++) {
                        $rootScope.$broadcast('change-password-has-error', err[i], ac.updatePwdForm);
                    }
                }
            });
        }.bind(this);

        this.showUpdatePassword = function () {
            $rootScope.$broadcast('change-password');
        }.bind(this);
    }
})();