
var netcore_angular_formDefaults = {
    // all the following also use: $scope, $attrs, $http, $element, $rootScope 
    onSuccess : function (result) { },
    onFail : function (result) { }, // includes both failures below
    onClientFail : function () { }, // client side failure
    onServerFail : function (result) { } // server side failure
};

(function () {

    var formDefaults = netcore_angular_formDefaults;

    angular.module('netcore-angular', [])
        .run(function ($rootScope) {
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
                            if (typeof ($attrs.loadOnSwap) !== "undefined" && $attrs.loadOnSwap === "true") {
                                $scope.loadContent();
                            }
                        }
                        else {
                            $scope.swappedIn = false;
                            $element.hide()
                            if (typeof ($attrs.loadOnSwap) !== "undefined" && $attrs.loadOnSwap === "true") {
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
                controller: function ($scope, $attrs, $http, $element, $rootScope) {
                    $scope.$parent.$watch('swappedIn', function (swap) {
                        if (typeof ($attrs.loadUrl) === "undefined") {
                            console.error("no load-url defined for loading");
                        }
                        else {
                            if ($attrs.loadOnSwap === "true" && typeof (swap) !== "undefined") {
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

                        $rootScope.$watch("scopeAccess." + $attrs.listeningRootKey, function (val) {
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
                                    formDefaults.onSuccess(res, $scope, $http, $attrs, $element, $rootScope);
                                    if (typeof ($attrs.onSuccess) !== "undefined") {
                                        if (typeof ($scope[$attrs.onSuccess]) === "function") {
                                            $scope[$attrs.onSuccess](res);
                                        }
                                        else console.error("No function found in scope with the name " + $attrs.onSuccess);
                                    }
                                    if (typeof ($attrs.onSuccessSwap) !== "undefined"
                                                    && $attrs.onSuccessSwap === "true") {
                                        swapScope = $scope;
                                        while (typeof (swapScope) !== "undefined" && typeof (swapScope.swap) !== "function")
                                            swapScope = $scope.$parent;
                                        if (typeof (swapScope) !== "undefined") swapScope.swap();
                                    }
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
                                    $element.find("input:not([type=button],[type=submit],[type=hidden])").val("");
                                    $element.find("[data-valmsg-for]").text("");
                                }, function (res) {
                                    if (typeof ($attrs.onFailureLoadResult) !== "undefined"
                                                        && $attrs.onFailureLoadResult === "true")
                                        $element.html(res.data);
                                    formDefaults.onServerFail(res, $scope, $attrs, $http, $element, $rootScope);
                                    formDefaults.onFail(res, $scope, $attrs, $http, $element, $rootScope );
                                });
                        }
                        else {
                            formDefaults.onClientFail($scope, $attrs, $http, $element, $rootScope );
                            formDefaults.onFail(null, $scope, $attrs, $http, $element, $rootScope );
                        }
                    });
                }
            }
        });

    function actionToScope($scope, scopeKey, meta) {
        if (meta.action === "push") {
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
        var arr = key.split(".");
        curr = obj;
        for (var i = 0; i < arr.length - 1; i++) {
            if (typeof (curr[arr[i]]) === "undefined" || curr[arr[i]] === null) {
                curr[arr[i]] = {};
            }
            curr = curr[arr[i]];
        }
        if (typeof (curr[arr[arr.length - 1]]) === "undefined" || curr[arr[arr.length - 1]] === null) {
            curr[arr[arr.length - 1]] = [];
        }
        return curr[arr[arr.length - 1]];
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