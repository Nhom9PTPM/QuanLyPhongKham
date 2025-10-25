namespace QuanLyPhongKham_Gateway.DTO
{
    public class ChiTietDonThuocDto
    {
        public int MaThuoc { get; set; }
        public int SoLuong { get; set; }
        public string CachDung { get; set; } = string.Empty;
        public double DonGia { get; set; }
    }

    public class DonThuocCreateDto
    {
        public int MaKham { get; set; }
        public int MaBacSi { get; set; }
        public string? GhiChu { get; set; }
        public string NguoiTao { get; set; } = string.Empty;
        public List<ChiTietDonThuocDto> ChiTiet { get; set; } = new();
    }
}
