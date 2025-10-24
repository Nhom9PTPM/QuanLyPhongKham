using System;
using System.ComponentModel.DataAnnotations;

namespace QuanLyPhongKham_Admin.Models
{
    public class ChiTietDonThuoc
    {
        [Key]
        public int MaChiTietDon { get; set; }

        public int MaDonThuoc { get; set; }        // FK -> DonThuoc
        public int MaThuoc { get; set; }           // FK -> Thuoc
        public int SoLuong { get; set; }
        public string CachDung { get; set; }
        public double? DonGia { get; set; }
    }
}
