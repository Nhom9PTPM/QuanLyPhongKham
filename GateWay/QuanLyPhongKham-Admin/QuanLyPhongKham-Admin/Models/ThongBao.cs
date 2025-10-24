namespace QuanLyPhongKham_Admin.Models
{
    public partial class ThongBao
    {
        public int MaThongBao { get; set; }
        public int? MaTaiKhoan { get; set; }
        public string TieuDe { get; set; } = null!;
        public string? NoiDung { get; set; }
        public bool DaDoc { get; set; }
        public DateTime NgayTao { get; set; }

        public virtual TaiKhoan? MaTaiKhoanNavigation { get; set; }
    }
}
