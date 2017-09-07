(function () {
    'use strict';

    angular.module('applicantLevelApp')
         .component('applicantLevelForm', {
             bindings: {
                 data: '='
             },
             templateUrl: 'modules/applicantLevelManagement/applicantLevelForm-tmpl.html',
             controller: ['applicantLevelContext', applicantLevelFormController]
         });

    function applicantLevelFormController(applicantLevelContext) {

        var alc = this;

        alc.$onInit = function () {


        };

        alc.$onChanges = function (changes) {


        };


    };

})();