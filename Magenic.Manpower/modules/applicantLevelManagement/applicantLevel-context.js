(function () {
    'use strict';

    angular.module('applicantLevelApp')
        .factory('applicantLevelContext', applicantLevelContext);

    applicantLevelContext.$inject = ['$q', 'xhr', 'appSettings', '_'];

    function applicantLevelContext($q, xhr, appSettings, _) {
        var url = appSettings.serverPath + 'api/applicantLevel/';

        var context = {
            getApplicantLevel: getApplicantLevelById,
            saveApplicantLevel: saveApplicantLevel,
            getApplicantLevelList: getApplicantLevelList,
            toggleActive: toggleActive,
            verifyApplicantLevel: verifyApplicantLevel
        };

        function getApplicantLevelById(id) {
            var tmpUrl = url + id;
            return xhr.get(tmpUrl);
        }
        
        function saveApplicantLevel(level) {
            var tmpUrl = "";
            if (level.id == 0 || level.id == undefined)
                tmpUrl = url + "create";
            else
                tmpUrl = url + "update";

            return xhr.post(tmpUrl, level);
        }


        function getApplicantLevelList() {
            var tmpUrl = url + "getlist";
            return xhr.get(tmpUrl);
        }


        function toggleActive(level) {
            var tmpUrl = url + level.id;
            return xhr.delete(tmpUrl, level);
        }


        function verifyApplicantLevel(alevel) {
            return $q(function (resolve, reject) {
                if (alevel === undefined) {
                    resolve({ showCheck: 2, errors: "Applicant Level Name must not be blank." });
                    return;
                }
                if ($.trim(alevel.name).length == 0) {
                    resolve({ showCheck: 2, errors: "Applicant Level Name must not be blank." });
                    return;
                }

                if ($.trim(alevel.description).length == 0) {
                    resolve({ showCheck: 2, errors: "Applicant Level Description must not be blank." });
                    return;
                }

                var tmpUrl = url + "verify/" + alevel.name;
                xhr.get(tmpUrl).then(function (response) {
                    if (response.data.success) {
                        switch (response.data.responseData.id) {
                            case alevel.id:
                                resolve({ showCheck: 1, errors: "" });
                                break;
                            case 0:
                                resolve({ showCheck: 1, errors: "" });
                                break;
                            default:
                                resolve({ showCheck: 2, errors: "Applicant Level Name already exists." });
                                break;
                        }
                    } else {
                        console.log(response.data.errors);
                        resolve({ showCheck: 3, errors: response.data.errors });
                    }
                }, function (response) {
                    console.log(response.data.errors);
                    resolve({ showCheck: 3, errors: response.data.errors });
                });
            });
        }

        return context;        
    }
})();