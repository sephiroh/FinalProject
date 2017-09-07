(function () {
    'use strict';

    angular.module('technologyApp')
        .factory('technologyDetailsContext', technologyDetailsContext);

    technologyDetailsContext.$inject = ['$q', 'xhr', 'appSettings', '_'];

    function technologyDetailsContext($q, xhr, appSettings, _) {
        var url = appSettings.serverPath + 'api/technologydetail/';

        var context = {
            getTechDetail: getTechDetailById,
            verifyTechDetail: verifyTechDetail,
            saveTechDetail: saveTechDetail,
            getTechDetailList: getTechDetailList,
            toggleActive: toggleActive
        };

        function getTechDetailById(id) {
            var tmpUrl = url + id;
            return xhr.get(tmpUrl);
        }

        function verifyTechDetail(tech) {
            return $q(function (resolve, reject) {
                if (tech === undefined) {
                    resolve({ showCheck: 2, errors: "Technical Name must not be blank." });
                    return;
                }

                if ($.trim(tech.name).length == 0) {
                    resolve({ showCheck: 2, errors: "Technical Name must not be blank." });
                    return;
                }

                var tmpUrl = url + "verify/" + tech.name;
                xhr.get(tmpUrl).then(function (response) {
                    if (response.data.success) {
                        switch (response.data.responseData.id) {
                            case tech.id:
                                resolve({ showCheck: 1, errors: "" });
                                break;
                            case 0:
                                resolve({ showCheck: 1, errors: "" });
                                break;
                            default:
                                resolve({ showCheck: 2, errors: "Technical Name already exists." });
                                break;
                        }

                        //if (response.data.responseData.id == tech.id || response.data.responseData.id == 0) {
                        //    resolve({ showCheck: 1, errors: "" });
                        //} else {
                        //    resolve({ showCheck: 2, errors: "" });
                        //}
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

        function saveTechDetail(tech) {
            var tmpUrl = "";
            if (tech.id == 0 || tech.id == undefined)
                tmpUrl = url + "create";
            else
                tmpUrl = url + "update";

            return xhr.post(tmpUrl, tech);
        }


        function getTechDetailList() {
            var tmpUrl = url + "getlist";
            return xhr.get(tmpUrl);
        }


        function toggleActive(tech) {
            var tmpUrl = url + tech.id;
            return xhr.delete(tmpUrl, tech);
        }
        return context;
    }
})();