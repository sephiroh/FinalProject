(function () {
    'use strict';

    angular
        .module('technologyApp')
         .component('technologyDetailsForm', {
             bindings: {
                 isNew: '<',
                 thisTech: '<',
                 onSave: '&'
             },
             templateUrl: 'modules/technologies/technologyDetailsForm-tmpl.html',
             controller: technologyDetailsFormController,
             controllerAs: 'tf'
         });

    technologyDetailsFormController.$inject = ['$scope', 'technologyDetailsContext'];

    function technologyDetailsFormController($scope, technologyDetailsContext) {
        this.tech = {};
        this.showCheck = 3;
        
        var reset = function (form) {
            this.tech.name = '';
            this.tech.description = '';
            this.tech.isActive = true;
            this.tech.dateCreated = '';
            this.tech.dateUpdated = '';
            this.tech.id = 0;
            this.showCheck = 3;
            form.$setPristine();
        }.bind(this);

        var prepareEditMode = function () {
            this.tech = angular.copy(this.thisTech);
            this.addEditModalLabel = 'Update Technology';
            this.addEditModalSave = 'Update';
        }.bind(this);

        this.ui = {
            addEditModal: '[data-hook="addEditModal"]'
        };

        this.$onChanges = function () {
            switch (this.isNew) {
                case true:
                    this.addEditModalLabel = 'Create Technology';
                    this.addEditModalSave = 'Save';
                    break;
                case false:
                    prepareEditMode();
                    break;
            }
            this.showCheck = 3;
        }.bind(this);

        this.saveTech = function (form) {
            var newTech = angular.copy(this.tech);
            this.onSave({ tech: newTech });
            reset(form);
        }.bind(this);

        this.verifyTech = function (tech) {
            technologyDetailsContext.verifyTechDetail(tech).then(function (result) {
                this.showCheck = result.showCheck;
            }.bind(this));
        }.bind(this);

        $scope.$on('tech-add-modal', function ($event) {
            $(this.ui.addEditModal).modal('show');
        }.bind(this));

        $scope.$on('tech-edit-modal', function ($event) {
            $(this.ui.addEditModal).modal('show');
        }.bind(this));
    };
})();