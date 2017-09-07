(function () {
    'use strict';

    angular
        .module('manpowerApp')
        .factory('session', session)

    session.$inject = ['$cookies', '$rootScope'];

    function session($cookies, $rootScope) {
        var obj = 'user';
        var session = {
            data: {},
            isAdmin: isAdmin,
            load: load,
            create: create,
            destroy: destroy
        }

        session.load();
        return session;

        function create(id, fName, lName, email, userRole, permissions) {
            var userInfo = {
                userId: id,
                name: fName + " " + lName,
                email: email,
                role: userRole,
                permissions: permissions
            }

            $cookies.putObject(obj, userInfo);
            session.load();
            $rootScope.$broadcast('currentUser', userInfo);
        }

        function load() {
            var user = $cookies.getObject(obj);
            if (user !== undefined) {
                session.data = user
            }
        }

        function destroy() {
            $cookies.remove(obj);
            session.data = null;
        }

        function isAdmin() {
            return session.data.role === 'administrator';
        }
    }
})();