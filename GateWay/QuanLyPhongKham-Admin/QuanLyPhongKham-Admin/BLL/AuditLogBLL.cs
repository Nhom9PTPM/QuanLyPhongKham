using QuanLyPhongKham_Admin.DAL;
using QuanLyPhongKham_Admin.Models;

namespace QuanLyPhongKham_Admin.BLL
{
    public class AuditLogBLL
    {
        private readonly AuditLogDAL _dal;

        public AuditLogBLL(AuditLogDAL dal)
        {
            _dal = dal;
        }

        public async Task GhiLichSu(string hanhDong, string? chiTiet, string? ip = null, int? maTaiKhoan = null)
        {
            var log = new LichSuHoatDong
            {
                MaTaiKhoan = maTaiKhoan,
                HanhDong = hanhDong,
                ChiTiet = chiTiet,
                DiaChiIP = ip
            };
            await _dal.GhiLogAsync(log);
        }

        public async Task<List<LichSuHoatDong>> LayTatCa()
        {
            return await _dal.LayTatCaAsync();
        }
    }
}
