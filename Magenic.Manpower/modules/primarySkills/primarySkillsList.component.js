(function () {
    'use strict';

    angular.module('primarySkillsApp')
         .component('primarySkillsList', {
             bindings: {
                 primarySkillsData: '=',
                 newDataAdded: '<',
                 refreshCount: '<'
             },
             templateUrl: 'modules/primarySkills/primarySkillsList.component-tmpl.html',
             controller: ['$scope', '_', 'filterFilter', 'primarySkillsContext', '$timeout', 'toaster', primarySkillListController]
         });

    function primarySkillListController($scope, _, filterFilter, primarySkillsContext, $timeout, toaster) {
        
        this.sortType = 'name';
        this.sortReverse = false;      
        
        this.$onInit = function () {
            //Check if there are data passed to the component before asking the API                                                
            if (this.primarySkillsData.length === 0) {
               this.getPrimarySkills();                
            }
            else {
                bindToggleButton();
            }
        };

        this.$onChanges = function (changes) {

            if (changes.newDataAdded && changes.newDataAdded.previousValue && changes.newDataAdded.previousValue.id != changes.newDataAdded.currentValue.id) {
                this.primarySkillsData.push(changes.newDataAdded.currentValue);

                bindToggleButton(changes.newDataAdded.currentValue.id);
            }
            
            if (changes.refreshCount && changes.refreshCount.currentValue > changes.refreshCount.previousValue) {                
                this.getPrimarySkills();
            }
        };

        this.getPrimarySkills = function () {
            
            primarySkillsContext.getPrimarySkills().then(
               function (response) {                   
                   
                   if (response.data.success) {
                       if (!_.isEqual(this.primarySkillsData, response.data.responseData)) {
                           this.primarySkillsData = response.data.responseData;
                           bindToggleButton();
                       }
                   }
                   else {
                       toaster.error("Manpower App", response.data.errors[0]);
                   }
               }.bind(this)
               , function (response) {
                   toaster.error("Manpower App", "API call failed.");
               }
           );

        };

        this.updatePrimarySkill = function (primarySkill) {
            if (primarySkill.name != '' && primarySkill.description != '') {
                    primarySkillsContext.updatePrimarySkill(primarySkill).then(
                     function (response) {
                         this.getPrimarySkills();
                         toaster.success("Manpower App", "Primary Skill is updated.");
                     }.bind(this)
                     , function (response) {
                         toaster.error("Manpower App", "API call failed.");
                     }
                 );
            } else {
                toaster.error("Manpower App", "All fields must be complete");
            }
        };

        this.updatePrimarySkillStatus = function (primarySkill) {
            primarySkillsContext.updatePrimarySkill(primarySkill).then(
             function (response) {
                 toaster.success("Manpower App", "Primary Skill is updated.");
             }.bind(this)
             , function (response) {
                 toaster.error("Manpower App", "API call failed.");
             }
         );

        };

        this.setPager = function () {

            this.pager = {
                pageSize: 200,
                totalItems: this.primarySkillsData.length,
                currentPage: 1
            };

            this.pager['maxSize'] = Math.ceil(this.pager.totalItems / this.pager.pageSize);

        };

        this.showModal = function (modalId, primarySkill) {

            this.primarySkill = primarySkill;
            $('#' + modalId).modal('show');

        };

        //Private functions
        var bindToggleButton = function (id) {

            if (!id) {
                //clear first
                $("input[data-id^='primarySkill']").bootstrapToggle('destroy');
            }

            $timeout(function () {
                (id ? $("input[data-id='primarySkill-" + id + "']") : $("input[data-id^='primarySkill']"))
                    .bootstrapToggle({
                        on: 'Activate',
                        off: 'Deactivate',
                        size: 'normal',
                        onstyle: 'success',
                        offstyle: 'danger',
                        width: 100,
                        height: 23
                    })
                    .change(function () {
                        toggleActive($(this).attr('data-id'), $(this).prop('checked'));
                    });
            }.bind(this), 1000);

        }.bind(this);

        var toggleActive = function (dataId, status) {

            if (dataId) {
                var id = dataId.split('-')[1];                
                var primarySkill = _.find(this.primarySkillsData, function (item) {                    
                    if (item.id == id) {
                        item.isActive = status;
                        this.updatePrimarySkillStatus(item);

                        return;
                    }
                }.bind(this));
            }

        }.bind(this);

    };

})();