var user = JSON.parse(localStorage.getItem("user"));
var app = angular.module("AppPhongKham", []);

app.controller("DashboardCtrl", function ($scope, $http) {

    $scope.host = current_img;
    $scope.currentYear = new Date().getFullYear();

    // ✅ 1. Load tổng quan
    $scope.LoadTongQuan = function () {
        $http({
            method: 'GET',
            headers: { "Authorization": 'Bearer ' + user.Token },
            url: current_url + '/api-admin/dashboard/get-tong-quan'
        }).then(function (response) {
            $scope.TongQuan = response.data;
        });
    };

    // ✅ 2. Load doanh thu theo tháng
    $scope.LoadDoanhThuThang = function () {
        $http({
            method: 'GET',
            headers: { "Authorization": 'Bearer ' + user.Token },
            url: current_url + '/api-admin/dashboard/get-doanhthu-thang/' + $scope.currentYear
        }).then(function (response) {
            $scope.DoanhThuThang = response.data;
        });
    };

    // ✅ 3. Lượt khám theo tháng
    $scope.LoadLuotKham = function () {
        $http({
            method: 'GET',
            headers: { "Authorization": 'Bearer ' + user.Token },
            url: current_url + '/api-admin/dashboard/get-luotkham-thang/' + $scope.currentYear
        }).then(function (response) {
            $scope.LuotKhamThang = response.data;
        });
    };

    // ✅ 4. Doanh thu theo khoảng ngày
    $scope.LoadDoanhThuKhoang = function () {

        if (!$scope.tuNgay || !$scope.denNgay) return;

        $http({
            method: 'GET',
            headers: { "Authorization": 'Bearer ' + user.Token },
            url: current_url + `/api-admin/dashboard/get-doanhthu-theo-khoang?tuNgay=${$scope.tuNgay}&denNgay=${$scope.denNgay}`
        }).then(function (response) {
            $scope.DoanhThuKhoang = response.data;
        });
    };

    // ✅ Gọi hàm load khi vào trang
    $scope.LoadTongQuan();
    $scope.LoadDoanhThuThang();
    $scope.LoadLuotKham();

});
