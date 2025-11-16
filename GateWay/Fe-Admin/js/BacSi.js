var _user = JSON.parse(localStorage.getItem("user"));
var app = angular.module('AppPhongKham', []);

app.controller("BacSiCtrl", function ($scope, $http) {
    // Thông tin phân trang và biến toàn cục
    $scope.page = 1;
    $scope.pageSize = 50;
    $scope.listBacSi = [];
    $scope.submit = "Thêm mới";

    // 🧠 Hàm tải danh sách Bác sĩ
    $scope.LoadBacSi = function () {
        $http({
            method: 'POST',
            headers: { "Authorization": 'Bearer ' + _user.Token },
            data: { page: $scope.page, pageSize: $scope.pageSize },
            url: current_url + '/api-admin/bacsi/search',
        }).then(function (res) {
            $scope.listBacSi = res.data.data;
        }, function (err) {
            console.error("Lỗi khi tải danh sách bác sĩ:", err);
            alert("Không thể tải danh sách bác sĩ!");
        });
    };

    // 🧠 Hàm lưu thông tin Bác sĩ (Thêm mới hoặc Cập nhật)
    $scope.Save = function () {
        let obj = {};
        obj.maBacSi = $scope.MaBacSi;
        obj.maNguoiDung = Number($scope.MaNguoiDung);
        obj.chuyenKhoa = $scope.ChuyenKhoa;
        obj.bangCap = $scope.BangCap;
        obj.kinhNghiem = $scope.KinhNghiem;
        obj.soPhong = $scope.SoPhong;
        obj.trangThai = $scope.TrangThai == true || $scope.TrangThai == "true";

        if ($scope.submit == "Thêm mới") {
            $http({
                method: 'POST',
                headers: { "Authorization": 'Bearer ' + _user.Token },
                data: obj,
                url: current_url + '/api-admin/bacsi/create-bacsi',
            }).then(function (res) {
                alert("Thêm bác sĩ thành công!");
                $scope.LoadBacSi();
            }, function (err) {
                console.error("Lỗi khi thêm bác sĩ:", err);
                alert("Không thể thêm bác sĩ!");
            });
        } else {
            $http({
                method: 'POST',
                headers: { "Authorization": 'Bearer ' + _user.Token },
                data: obj,
                url: current_url + '/api-admin/bacsi/update-bacsi',
            }).then(function (res) {
                alert("Cập nhật thông tin bác sĩ thành công!");
                $scope.LoadBacSi();
            }, function (err) {
                console.error("Lỗi khi cập nhật bác sĩ:", err);
                alert("Không thể cập nhật bác sĩ!");
            });
        }
    };

    // 🧠 Hàm sửa Bác sĩ (load dữ liệu lên form)
    $scope.Sua = function (id) {
        $scope.submit = "Lưu lại";
        $http({
            method: 'GET',
            headers: { "Authorization": 'Bearer ' + _user.Token },
            url: current_url + '/api-admin/bacsi/get-by-id/' + id,
        }).then(function (res) {
            let x = res.data;
            $scope.MaBacSi = x.maBacSi;
            $scope.MaNguoiDung = x.maNguoiDung;
            $scope.ChuyenKhoa = x.chuyenKhoa;
            $scope.BangCap = x.bangCap;
            $scope.KinhNghiem = x.kinhNghiem;
            $scope.SoPhong = x.soPhong;
            $scope.TrangThai = x.trangThai;
        }, function (err) {
            console.error("Lỗi khi tải bác sĩ:", err);
            alert("Không thể tải thông tin bác sĩ!");
        });
    };

    // 🧠 Hàm xóa Bác sĩ
    $scope.Xoa = function (id) {
        var result = confirm("Bạn có chắc muốn xóa bác sĩ mã " + id + " ?");
        if (result) {
            $http({
                method: 'GET',
                headers: { "Authorization": 'Bearer ' + _user.Token },
                url: current_url + '/api-admin/bacsi/delete-bacsi/' + id,
            }).then(function (res) {
                alert("Xóa bác sĩ thành công!");
                $scope.LoadBacSi();
            }, function (err) {
                console.error("Lỗi khi xóa bác sĩ:", err);
                alert("Không thể xóa bác sĩ!");
            });
        }
    };

    // ✅ Gọi tự động khi khởi động trang
    $scope.LoadBacSi();
});
