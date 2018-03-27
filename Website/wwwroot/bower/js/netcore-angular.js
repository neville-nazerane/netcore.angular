(function () {
    angular.module('netcore-angular', [])

        .directive('setToScope', function () {
            return {
                scope: true,
                restrict: 'A',
                controller: function ($attrs, $scope) {
                    if (typeof (netcore_angular_pairs) !== "undefined"
                        && typeof ($attrs.netcoreAngularSet) !== "undefined") {
                        console.log(netcore_angular_pairs[$attrs.netcoreAngularSet]);
                        $scope[$attrs.setToScope] = netcore_angular_pairs[$attrs.netcoreAngularSet];
                    }
                }
            };
        });

})();