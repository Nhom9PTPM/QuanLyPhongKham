namespace QuanLyPhongKham_Admin.Models.DTOs
{
    /// <summary>
    /// Dữ liệu đầu vào cho API 6.3 - Báo cáo doanh thu theo bác sĩ/chuyên khoa
    /// </summary>
    public class BaoCaoDoanhThuRequest
    {
        public DateTime? TuNgay { get; set; }
        public DateTime? DenNgay { get; set; }
        public string? LoaiBaoCao { get; set; } // "BacSi" hoặc "ChuyenKhoa"
    }
}
