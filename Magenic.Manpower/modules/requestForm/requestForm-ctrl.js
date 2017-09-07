(function () {
    "use strict";

    angular
        .module('requestFormApp')
        .controller('requestFormController', requestFormController);

    requestFormController.$inject = ['_', '$scope', 'requestFormContext', 'technologyDetailsContext', 'primarySkillsContext', 'lookup', 'toaster', 'session', 'projectManagementContext'];

    function requestFormController(_, $scope, requestFormContext, technologyDetailsContext, primarySkillsContext, lookup, toaster, session, projectManagementContext) {
        var vm = this;

        vm.submit = submit;
        vm.selectedTechnologies = [];
        vm.selectedReasons = [];
        vm.numberOfHires = [];
        vm.getNumberOfHires = getNumberOfHires;

        vm.$onInit = function () {
            //console.log($scope);
            vm.regions = lookup.getRegions();
            vm.levels = lookup.getLevels();
            vm.reasonsOfRequest = lookup.getReasons();

            getPrimarySkills();
            getPrimaryTechnologies();
            getProjects();
        }

        function getPrimarySkills() {
            primarySkillsContext.getPrimarySkills()
                .then(function (res) {
                    vm.primarySkills = res.data.responseData;
                });
        }

        function getProjects() {
            projectManagementContext.getProjectList()
            .then(function (res) {
                vm.projects = res.data.responseData;
            });
        }

        function getPrimaryTechnologies() {
            technologyDetailsContext.getTechDetailList()
                .then(function (res) {
                    vm.primaryTechnologies = res.data.responseData;
                });
        }

        function submit() {
            var info = getInfo();
            requestFormContext.submitRequest(info).then(function (result) {
                if (result.data.success) {
                    toaster.success("Manpower App", "Request Submitted!");
                    window.setTimeout(function () { location.reload() }, 2000);
                } else {
                    toaster.error("Manpower App", "API call failed.");
                }
                //clearPage();
            });
        }

        function getInfo() {
            return {
                ProjectId: vm.projectId,
                RegionId: vm.region,
                NumberOfHires: vm.numberOfHires,
                ProjectedStartDate: formatDateTime(vm.startDate),
                JobDescription: vm.jobDescription,
                PrimarySkillId: vm.primarySkill,
                Technologies: vm.selectedTechnologies,
                IsForReplacement: validateReason('Replacement'),
                IsForAdditionalResource: validateReason('Additional resource'),
                IsChangeRequest: validateReason('Change request'),
                RequestedBy: session.data.userId
            };
        }

        function validateReason(reason) {
            var reasons = _.intersection(vm.reasonsOfRequest.reasons, vm.selectedReasons);
            return reasons.indexOf(reason) > -1;
        }

        function clearPage() {
            vm.projectName = "";
            vm.region = "";
            vm.applicantLevel = "";
            vm.numberOfHires = "";
            vm.startDate = "";
            vm.jobDescription = "";
            vm.primarySkill = "";
            vm.selectedTechnologies = [];
            vm.selectedReasons = [];
        }

        function formatDateTime(date) {
            var d = new Date(date);
            var curr_date = d.getDate();
            var curr_month = d.getMonth() + 1; //Months are zero based
            var curr_year = d.getFullYear();
            return curr_year + "-" + curr_month + "-" + curr_date;
        }

        function getNumberOfHires() {
            var count = 0;
            _.each(vm.numberOfHires, function (item) {
                console.log("item", item);
                if (item == undefined)
                {
                    item = 0;
                }
                count = count + item;
            });

            if (count == 0) {
                $scope.requestForm.$invalid = true;
            }
            else {
                $scope.requestForm.$invalid = false;
            }
            console.log(count);
            return count;
        };
    }
}());