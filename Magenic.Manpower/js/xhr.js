(function () {
   'use strict';

    angular
        .module('manpowerApp')
        .factory('xhr', xhr);

    xhr.$inject = ['_', '$http', '$q'];

    function xhr(_, $http, $q) {
        return {
            get: get,
            post: post,
            'delete': del,
            jsonp: jsonp,
            put: put
        };

        function executeDeferred(fn, args) {
            var deferred = $q.defer();

            fn.apply(this, args)
                .then(function (response) {
                    deferred.resolve(response);
                })
                .catch(function (response) {
                    deferred.reject(response);
                });

            return deferred.promise;
        }

        function get() {
            return executeDeferred($http.get, _.toArray(arguments));
        }

        function post() {
            return executeDeferred($http.post, _.toArray(arguments));
        }

        function del() {
            return executeDeferred($http.delete, _.toArray(arguments));
        }

        function put() {
            return executeDeferred($http.put, _.toArray(arguments));
        }

        function jsonp() {
            return executeDeferred($http.jsonp, _.toArray(arguments));
        }
    }
}());