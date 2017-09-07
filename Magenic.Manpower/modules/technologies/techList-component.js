(function () {
    'use strict';
    angular
        .module('technologyApp')
        .component('techList', {
            bindings: {
                thisTech: '<',
                onEdit: '&'
            },
            templateUrl: 'modules/technologies/techList-component-tmpl.html',
            controller: technologyListController
        });

    technologyListController.$inject = ['$scope', '$timeout', 'technologyDetailsContext', 'toaster', '$location'];

    function technologyListController($scope, $timeout, technologyDetailsContext, toaster, $location) {
        this.techList = [];

        this.ui = {
            toggles: '[data-hook="toggles"]'
        }

        this.$onInit = function () {
            getTechnologyList();
        }.bind(this);

        this.$onChanges = function () {
        }

        var getTechnologyList = function () {
            technologyDetailsContext.getTechDetailList().then(
                function (response) {
                    if (response.data.success) {
                        this.techList = response.data.responseData;
                        bindToggleButton();
                    }
                    else {
                        this.techList = [];
                        toaster.error("Manpower App", response.data.errors[0]);
                    }
                }.bind(this),
                function (response) {
                    toaster.error("Manpower App", "API call failed.");
                });
        }.bind(this);

        this.ui = {
            toggles: '[data-hook="toggles"]'
        };

        var bindToggleButton = function (id) {
            $(id ? '[data-id="tech-' + id + '"]' : this.ui.toggles).bootstrapToggle('destroy');//clear first
            $timeout(function () {
                $(id ? '[data-id="tech-' + id + '"]' : this.ui.toggles)
                    .bootstrapToggle({
                        on: 'Activate',
                        off: 'Deactivate',
                        size: 'normal',
                        onstyle: 'success',
                        offstyle: 'danger',
                        width: 120,
                        height: 23
                    })
                    .change(function () {
                        toggleActive($(this).attr('data-id'));
                    });
            }.bind(this), 10, false);
        }.bind(this);

        var toggleActive = function (id) {
            var tech = _.find(this.techList, function (item) {
                return 'tech-' + item.id == id;
            });

            if (tech) {
                technologyDetailsContext.toggleActive(tech).then(
                    function (response) {
                        if (response.data.success) {
                            tech.isActive = response.data.responseData.isActive;
                            toaster.success("Manpower App", tech.name + (tech.isActive ? ' is activated.' : ' is deactivated.'));
                        }
                        else {
                            this.list = [];
                            toaster.error("Manpower App", response.data.errors[0]);
                        }
                    }.bind(this)
                    , function () {
                        toaster.error("Manpower App", "API call failed.");
                    }
                );
            }
        }.bind(this);

        $scope.$on('tech-added', function ($event) {
            $('[data-hook="addEditModal"]').modal('hide');
            getTechnologyList();
        }.bind(this));

        $scope.$on('tech-updated', function ($event, user) {
            $('[data-hook="addEditModal"]').modal('hide');
            getTechnologyList();
        }.bind(this));

        this.edit = function (editTech) {
            var tech = {
                name: editTech.name,
                description: editTech.description,
                isActive: editTech.isActive,
                dateCreated: editTech.dateCreated,
                dateUpdated: editTech.dateUpdated,
                id: editTech.id
            }
            this.onEdit({ tech: tech });
        }.bind(this);
    }
})();



