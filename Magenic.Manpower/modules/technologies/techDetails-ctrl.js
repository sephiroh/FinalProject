(function () {
    'use strict';

    angular.module('technologyApp')
        .controller('technologyDetailsController', technologyDetailsController);

    technologyDetailsController.$inject = ['$scope', 'technologyDetailsContext', 'toaster'];

    function technologyDetailsController($scope, technologyDetailsContext, toaster) {
        var controller = this;
        controller.showCheck = 3;
        //// get id from url
        //var id = $scope.$resolve.$stateParams.techId;
        //if (id != "") {
        //    // call service to get tech details 
        //    technologyDetailsContext.getTechDetail(id).then(function (response) {
        //        if (response.data.success) {
        //            controller.tech = response.data.responseData;
        //        }
        //    }, function (response) {

        //    });
        //    controller.process = "Edit";
        //} else {
        //    controller.process = "Create";
        //    clearFields();
        //}

        controller.saveTech = function (tech, techForm) {
            // check if form is valid
            if (techForm.$valid) {
                technologyDetailsContext.verifyTechDetail(tech.name, tech.id).then(function (result) {
                    if (result.showCheck == 1) {
                        // call service to save values
                        technologyDetailsContext.saveTechDetail(tech).then(function (response) {
                            // show success/error result to user
                            if (response.data.success) {
                                toaster.success("Manpower App", controller.process + ' Technology entry successful!');
                                if (controller.process == "Create") {
                                    clearFields();
                                }
                            } else {
                                var eList = "";
                                angular.forEach(response.data.errors, function (value, key) {
                                    elist = elist + " " + value;
                                });
                                toaster.error("Manpower App", controller.process + ' Technology entry failed! Error: ' + eList);
                            }
                        });
                    } else {
                        toaster.error("Manpower App", result.errors);
                    }
                });
            }
        }

        controller.verifyTech = function (tech) {
            if (tech.name.trim().length > 0) {
                technologyDetailsContext.verifyTechDetail(tech.name, tech.id).then(function (response) {
                    controller.showCheck = response.showCheck;
                    if (response.errors != "") {
                        toaster.error("Manpower App", errors);
                    }
                });
            } else {
                controller.showCheck = 3;
            }
        }

        controller.cancelTech = function () {
            clearFields();
            // redirect to tech list
            window.location.href('');
        }

        function clearFields() {
            controller.tech = { name: "", description: "", isActive: true, createDate: "", modifyDate: "" };
            controller.showCheck = 3;
        }
    }
})();