(function () {
    'use strict';

    angular.module('myUserAccess')
        .service('userAccessSvc', userAccessSvc);


    function userAccessSvc($q) {
        return {
            getPermissions() {
                return $q((resolve, reject) => {
                    resolve({
                        data: [
                            {
                                Id: '1',
                                Name: '001',
                                Description: 'User can request manpower'
                            },
                            {
                                Id: '2',
                                Name: '002',
                                Description: 'User can add applicant as candidate to a reference number'
                            },
                            {
                                Id: '3',
                                Name: '003',
                                Description: 'User can cancel a request'
                            }
                        ]
                    });
                });
            }
        };
    }
})();