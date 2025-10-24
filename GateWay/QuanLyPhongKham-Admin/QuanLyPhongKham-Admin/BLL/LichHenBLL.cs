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
    }
}
