(function () {
    'use strict';
    angular.module('applicantLevelApp')
          .component('applicantLevelList', {
              bindings: {
                  loadList: '<'
              },
              templateUrl: 'modules/applicantLevelManagement/applicantLevelList-component-tmpl.html',
              controller: ['$scope', '$timeout', 'applicantLevelContext', 'toaster', '$location', applicantLevelListController]
          });



    function applicantLevelListController($scope, $timeout, applicantLevelContext, toaster, $location) {
        var alc = this;
        alc.applicantLevelList = [];

        alc.$onInit = function () {
            alc.getApplicantLevelList();
        }

        alc.getApplicantLevelList = function () {
            applicantLevelContext.getApplicantLevelList().then(
                function (response) {
                    if (response.data.success) {
                        this.applicantLevelList = response.data.responseData;
                        bindToggleButton();
                    }
                    else {
                        this.applicantLevelList = [];
                        toaster.error("Manpower App", response.data.errors[0]);
                    }
                }.bind(this));
        }
        var bindToggleButton = function (id) {
            $(id ? '[data-id="alevel-' + id + '"]' : this.ui.toggles).bootstrapToggle('destroy');//clear first
            $timeout(function () {
                $(id ? '[data-id="alevel-' + id + '"]' : this.ui.toggles)
                    .bootstrapToggle({
                        on: 'Active',
                        off: 'Deactivated',
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

        alc.$onChanges = function () {
            if (alc.loadList == "load") {
                alc.getApplicantLevelList();
                $scope.$parent.alc.load = "";
            }
        }


        this.ui = {
            toggles: '[data-hook="toggles"]'
        };

        var toggleActive = function (id) {
            var alevel = _.find(this.applicantLevelList, function (item) {
                return 'alevel-' + item.id == id;
            });

            if (alevel) {
                applicantLevelContext.toggleActive(alevel).then(
                    function (response) {
                        if (response.data.success) {
                            alevel.isActive = response.data.responseData.isActive;
                            toaster.success("Manpower App", alevel.name + (alevel.isActive ? ' is activated.' : ' is deactivated.'));
                        }
                        else {
                            this.list = [];
                            toaster.error("Manpower App", response.data.errors[0]);
                        }
                    }.bind(this)
                    , function () {
                        toaster.error("Manpower App", "API call successful.");
                    }
                );
            }
        }.bind(this);

        this.goPath = function (id) {
            if (id != 0) {
                $location.url('/applicantLevel/' + id);
            } else {
                $location.url('/applicantLevel/');
            }

        }

        alc.showModal = function (modalId, alevel) {
            alc.alevel = alevel;
            $('#' + modalId).modal('show');
        }

        alc.updateApplicantLevel = function (data) {
            $scope.$parent.alc.updateApplicantLevel(data);
        }

        alc.cancelUpdate = function () {
            $scope.$parent.alc.resetData();
        }
    }
})();



