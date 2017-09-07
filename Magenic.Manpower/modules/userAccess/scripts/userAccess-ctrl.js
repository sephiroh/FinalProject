(function () {
    'use strict';

    angular.module('myUserAccess')
       .controller('UserAccessCtrl', UserAccessCtrl);

    UserAccessCtrl.$inject = ['$scope', 'userAccessSvc'];

    function UserAccessCtrl($scope, userAccessSvc) {
        var ua = this;
        ua.userView = '';

        ua.users = [{
            Id: '001',
            Name: 'Super X'
        }, {
            Id: '002',
            Name: 'Super X2'
        }];

        ua.user = { // User Person
            Id: '',
            Name: ''
        };

        ua.permissions = []; // List of permissions
        ua.permission = { // object to be submitted
            id: '',
            user: {},
            permissions: []
        };

        ua.clickMe = function () {
            ua.userView = ua.templates[0].url;
        };

        ua.templates =
        [
            { name: 'addUpdate', url: '/modules/userAccess/views/addUpdate-tmpl.html' },
            { name: 'main', url: '/modules/userAccess/views/userAccessList-tmpl.html' }
        ];


        userAccessSvc.getPermissions().then(function (response) {
            ua.permissions = response.data;
        });
    };
})();
