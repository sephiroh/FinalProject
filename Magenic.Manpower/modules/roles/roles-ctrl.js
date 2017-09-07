(function () {
    'use strict';

    angular
        .module('rolesModule')
        .controller('rolesCtrl', rolesCtrl);

    rolesCtrl.$inject = ['rolesContext', '$scope', '$rootScope', 'toaster', 'lookup'];

    function rolesCtrl(rolesContext, $scope, $rootScope, toaster, lookup) {
        //properties
        this.thisRole = {};
        this.event = '';

        //public functions
        this.create = function () {
            this.event = 'role-adding';
            $rootScope.$broadcast('role-add-modal');
        };

        this.edit = function (role) {
            this.thisRole = role;
            this.event = 'role-updating';
            $rootScope.$broadcast('role-edit-modal');
        }.bind(this);


        this.save = function (role) {
            if (this.event === 'role-adding') {
                add(role);
                }
                else {
                update(role);
                }
        };

        //private functions, called internally
        var add = function (role) {
            
            rolesContext.addRole(role)
                .then(function (response) {
                    var ret = helper.evaluateResponse(response);
                    if (ret.hasOwnProperty('id')) {
                        this.event = 'role-added';
                        toaster.success("Manpower App", "Role is added.");
                    } else {
                        this.event = 'role-canceling';
                        toaster.error("Manpower App", "Invalid Role.");
                    }
                }.bind(this),
                function () {
                    this.event = 'role-canceling';
                    toaster.error("Manpower App", "Adding the role fails.</br>");
                });
                       
        }.bind(this);

        var update = function (role) {
            rolesContext.updateRole(role)
                .then(function (response) {

                    var ret = helper.evaluateResponse(response);
                    if (ret.hasOwnProperty('id')) {
                        this.event = 'role-updated';
                        toaster.success("Manpower App", "Role is updated.");
                    } else {
                        this.event = 'role-canceling';
                        toaster.error("Manpower App", "Invalid Role.");
                    }
                }.bind(this),
                function() {
                    this.event = 'role-canceling';
                    toaster.error("Manpower App", "Updating the role fails.");
                });
                        
        }.bind(this);

    }
})();