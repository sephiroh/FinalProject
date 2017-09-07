(function () {

    angular.module('myUserAccess',[])
        .directive('myAccess', myAccess);

    function myAccess() {

        return {

            templateUrl: '/modules/userAccess/views/userAccessList-tmpl.html',

            link: function (scope, element, attrs) {
                scope.$on('clickMe', function ($compile) {
                    $('#exampleModal').modal('show');
                });

            },
            controller: 'UserAccessCtrl'
        };
    };

})();