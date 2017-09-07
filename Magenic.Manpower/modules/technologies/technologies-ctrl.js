(function () {
    'use strict';

    angular
        .module('technologyApp')
        .controller('technologyController', technologyController);

    technologyController.$inject = ['$scope', 'technologyDetailsContext', 'toaster', '$rootScope'];

    function technologyController($scope, technologyDetailsContext, toaster, $rootScope) {
        this.techList = [];
        this.mode = "";
        this.thisTech = {};

        this.$oninit = function () {
            this.resetData();
        }.bind(this);

        this.createTechModal = function () {
            this.isNew = true;
            this.mode = "Create";
            $rootScope.$broadcast("tech-add-modal");
        }

        this.updateTechModal = function (tech) {
            this.isNew = false;
            this.mode = "Update";
            this.thisTech = tech;
            $rootScope.$broadcast("tech-edit-modal");
        }.bind(this);

        var saveTech = function (tech, process) {
            // check if form is valid
            technologyDetailsContext.verifyTechDetail(tech).then(function (result) {
                if (result.showCheck == 1) {
                    // call service to save values
                    technologyDetailsContext.saveTechDetail(tech).then(function (response) {
                        // show success/error result to user
                        if (response.data.success) {
                            if (process == "Create") {
                                $rootScope.$broadcast('tech-added');
                            } else {
                                $rootScope.$broadcast('tech-updated');
                            }
                            toaster.success("Manpower App", process + ' Technology entry successful!');
                        } else {
                            var eList = "";
                            angular.forEach(response.data.errors, function (value, key) {
                                elist = elist + " " + value;
                            });
                            toaster.error("Manpower App", process + ' Technology entry failed! Error: ' + eList);
                        }
                    });
                } else {
                    
                }
            });
        }.bind(this);

        this.save = function (tech) {
            saveTech(tech, this.mode);
        }

        this.resetData = function () {
            this.tech = { name: "", description: "", isActive: true, dateCreated: "", dateUpdated: "" };
        }.bind(this);
    }
})();