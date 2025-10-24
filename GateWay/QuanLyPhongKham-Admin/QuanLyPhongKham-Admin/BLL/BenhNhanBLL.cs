using QuanLyPhongKham_Admin.DAL;
using QuanLyPhongKham_Admin.Models;

namespace QuanLyPhongKham_Admin.BLL
{
    public class BenhNhanBLL
    {
        private readonly BenhNhanDAL _benhNhanDAL;

        public BenhNhanBLL(BenhNhanDAL benhNhanDAL)
        {
            _benhNhanDAL = benhNhanDAL;
        }

        public async Task<List<BenhNhan>> LayDanhSach()
        {
            return await _benhNhanDAL.GetAllAsync();
        }

        public async Task<BenhNhan?> LayChiTiet(int id)
        {
            return await _benhNhanDAL.GetByIdAsync(id);
        }

        public async Task Them(BenhNhan bn)
        {
            if (string.IsNullOrWhiteSpace(bn.HoTen))
                throw new Exception("Tên bệnh nhân không được để trống.");

            await _benhNhanDAL.AddAsync(bn);
        }

        public async Task CapNhat(BenhNhan bn)
        {
            await _benhNhanDAL.UpdateAsync(bn);
        }

        public async Task Xoa(int id)
        {
            await _benhNhanDAL.DeleteAsync(id);
        }
    }
}
