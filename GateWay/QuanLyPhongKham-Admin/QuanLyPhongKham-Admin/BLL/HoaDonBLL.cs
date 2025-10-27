using QuanLyPhongKham_Admin.DAL;
using QuanLyPhongKham_Admin.Models;

namespace QuanLyPhongKham_Admin.BLL
{
    public class HoaDonBLL
    {
        private readonly HoaDonDAL _hoaDonDAL;

        public HoaDonBLL(HoaDonDAL hoaDonDAL)
        {
            _hoaDonDAL = hoaDonDAL;
        }

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
    }
}
