(function () {
    'use strict';
    angular
    .module('sampleModule')
    .component('sampleList', {
        bindings: {
            list: '<'
        },
        templateUrl: 'modules/sample/sampleList.html',
        controller: sampleListController
    });

    sampleListController.$inject = ['sampleContext', '$scope'];

    function sampleListController(sampleContext, $scope) {

        this.$onInit = function () {
            sampleContext.getList().then(
                function (response) {
                    if (response.data.success) {
                        this.list = response.data.responseData;
                    }
                    else {
                        this.list = [];
                        alert(response.data.errors[0]);
                    }
                }.bind(this));
        }.bind(this);

        //this.$onChanges = function (changes) {
        //};

        this.view = function (role) {
            $scope.$emit('view-component', angular.copy(role));//use angular copy to avoid seeing your update while changing values
        };
    }
})();