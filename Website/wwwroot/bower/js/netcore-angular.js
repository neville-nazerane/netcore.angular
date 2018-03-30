
var netcore_angular_formDefaults = {
    onSuccess : function (result) { },
    onFail : function (result) { }, // includes both failures below
    onClientFail : function () { }, // client side failure
    onServerFail : function (result) { } // server side failure
};

(function () {

    var formDefaults = netcore_angular_formDefaults;

    angular.module('netcore-angular', [])
        .run(function ($rootScope) {
            $rootScope.formDefaults = {
                onSuccess: function (result) { },
                onFail: function (result) { }, // includes both failures below
                onClientFail: function () { }, // client side failure
                onServerFail: function (result) { } // server side failure
            };
        })
        .directive('setToScope', function () {
            return {
                scope: true,
                restrict: 'A',
                controller: function ($attrs, $scope) {
                    if (typeof (netcore_angular_pairs) !== "undefined"
                        && typeof ($attrs.netcoreAngularSet) !== "undefined") {
                        $scope[$attrs.setToScope] = netcore_angular_pairs[$attrs.netcoreAngularSet];
                    }
                }
            };
        })
        .directive('listenForScope', function () {
            return {
                scope: true,
                restrict: 'A',
                controller: function ($scope, $rootScope, $attrs) {
                    if (typeof ($attrs.rootKey) != "undefined" && typeof ($attrs.targetScope) != "undefined") {
                        $rootScope.$watch($attrs.rootKey, function (val) {
                            if (val.action == "push") {
                                if (typeof ($scope[$attrs.targetScope]) == "undefined")
                                    $scope[$attrs.targetScope] = [];
                                $scope[$attrs.targetScope].push(val.data);
                            }
                            else if (val.action == "set") {
                                $scope[$attrs.targetScope] = val.data;
                            }
                        });
                    }
                }
            };
        })
        .directive('angSubmit', function () {
            return {
                scope: true,
                restrict: 'A',
                controller: function ($scope, $rootScope, $element, $http, $attrs) {
                    $element.submit(function (e) {
                        e.preventDefault();
                        if ($(this).valid()) {
                            $http.post($(this).attr("action"), $(this).keyValArray())
                                .then(function (res) {
                                    formDefaults.onSuccess(res);
                                    $rootScope.formDefaults.onSuccess(res);
                                }, function (res) {
                                    formDefaults.onServerFail(res);
                                    formDefaults.onFail(res);
                                    $rootScope.formDefaults.onServerFail(res);
                                    $rootScope.formDefaults.onFail(res);
                                });
                        }
                        else {
                            formDefaults.onClientFail();
                            formDefaults.onFail();
                            $rootScope.formDefaults.onClientFail();
                            $rootScope.formDefaults.onFail();
                        }
                    });
                }
            }
        });

})();

(function ($) {


    $.fn.keyValArray = function () {
        var result = {};
        $.each($(this).serializeArray(), function () {
            result[this.name] = this.value;
        });
        return result;
    };

})(jQuery);