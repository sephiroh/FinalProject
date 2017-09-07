(function () {
    'use strict';

    angular
        .module('primarySkillsApp')
        .controller('primarySkillsCtrl', primarySkillsCtrl)

    primarySkillsCtrl.$inject = ['primarySkillsContext', '$scope', '$rootScope', '$timeout', 'toaster'];

    function primarySkillsCtrl(primarySkillsContext, $scope, $rootScope, $timeout, toaster) {
               
        this.primarySkills = [];
        this.newprimarySkills = {};
        this.refreshCount = 0;

        this.$onInit = function () {

            this.resetData();

        };
        
        //Public function
        this.showModal = function (modalId) {            
            $('#' + modalId).modal('show');          
        };     

        this.createPrimarySkill = function (primarySkill) {
            if (primarySkill.name != '' && primarySkill.description != '') {

            
            primarySkillsContext.addPrimarySkill(primarySkill).then(
             function (response) {
                 //Add data to list                 
                 this.newprimarySkills = response.data;
                 
                 toaster.success("Manpower App", "Primary Skill is added.");
             }.bind(this)
             , function (response) {
                 toaster.error("Manpower App", "API call failed.");
             }
         );
            this.resetData();
            } else {
                toaster.error("Manpower App", "All fields must be complete");
            }
            
        };        

        this.resetData = function () {
            this.primarySkill = { id: 0, name: "", description: "", dateCreated: "", dateUpdated: "", isActive: false };
        };

    }
})();