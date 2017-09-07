(function () {
    'use strict';

    angular
        .module('userModule')
        .factory('userContext', userContext)

    userContext.$inject = ['xhr', 'appSettings'];

    function userContext(xhr, appSettings) {

        return {
            getUsers: getUsers,
            createUser: createUser,
            updateUser: updateUser,
            toggleActive: toggleActive
        };

        function getUsers() {
            var url = appSettings.serverPath + 'api/user';

            return xhr.get(url);
        }

        function createUser(newUser) {
            var url = appSettings.serverPath + 'api/user';

            return xhr.post(url, newUser);
        }

        function updateUser(updatedUser) {
            var url = appSettings.serverPath + 'api/user';

            return xhr.put(url, updatedUser);
        }

        function toggleActive(user) {
            var url = appSettings.serverPath + 'api/user/' + user.id;

            return xhr.delete(url, user);
        }
    }
})();