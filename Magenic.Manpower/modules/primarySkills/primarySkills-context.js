(function () {
    'use strict';

    angular
        .module('primarySkillsApp')
        .factory('primarySkillsContext', primarySkillsContext)

    primarySkillsContext.$inject = ['xhr', 'appSettings'];

    function primarySkillsContext(xhr, appSettings) {
       
        return {
            getPrimarySkills: getPrimarySkills,
            getPrimarySkill: getPrimarySkill,
            addPrimarySkill: addPrimarySkill,
            updatePrimarySkill: updatePrimarySkill
        };

        function getPrimarySkills() {
            var url = appSettings.serverPath + 'api/primaryskill/';
            
            return xhr.get(url);
        }

        function getPrimarySkill(id) {
            var url = appSettings.serverPath + 'api/primaryskill/' + id;

            return xhr.get(url);
        }

        function addPrimarySkill(primarySkill) {
            var url = appSettings.serverPath + 'api/primaryskill/';

            return xhr.post(url, primarySkill);
        }

        function updatePrimarySkill(primarySkill) {
            var url = appSettings.serverPath + 'api/primaryskill/';

            return xhr.put(url, primarySkill);
        }        
    }
})();