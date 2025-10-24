using QuanLyPhongKham_Admin.Models;

namespace QuanLyPhongKham_Admin.Code
{
    public class HoSoBenhAnModels
    {
        public HoSoBenhAn hosobenhan { get; set; }
        public List<TapTin> listchitiet { get; set; }
    }

    public class HoSoBenhAnEditModels
    {
        public HoSoBenhAn hosobenhan { get; set; }
        public List<TapTinEdit> listchitiet { get; set; }
    }

    public class TapTinEdit
    {
        public int MaTapTin { get; set; }
        public int? MaHoSo { get; set; }
        public string? TenTapTin { get; set; }
        public string? DuongDan { get; set; }
        public long? KichThuoc { get; set; }
        public string? DinhDang { get; set; }
        public int TrangThai { get; set; } 
    }
}
