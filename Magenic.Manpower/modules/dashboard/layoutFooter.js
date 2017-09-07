(function () {
    'use strict';
    angular
        .module('manpowerApp')
        .component('layoutFooter', {
            templateUrl: '/modules/dashboard/footer.html',
            bindings: {},
            controller: Controller
        });

    Controller.$inject = [];
    function Controller() {
        var ctrl = this;

        initialize();
        function initialize() {
        }
    }
}());