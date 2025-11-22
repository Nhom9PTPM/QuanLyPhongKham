using QuanLyPhongKham_Admin.Models;

namespace QuanLyPhongKham_Admin.Code
{
    public class HoSoBenhAnPostModel
    {
        public int MaBenhNhan { get; set; }
        public string? TomTatBenhLy { get; set; }
        public string? ChanDoanChinh { get; set; }
        public string? LichSuBenhLy { get; set; }
        public DateTime NgayLap { get; set; }
        public string? NguoiLap { get; set; }
        public bool DaXoa { get; set; }
        public List<TapTinPostModel>? TapTins { get; set; }
    }

    public class TapTinPostModel
    {
        public string TenTapTin { get; set; }
        public string DuongDan { get; set; }
        public long KichThuoc { get; set; }
        public string DinhDang { get; set; }
    }

    // ViewModel cho hiển thị / edit
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
