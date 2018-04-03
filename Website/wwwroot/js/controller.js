(function () {
    'use strict';

    angular
        .module('app')
        .controller('sample', function controller($scope) {

            $scope.tryal = function (me) {
                console.log(me);
            }

        });
    
})();
