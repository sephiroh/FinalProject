(function () {
    'use strict';
    angular
        .module('manpowerApp')
        .component('sideNav', {
            templateUrl: '/modules/dashboard/sideNav.html',
            controller: sideNavController,
            controllerAs: 'vm'
        });

    sideNavController.$inject = ['$rootScope', 'access'];

    function sideNavController($rootScope, access) {
        var ctrl = this;
        ctrl.menuItems = [];

        initialize();

        function initialize() {
            $rootScope.$on('authorizePages', function (event, pages) {
                ctrl.menuItems = pages;
                return;
            });

            //this is for refreshing page
            if (access.isAuthenticated()) {
                ctrl.menuItems = access.reloadAuthorizedPages();
            }
        }
    }
}());