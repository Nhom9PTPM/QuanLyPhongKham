namespace QuanLyPhongKham_Gateway.DTO
{
    public class DonThuocUpdateDto
    {
        public int MaDonThuoc { get; set; }
        public string? GhiChu { get; set; }
        public string NguoiSua { get; set; } = string.Empty;
        public List<ChiTietDonThuocDto1> ChiTietJSON { get; set; } = new();
    }
    public class ChiTietDonThuocDto1
    {
        public int MaThuoc { get; set; }
        public int SoLuong { get; set; }
        public string? CachDung { get; set; }
        public decimal DonGia { get; set; }
    }
}
