(function () {
    'use strict';

    angular.module('primarySkillsApp')
         .component('primarySkillsForm', {
             bindings: {
                 data: '='
             },
             templateUrl: 'modules/primarySkills/primarySkillsForm-tmpl.html',
             controller: ['primarySkillsContext', primarySkillsFormCtrl]
         });

    function primarySkillsFormCtrl(primarySkillsContext) {

        var vm = this;
        
        vm.$onInit = function () {
          

        };

        vm.$onChanges = function (changes) {


        };
        

    };

})();