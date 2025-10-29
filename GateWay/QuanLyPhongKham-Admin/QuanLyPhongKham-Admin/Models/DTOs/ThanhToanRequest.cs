namespace QuanLyPhongKham_Admin.Models.DTOs
{
    /// <summary>
    /// Dùng cho API 6.2 - Thu phí & cập nhật trạng thái thanh toán
    /// </summary>
    public class ThanhToanRequest
    {
        /// <summary>
        /// Mã hóa đơn cần thanh toán
        /// </summary>
        public int MaHoaDon { get; set; }

        /// <summary>
        /// Phương thức thanh toán (VD: Tiền mặt, Chuyển khoản, QR)
        /// </summary>
        public string PhuongThucThanhToan { get; set; } = string.Empty;

        /// <summary>
        /// Mã người thu (tài khoản nhân viên thu ngân)
        /// </summary>
        public int MaNguoiThu { get; set; }

        /// <summary>
        /// Ghi chú thêm (nếu có)
        /// </summary>
        public string? GhiChu { get; set; }
    }
}
