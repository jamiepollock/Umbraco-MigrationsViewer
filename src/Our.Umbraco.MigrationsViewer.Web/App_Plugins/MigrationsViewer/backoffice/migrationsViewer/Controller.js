angular.module("umbraco").controller("Our.Umbraco.MigrationsViewer.Controller", function ($scope, $http, $routeParams) {
    $scope.product = $routeParams.id;
    $scope.sortOptions = {
        propertyName: "CreateDate",
        direction: "Descending"
    };

    $scope.refreshData = function () {
        $http.get("/umbraco/backoffice/api/MigrationsViewerApi/Get",
            {
                params: {
                    "productName": $routeParams.id,
                    "orderByPropertyName": $scope.sortOptions.propertyName,
                    "orderByDirection": $scope.sortOptions.direction
                }
            })
            .then(function (response) {
                $scope.items = response.data;
            });
    };

    $scope.sort = function (field) {
        $scope.sortOptions.propertyName = field;

        if ($scope.sortOptions.direction === "Descending") {
            $scope.sortOptions.direction = "Ascending";
        }
        else {
            $scope.sortOptions.direction = "Descending";
        }

        $scope.refreshData();
    };
    $scope.isSortDirection = function (col, direction) {
        return $scope.sortOptions.propertyName.toUpperCase() == col.toUpperCase() && $scope.sortOptions.direction == direction;
    };

    $scope.refreshData();
});