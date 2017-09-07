(function () {
    'use strict';

    angular
        .module('manpowerApp')
        .factory('access', accessValidator);

    accessValidator.$inject = ['_', 'session', 'defaultPage', '$state', '$rootScope'];

    function accessValidator(_, session, defaultPage, $state, $rootScope) {
        return {
            isAuthorized: isAuthorized,
            isAuthenticated: isAuthenticated,
            loadAuthorizedPages: loadAuthorizedPages,
            unloadPages: unloadPages,
            reloadAuthorizedPages: getAuthorizedPages
        }

        function isAuthorized(accessPermissions) {
            if (!angular.isArray(accessPermissions))
                accessPermissions = [accessPermissions];

            return (isAuthenticated()
                && _.intersection(accessPermissions, session.data.permissions).length !== 0);
        }

        function isAuthenticated() {
            return session.data !== null && !!session.data.userId;
        }

        function loadAuthorizedPages() {
            var pages = getAuthorizedPages();
            $rootScope.$broadcast('authorizePages', pages);

            if (session.isAdmin())
                $state.go(defaultPage.ADMIN);
            else
                $state.go(defaultPage.CM);
        }

        function unloadPages() {
            $rootScope.$broadcast('authorizePages', null);
        }

        function getAuthorizedPages() {
            var pages = [];
            $state.get().filter(function (cState) {
                if (cState.data !== undefined
                    && cState.data.label !== undefined
                    && cState.data.permission !== undefined
                    && _.intersection(cState.data.permission, session.data.permissions).length !== 0
                    && cState.data.isParent) {
                    
                    pages.push({ name: cState.name, label: cState.data.label });
                }
            });

            return pages;
        }

    }
})();