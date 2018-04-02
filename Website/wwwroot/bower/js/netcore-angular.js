
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
        .directive('swapable', function () {
            return {
                scope: true,
                restrict: 'A',
                controller: function ($scope) {

                    $scope.swapCurrentIndex = 0;

                    $scope.swap = function () {
                        $scope.swapCurrentIndex = 1 - $scope.swapCurrentIndex;
                    };
                    $scope.swapTo = function (index) {
                        $scope.swapCurrentIndex = index;
                    }
                }
            }
        })
        .directive('swapIndex', function () {
            return {
                scope: true,
                restrict: 'A',
                controller: function ($scope, $attrs, $element) {

                    $scope.swappedIn = false;

                    $scope.$parent.$watch('swapCurrentIndex', function (val) {
                        if (val === Number($attrs.swapIndex)) {
                            $scope.swappedIn = true;
                            $element.show();
                            if (typeof ($attrs.loadOnSwap) !== "undefined") {
                                $scope.loadContent();
                            }
                        }
                        else {
                            $scope.swappedIn = false;
                            $element.hide()
                            if (typeof ($attrs.loadOnSwap) !== "undefined") {
                                $scope.unloadContent();
                            }
                        }
                    });

                    $scope.swap = function () {
                        $scope.$parent.swap();
                    };
                }
            };
        })
        .directive('loadUrl', function () {
            return {
                scope: true,
                restrict: 'A',
                controller: function ($scope, $attrs, $http, $element) {
                    $scope.loadContent = function () {
                        $http.get($attrs.loadUrl)
                            .then(function (res) {
                                $element.html(res.data);
                            },
                            function (e) {
                                console.error("unable to load content from " + $attrs.loadUrl, e);
                            });
                    };
                    $scope.unloadContent = function () {
                        $element.html('');
                    };
                }
            };
        })
        .directive('loadOnSwap', function () {
            return {
                scope: true,
                restrict: 'A',
                controller: function ($scope, $attrs, $http, $element) {
                    $scope.$parent.$watch('swappedIn', function (swap) {
                        if (typeof ($attrs.loadUrl) === "undefined") {
                            console.error("no load-url defined for loading");
                        }
                        else {
                            if ($attrs.loadOnSwap && typeof (swap) !== "undefined") {
                                if (swap) $scope.loadContent();
                                else $scope.unloadContent();
                            }
                        }
                    });

                }
            }
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
        .directive('listeningRootKey', function () {
            return {
                scope: true,
                restrict: 'A',
                controller: function ($scope, $rootScope, $attrs) {
                    if (typeof ($attrs.listeningRootKey) !== "undefined") {

                        $rootScope.$watch("scopeAccess." + $scope.$eval($attrs.listeningRootKey), function (val) {
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
                                    if (typeof ($attrs.onSuccessEdit) !== "undefined"
                                                            && typeof ($attrs.onSuccessEditIndex) !== "undefined") {
                                        if (typeof ($attrs.onSuccessEditExternal) !== "undefined") {
                                            // external scope
                                            $rootScope.scopeAccess[$attrs.onSuccessEditExternal] = {
                                                action: "edit",
                                                scopeKey: $attrs.onSuccessEdit,
                                                index: $attrs.onSuccessEditIndex,
                                                data: res.data
                                            };
                                        }
                                        else {
                                            actionToScope($scope, $attrs.onSuccessEdit, {
                                                action: "edit",
                                                index: $attrs.onSuccessEditIndex,
                                                data: res.data
                                            });
                                        }
                                    }
                                    $element[0].reset();
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
            //if (typeof ($scope[scopeKey]) === "undefined")
            //    $scope[scopeKey] = [];
            //$scope[scopeKey].push(meta.data);
            fetchObj($scope, scopeKey).push(meta.data);
        }
        else if (meta.action === "set") {
            $scope[scopeKey] = meta.data;
        }
        else if (meta.action === "edit") {
            $scope[scopeKey][meta.index] = meta.data;
        }
    }

    function fetchObj(obj, key) {
        var curr = obj;
        key.split(".").forEach(function (k) {
            if (typeof (curr) === "undefined") return;
            curr = curr[k];
        });
        return curr;
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