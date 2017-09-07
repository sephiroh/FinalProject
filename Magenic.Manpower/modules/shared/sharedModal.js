(function () {
    'use strict';

    angular.module('sharedApp')
         .component('sharedModal', {
             bindings: {
                 data: '=',
                 modalid: '<',
                 title: '<',
                 body: '@',
                 onSave: '&',
                 onCancel: '&',
                 saveButtonLabel: '<',
                 saveButtonClass: '<',                               
             },
             templateUrl: 'modules/shared/sharedModal-tmpl.html',
             controller: sharedModalController
         });

    function sharedModalController($sce) {

        var vm = this;        
                       
        vm.$onInit = function () {                    
            

        };

        vm.$onChanges = function (changesObj) {
            
        };

        vm.save = function () {
            
            vm.onSave({ data: vm.data });

            //Hide Modal
            setTimeout(function () {                
                $('#' + vm.modalid).modal('hide');
            }, 1000);

        };

        vm.cancel = function () {
            vm.onCancel();
            setTimeout(function () {                
                $('#' + vm.modalid).modal('hide');
            }, 1000);
        }
     
    };

})();