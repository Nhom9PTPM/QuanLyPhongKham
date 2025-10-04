namespace LichHenService.Models
{
    public class LichHen
    {
        public int maLichHen { get; set; }
        public int maBenhNhan { get; set; }
        public int maBacSi { get; set; }
        public DateTime ngayHen { get; set; }
        public TimeSpan gioHen { get; set; }
        public string? trangThai { get; set; }
    }
}
