namespace LichHenService.Models
{
    public class NhacLich
    {
        public int maNhacLich { get; set; }
        public int maLichHen { get; set; }
        public DateTime ngayGioGui { get; set; }
        public string? hinhThuc { get; set; }
        public string? ghiChu { get; set; }

        public string? tenBenhNhan { get; set; }
        public string? soDienThoai { get; set; }
        public string? email { get; set; }
        public DateTime ngayHen { get; set; }
        public TimeSpan gioHen { get; set; }

    }
}
