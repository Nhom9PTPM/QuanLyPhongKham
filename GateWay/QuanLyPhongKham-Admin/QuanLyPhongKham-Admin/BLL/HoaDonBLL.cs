using QuanLyPhongKham_Admin.DAL;
using QuanLyPhongKham_Admin.Models;
using QuanLyPhongKham_Admin.Models.DTOs;

namespace QuanLyPhongKham_Admin.BLL
{
    public class HoaDonBLL
    {
        private readonly HoaDonDAL _hoaDonDAL;

        public HoaDonBLL(HoaDonDAL hoaDonDAL)
        {
            _hoaDonDAL = hoaDonDAL;
        }

        // ======================================================
        // 6.1 - CÁC CHỨC NĂNG CRUD CƠ BẢN
        // ======================================================

        public async Task<List<HoaDon>> LayTatCa()
        {
            return await _hoaDonDAL.GetAllAsync();
        }

        public async Task<HoaDon?> LayChiTiet(int id)
        {
            return await _hoaDonDAL.GetByIdAsync(id);
        }

        public async Task Them(HoaDon hoaDon)
        {
            hoaDon.TrangThai ??= "Chưa thanh toán";
            hoaDon.NgayLap = DateTime.Now;
            await _hoaDonDAL.AddAsync(hoaDon);
        }

        public async Task CapNhat(HoaDon hoaDon)
        {
            await _hoaDonDAL.UpdateAsync(hoaDon);
        }

        public async Task Xoa(int id)
        {
            await _hoaDonDAL.DeleteAsync(id);
        }


        // ======================================================
        // 6.2 - THU PHÍ & CẬP NHẬT TRẠNG THÁI THANH TOÁN
        // ======================================================

        /// <summary>
        /// 6.2A - Lấy danh sách hóa đơn chưa thanh toán
        /// </summary>
        public async Task<List<HoaDon>> LayHoaDonChuaThanhToan()
        {
            return await _hoaDonDAL.GetChuaThanhToanAsync();
        }

        /// <summary>
        /// 6.2B - Thực hiện thanh toán cho hóa đơn
        /// </summary>
        public async Task<(bool Success, string Message, object? Data)> ThanhToanHoaDonAsync(ThanhToanRequest request)
        {
            // 1️⃣ Kiểm tra tồn tại
            var hoaDon = await _hoaDonDAL.GetByIdAsync(request.MaHoaDon);
            if (hoaDon == null)
                return (false, "Không tìm thấy hóa đơn.", null);

            // 2️⃣ Kiểm tra trạng thái
            if (hoaDon.TrangThai == "Đã thanh toán" || hoaDon.TrangThai == "Da thanh toan")
                return (false, "Hóa đơn này đã được thanh toán trước đó.", null);

            // 3️⃣ Kiểm tra dữ liệu hợp lệ
            if (hoaDon.TongTien <= 0)
                return (false, "Tổng tiền không hợp lệ để thu phí.", null);

            if (string.IsNullOrWhiteSpace(request.PhuongThucThanhToan))
                return (false, "Vui lòng chọn phương thức thanh toán.", null);

            // 4️⃣ Cập nhật dữ liệu
            hoaDon.TrangThai = "Đã thanh toán";
            hoaDon.PhuongThucThanhToan = request.PhuongThucThanhToan;
            hoaDon.MaNguoiThu = request.MaNguoiThu;
            hoaDon.NgayLap = DateTime.Now;
            hoaDon.GhiChu = request.GhiChu;

            // 5️⃣ Ghi thay đổi vào DB
            await _hoaDonDAL.UpdateAsync(hoaDon);

            // 6️⃣ Chuẩn bị dữ liệu phản hồi
            var data = new
            {
                hoaDon.MaHoaDon,
                hoaDon.TrangThai,
                hoaDon.PhuongThucThanhToan,
                hoaDon.TongTien,
                hoaDon.NgayLap
            };

            return (true, "Thanh toán thành công.", data);
        }
    }
}
