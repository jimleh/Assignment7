(function () {
    var app = angular.module("myApp", []);

    var myController = function ($scope, $http, $compile) {

        var GetData = function () {
            $http.get("../api/person/get")
                    .then(function (response) {
                        $scope.persons = response.data;
                        GetCities();
                    });
        };

        var GetCities = function () {
            var types = $scope.persons.filter(function (person) {
                if (!$scope.cities.includes(person.City) && person.City && person.City != "") {
                    $scope.cities.push(person.City);
                };
            });
        };

        $scope.persons = [];
        $scope.cities = [];
        $scope.occupations = [];
        $scope.sorter = "+ID";

        $scope.sortData = function (sorter) {
            if ($scope.sorter.includes("+")) {
                $scope.sorter = "-" + sorter;
            }
            else {
                $scope.sorter = "+" + sorter;
            }

            console.log("Sorting by: " + $scope.sorter);
        };

        $scope.getData = function () {
            GetData();
            console.log("Sorting by: " + $scope.sorter);
        };

        $scope.deleteData = function (id) {
            $http.delete("../api/person/delete/" + id)
                .then(function (response) {
                    GetData();
                    console.log(response.data);
                })
        };

        $scope.startEdit = function (person) {

            var input = document.getElementById("input");

            if (!$scope.edit) {

                $scope.edit = true;
                $scope.Person = person;

                input.classList.add("alert-success");
                document.getElementById("submitBtn").value = "Edit!";
                var div = angular.element(input.children.item(0));
                div.append(
                        $compile('<input id="abortBtn" ng-click="startEdit()" class="btn btn-warning" type="button" value="Abort!"/>')($scope)
                 );
            }
            else {
                $scope.edit = false;
                $scope.Person = {};
                GetCities();
                document.getElementById("submitBtn").value = "Add!";
                var btn = document.getElementById("abortBtn");
                btn.parentNode.removeChild(btn);
                input.classList.remove("alert-success");
            }

        };

        $scope.handleInput = function () {
            if ($scope.edit) {
                $http.put("../api/person/put/" + $scope.Person.ID, $scope.Person)
                .then(function (response) {
                    console.log(response.data);
                    $scope.startEdit();
                });
            }
            else {
                var length = $scope.persons.length;
                for (var i = 0; i < length; i++) {
                    if (!$scope.Person.ID || $scope.persons[i].ID >= $scope.Person.ID) {
                        $scope.Person.ID = $scope.persons[i].ID + 1;
                    }
                };
                $http.post("../api/person/post", $scope.Person)
                    .then(function () {
                        GetData();

                        console.log($scope.Person.Name + " was added successfully!");
                    });
            }
        };
    };

    app.controller("myController", ["$scope", "$http", "$compile", myController]);
}());