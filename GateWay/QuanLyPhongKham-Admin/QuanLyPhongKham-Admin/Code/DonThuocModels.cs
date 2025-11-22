using QuanLyPhongKham_Admin.Models;

namespace QuanLyPhongKham_Admin.Code
{
    public class DonThuocModels
    {
        public DonThuoc donthuoc { get; set; }
        public List<ChiTietDonThuoc> listchitiet { get; set; } = new List<ChiTietDonThuoc>();
    }

    public class DonThuocEditModels
    {
        public DonThuoc donthuoc { get; set; }
        public List<ChiTietDonThuoc> listchitiet { get; set; } = new List<ChiTietDonThuoc>();
    }

    public class ChiTietDonThuocEdit
    {
        public int MaChiTietDon { get; set; }
        public int? MaDonThuoc { get; set; }
        public int MaThuoc { get; set; }
        public int SoLuong { get; set; }
        public string? CachDung { get; set; }
        public double? DonGia { get; set; }
    }
}
