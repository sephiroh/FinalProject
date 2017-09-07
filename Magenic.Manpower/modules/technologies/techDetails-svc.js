(function () {
    'use strict';

    angular.module('technologyDetailsApp')
        .factory('techDetailsService', technologyDetailsService);

    technologyDetailsService.$inject = ['technologyDetailsContext'];

    function technologyDetailsService(techDetailsContext) {
        var service = {
            getTech: getTech,
            saveTech: saveTech,
            verifyTech: verifyTech
        };

        function getTech(id) {
            // sample
            return { id: 1, name: 'Angular JS', description: 'Lorem ipsum dolor sit amet, consectetur adipiscing elit. Pellentesque viverra nec sapien et iaculis.' };
            //techDetailsContext.getTechDetailById(id)
            //    .then(function () {
            //    })
            //    .error(function () {
            //    });
        }

        function saveTech(tech) {

        }

        function verifyTech(name) {
            techDetailsContext.getTechDetailByName(name)
                .then(function () {
                })
                .error(function () {
                });
            return true;
        }

        return service;
    };
})();