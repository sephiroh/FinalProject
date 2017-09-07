(function () {
    'use strict';

    angular
        .module('applicantLevelApp')
        .controller('applicantLevelController', applicantLevelController);

    applicantLevelController.$inject = ['$scope', 'applicantLevelContext', 'toaster', '$rootScope'];

    function applicantLevelController($scope, applicantLevelContext, toaster, $rootScope) {
        var alc = this;

        this.applicantLevelList = [];
        this.load = "";
        this.process = "";


        this.$oninit = function () {
            this.resetData();
        }

        this.resetData = function () {
            alc.alevel = { name: "", description: "", isActive: true, dateCreated: "", dateUpdated: "" };
            alc.showCheck = 3;
        }
        this.createApplicantLevel = function (aLevel) {
            alc.saveApplicantLevel(aLevel, "Create");
        }

        this.updateApplicantLevel = function (aLevel) {
            alc.saveApplicantLevel(aLevel, "Update");
        }

        this.saveApplicantLevel = function (aLevel, process) {
            // check if form is valid
            applicantLevelContext.verifyApplicantLevel(aLevel).then(function (result) {
                if (result.showCheck == 1) {
                    // call service to save values
                    applicantLevelContext.saveApplicantLevel(aLevel).then(function (response) {
                        // show success/error result to user
                        if (response.data.success) {
                            alc.load = "load";
                            toaster.success("Manpower App", process + ' Applicant Level entry successful!');
                            alc.resetData();
                        } else {
                            var eList = "";
                            angular.forEach(response.data.errors, function (value, key) {
                                elist = elist + " " + value;
                            });
                            toaster.error("Manpower App", process + ' Applicant Level entry failed! Error: ' + eList);
                            alc.load = "";
                        }
                    });
                } else {
                    toaster.error("Manpower App", result.errors);
                }
            });
        }

        this.showModal = function (modalId) {
            $('#' + modalId).modal('show');
        }
    }
})();