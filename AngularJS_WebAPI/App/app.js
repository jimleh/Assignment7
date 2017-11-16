(function () {
    var app = angular.module("myApp", []);

    var myController = function ($scope, $http) {

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
            if($scope.sorter.includes("+"))
            {
                $scope.sorter = "-" + sorter;
            }
            else
            {
                $scope.sorter = "+" + sorter;
            }

            console.log("Sorting by: " + $scope.sorter);
        };

        $scope.getData = function () {
            GetData();
            console.log("Sorting by: " + $scope.sorter);
        };

        $scope.sendData = function () {
            for (var i = 0; i < $scope.persons.length; i++)
            {
                if(!$scope.Person.ID || $scope.persons[i].ID >= $scope.Person.ID)
                {
                    $scope.Person.ID = $scope.persons[i].ID + 1;
                }
            }
            $http.post("../api/person/post", $scope.Person)
                .then(function () {
                    GetData();

                    console.log($scope.Person.Name + " was added successfully!");
                });
        };

        $scope.deleteData = function (id) {
            $http.delete("../api/person/delete/" + id)
                .then(function () {
                    GetData();
                    console.log("Delete successful!");
                })
        };

        $scope.startEdit = function (person) {
            $scope.edit = true;
            $scope.Person = person;
        };

        $scope.editData = function () {
            $http.put("../api/person/put/" + $scope.Person.ID, $scope.Person)
                .then(function () {
                    console.log($scope.Person.Name + " was edited successfully!");
                    $scope.edit = false;
                    $scope.Person = {};
                    GetCities();
                });
        };
    };

    app.controller("myController", ["$scope", "$http", myController]);
}());