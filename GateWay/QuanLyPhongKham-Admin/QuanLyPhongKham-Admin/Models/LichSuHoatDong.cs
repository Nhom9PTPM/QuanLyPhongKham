using System;
using System.ComponentModel.DataAnnotations;

namespace QuanLyPhongKham_Admin.Models
{
    public class LichSuHoatDong
    {
        [Key]
        public int MaLichSu { get; set; }

        public int? MaTaiKhoan { get; set; }        // Ai thực hiện (nếu có đăng nhập)
        public string HanhDong { get; set; }        // Mô tả hành động: "Thêm bệnh nhân", "Xóa lịch hẹn", ...
        public DateTime DongThoiGian { get; set; }  // Thời gian ghi log
        public string? DiaChiIP { get; set; }       // IP máy gọi API
        public string? ChiTiet { get; set; }        // Nội dung chi tiết JSON nếu cần
    }
}
