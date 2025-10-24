using QuanLyPhongKham_Admin.Models;

namespace QuanLyPhongKham_Admin.Code
{
    public class HoaDonModels
    {
        public HoaDon hoadon { get; set; }
        public List<ChiTietHoaDon> listchitiet { get; set; }
    }

    public class HoaDonEditModels
    {
        public HoaDon hoadon { get; set; }
        public List<ChiTietHoaDonEdit> listchitiet { get; set; }
    }

    public class ChiTietHoaDonEdit
    {
        public int MaChiTietHoaDon { get; set; }
        public int? MaHoaDon { get; set; }
        public int? MaThuoc { get; set; }
        public int? SoLuong { get; set; }
        public double? DonGia { get; set; }
        public int TrangThai { get; set; }
    }
}
