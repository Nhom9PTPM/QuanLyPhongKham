using QuanLyPhongKham_Admin.Models;

namespace QuanLyPhongKham_Admin.Code
{
    public class TonKhoThuocModels
    {
        public TonKhoThuoc tonkho { get; set; }
    }

    public class TonKhoThuocEditModels
    {
        public int MaTonKho { get; set; }
        public int? SoLuong { get; set; }
        public string? MaKho { get; set; }
    }
}
