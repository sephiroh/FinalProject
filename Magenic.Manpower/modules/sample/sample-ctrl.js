(function () {
    'use strict';

    angular
        .module('sampleModule')
        .controller('sampleController', sampleController)

    sampleController.$inject = ['$scope', 'sampleContext'];

    function sampleController($scope, sampleContext) {

        var view = function (component) {
            $("sample-List").hide();
        };

        $scope.$on('view-component', function ($event, role) {
            view(role);
        });
    }
})();