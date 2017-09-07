(function () {
    'use strict';
    angular
        .module('manpowerApp')
        .component('layoutHeader', {
            templateUrl: '/modules/dashboard/header.html',
            bindings: {},
            controller: headerController,
            controllerAs: 'vm'
        });

    headerController.$inject = ['$rootScope', '$state', '$timeout', 'session', 'access', 'xhr', 'appSettings'];
    function headerController($rootScope, $state, $timeout, session, access, xhr, appSettings) {
        var ctrl = this;
        ctrl.currentUser = null;
        ctrl.menuItems = [];

        ctrl.logout = function (event) {
            $timeout(function () {
                reset();
                session.destroy();
                $state.go('login');
            });
        }

        ctrl.getMyAccount = function () {
            $rootScope.$broadcast('my-account');
            $state.go('my-account');
        }

        initialize();
        
        $rootScope.$on('logout', function () {
            ctrl.logout();
        });

        function initialize() {
            $rootScope.$on('currentUser', function (event, user) {
                ctrl.currentUser = user.name;
                return;
            });

            if (access.isAuthenticated) {
                ctrl.currentUser = session.data.name;
            }

            $rootScope.$on('authorizePages', function (event, pages) {
                ctrl.menuItems = pages;
                return;
            });

            //this is for refreshing page
            if (access.isAuthenticated()) {
                ctrl.menuItems = access.reloadAuthorizedPages();
            }
        }

        function reset() {
            access.unloadPages();
            ctrl.currentUser = null;
        };
    }
}());