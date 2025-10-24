using System;
using System.ComponentModel.DataAnnotations;

namespace QuanLyPhongKham_Admin.Models
{
    public class DonThuoc
    {
        [Key]
        public int MaDonThuoc { get; set; }

        public int? MaKham { get; set; }           // FK -> KhamBenh
        public int? MaBacSi { get; set; }
        public DateTime NgayKe { get; set; }
        public string GhiChu { get; set; }
        public string NguoiKe { get; set; }
        public bool DaXoa { get; set; }
    }
}
