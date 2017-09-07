(function () {
    'use strict';

    angular
        .module('manpowerApp')
         .constant('permission', {
             APP_MGMT: 'Application Management',
             REQUEST_MANPOWER: 'Request Manpower',
             CANCEL_REQUEST: 'Cancel Request',
             TAG_APPLICANTS: 'Tag Applicants'
         })
        .constant('appSettings', {
            serverPath: 'http://localhost:55022/'
        })
        .constant('defaultPage', {
            ADMIN: 'users',
            CM: 'requestForm'
        });
})();