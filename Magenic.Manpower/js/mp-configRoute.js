(function () {
    'use strict';

    var app = angular.module('manpowerApp');
    app.config(['$urlRouterProvider', '$stateProvider', 'permission', function ($urlRouterProvider, $stateProvider, permission) {
        $urlRouterProvider.otherwise('/login');

        $stateProvider
            .state('home', {
                url: '/'
            })
            .state('samples', {
                url: '/samples',
                templateUrl: '/modules/sample/samples-tmpl.html',
                controller: 'sampleController',
                controllerAs: 'vm',
                data: {
                    permission: [permission.APP_MGMT]
                }
            })
            .state('login', {
                url: '/login',
                templateUrl: '/modules/login/login-tmpl.html',
                controller: 'loginController',
                controllerAs: 'vm',
                data: {
                    label: 'Login'
                }
            })
           
            .state('projectManagement', {
                url: '/projectManagement',
                templateUrl: '/modules/projectManagement/projecManagement-mdl-tmpl.html',
                controller: 'projectManagementCtrl',
                controllerAs: 'vm',
                data: {
                    permission: [permission.REQUEST_MANPOWER],
                    isParent: false,
                    label: 'Project Management'
                }
            })
            .state('primarySkills', {
                url: '/primaryskills',
                templateUrl: '/modules/primarySkills/primarySkillsList-tmpl.html',
                controller: 'primarySkillsCtrl',
                controllerAs: 'vm',
                data: {
                    permission: [permission.APP_MGMT],
                    isParent: true,
                    label: 'Primary Skills'
                }
            })
            .state('requestForm', {
                url: '/requestForm',
                templateUrl: '/modules/requestForm/requestForm-tmpl.html',
                controller: 'requestFormController',
                controllerAs: 'vm',
                data: {
                    permission: [permission.REQUEST_MANPOWER],
                    isParent: false,
                    label: 'Create New Request'
                }
            })
            .state('technology',
            {
                url: '/technology',
                templateUrl: '/modules/technologies/techList-tmpl.html',
                controller: 'technologyController',
                controllerAs: 'vm',
                data: {
                    permission: [permission.APP_MGMT],
                    isParent: true,
                    label: 'Technologies'
                }

            })
            .state('applicantLevel',
            {
                url: '/applicantLevel',
                templateUrl: '/modules/applicantLevelManagement/applicantLevelList-tmpl.html',
                controller: 'applicantLevelController',
                controllerAs: 'alc',
                data: {
                    permission: [permission.APP_MGMT],
                    isParent: true,
                    label: 'Applicant Levels'
                }
            })
            //.state('technologyDetails', {
            //    url: '/technologyDetails/:techId',
            //    templateUrl: '/modules/technologies/techDetails-tmpl.html',
            //    controller: 'technologyDetailsController',
            //    controllerAs: 'vm',
            //    data: {
            //        permission: [permission.APP_MGMT],
            //        isParent: false,
            //        label: 'Technology Details'
            //    }
            //})
             .state('roles', {
                 url: '/roles',
                 templateUrl: '/modules/roles/roles-mdl-tmpl.html',
                 controller: 'rolesCtrl',
                 controllerAs: 'vm',
                 data: {
                     permission: [permission.APP_MGMT],
                     isParent: true,
                     label: 'Roles'
                 }
             })
            .state('users', {
                url: '/user',
                templateUrl: '/modules/user/user-mdl-tmpl.html',
                controller: 'UserController',
                controllerAs: 'vm',
                data: {
                    permission: [permission.APP_MGMT],
                    isParent: true,
                    label: 'Users'
                }
            })
            .state('CM Dashboard', {
                url: '/cmdb',
                templateUrl: '/modules/cmDashboard/cmDashboard.html',
                controller: 'cmDashboardController',
                controllerAs: 'vm',
                data: {
                    permission: [permission.REQUEST_MANPOWER, permission.CANCEL_REQUEST],
                    isParent: true,
                    label: 'CM Dashbaord'
                }
            })
            .state('my-account', {
                url: '/myaccount',
                templateUrl: '/modules/account/my-account-view.html',
                controller: 'accountController',
                controllerAs: 'ac',
                data: {
                    permission: [permission.APP_MGMT, permission.REQUEST_MANPOWER],
                    isParent: false,
                    label: 'My Account'
                }
            });
    }])

    app.run(['$rootScope', '$state', 'access', function ($rootScope, $state, access) {
        $rootScope.$on('$stateChangeStart', function (event, toState, toParams) {
            if (toState.name === undefined)
                return $state.go('home');

            if (!isLoginPage(toState.name) &&
                (toState.data === undefined ||
                !access.isAuthorized(toState.data.permission))) {

                event.preventDefault();
                return $state.go('login');
            }
        });

        $rootScope.$on('$stateChangeSuccess', function (event, toState, toParams, from, fromParams) {
            if (from.name === ""
                && isLoginPage(toState.name)
                && access.isAuthenticated()) {
                event.preventDefault();
                return $state.go('users');
            }

            if (isLoginPage(toState.name) && access.isAuthenticated() && !!from.name) {
                event.preventDefault();
                return $state.go(from.name);
            }
        });

        function isLoginPage(page) {
            return page === 'login';
        }
    }]);
})();