
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
            $rootScope.scopeAccess = {};
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
        .directive('rootKey', function () {
            return {
                scope: true,
                restrict: 'A',
                controller: function ($scope, $rootScope, $attrs) {
                    if (typeof ($attrs.rootKey) !== "undefined") {
                        $rootScope.$watch("scopeAccess." + $attrs.rootKey, function (val) {
                            if (typeof (val) === "undefined") return;
                            var scopeKey = null;
                            if (typeof ($attrs.targetScope) !== "undefined") {
                                scopeKey = $attrs.targetScope;
                            }
                            else if (typeof (val.scopeKey) !== "undefined") {
                                scopeKey = val.scopeKey;
                            }
                            else return;
                            actionToScope($scope, scopeKey, val);
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
                                    if (typeof ($attrs.onSuccessAppend) !== "undefined") {
                                        if (typeof ($attrs.onSuccessAppendExternal) !== "undefined") {
                                            // external scope
                                            $rootScope.scopeAccess[$attrs.onSuccessAppendExternal] = {
                                                action: "push",
                                                scopeKey: $attrs.onSuccessAppend,
                                                data: res.data
                                            };
                                        }
                                        else {
                                            actionToScope($scope, $attrs.onSuccessAppend, {
                                                action: "push",
                                                data: res.data
                                            });
                                        }
                                    }
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

    function actionToScope($scope, scopeKey, meta) {
        if (meta.action === "push") {
            if (typeof ($scope[scopeKey]) === "undefined")
                $scope[scopeKey] = [];
            $scope[scopeKey].push(meta.data);
            console.log(3, $scope[scopeKey]);
        }
        else if (meta.action === "set") {
            $scope[scopeKey] = meta.data;
        }
    }


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