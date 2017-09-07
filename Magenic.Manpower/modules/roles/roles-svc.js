(function () {
    'use strict';

    angular
        .module('rolesModule')
        .factory('rolesSvc', rolesSvc)

    rolesSvc.$inject = ['rolesContext'];

    function rolesSvc(rolesContext) {

        function getRoles(fn, fnx) {
            rolesContext.getRoles()
                .then(function (response) {//success handling
                    fn(response.data);
                }.bind(this)
                , function (response) {//error handling
                    fnx(response.data);
                });
        };

        function getRoles2(fn, fnx) {
            rolesContext.getRoles2()
                .then(function (response) {
                    if (response.data.success) {
                        fn(response.data.responseData);
                    }
                    else {
                        fnx(response.data.errors[0]);
                    }
                }.bind(this));
        };

        return {
            getRoles: getRoles,
            getRoles2: getRoles2
        };
    }
})();