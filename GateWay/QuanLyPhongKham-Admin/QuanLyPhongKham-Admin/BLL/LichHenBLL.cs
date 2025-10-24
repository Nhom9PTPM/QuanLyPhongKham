using QuanLyPhongKham_Admin.DAL;

namespace QuanLyPhongKham_Admin.BLL
{
    public class LichHenBLL
    {
        private readonly LichHenDAL _dal;

        public LichHenBLL(LichHenDAL dal)
        {
            _dal = dal;
        }

        public async Task<List<Models.LichHen>> LayDanhSach()
        {
            return await _dal.GetAllAsync();
        }

        public async Task<Models.LichHen?> LayChiTiet(int id)
        {
            return await _dal.GetByIdAsync(id);
        }

        // ✅ Liên kết lịch hẹn với hồ sơ bệnh án
        public async Task<bool> LienKetVoiHoSo(int maLichHen, int maHoSo)
        {
            return await _dal.CapNhatTrangThaiVaLienKetAsync(maLichHen, maHoSo);
        }

        // ✅ Gợi ý khung giờ còn trống trong ngày cho bác sĩ
        public async Task<List<string>> GoiYLichHen(int maBacSi, DateTime ngay)
        {
            // Lấy danh sách lịch hẹn đã có trong ngày
            var lichDaDat = await _dal.GetByBacSiVaNgay(maBacSi, ngay);
            var gioDaDat = lichDaDat.Select(l => l.GioHen).ToList();

            // Danh sách khung giờ chuẩn của phòng khám
            var khungGioChuan = new List<string>
            {
                "08:00", "08:30", "09:00", "09:30",
                "10:00", "10:30", "11:00",
                "13:30", "14:00", "14:30", "15:00", "15:30"
            };

            // Loại bỏ các khung giờ đã có lịch
            var gioTrong = khungGioChuan
                .Where(g => !gioDaDat.Contains(g))
                .ToList();

            return gioTrong;
        }
    }
}
