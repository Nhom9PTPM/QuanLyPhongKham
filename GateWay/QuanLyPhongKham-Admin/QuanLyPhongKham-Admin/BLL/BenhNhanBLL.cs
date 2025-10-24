using QuanLyPhongKham_Admin.DAL;
using QuanLyPhongKham_Admin.Models;
using QuanLyPhongKham_Admin.Common; // ✅ Thêm namespace để dùng Masking

namespace QuanLyPhongKham_Admin.BLL
{
    public class BenhNhanBLL
    {
        private readonly BenhNhanDAL _benhNhanDAL;

        public BenhNhanBLL(BenhNhanDAL benhNhanDAL)
        {
            _benhNhanDAL = benhNhanDAL;
        }

        // ✅ Áp dụng Masking
        public async Task<List<BenhNhan>> LayDanhSach()
        {
            var list = await _benhNhanDAL.GetAllAsync();

            // Ẩn dữ liệu nhạy cảm
            foreach (var bn in list)
            {
                bn.SoDienThoai = DataMaskHelper.MaskPhone(bn.SoDienThoai);
                bn.CMTND_NV = DataMaskHelper.MaskCMT(bn.CMTND_NV);
            }

            return list;
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
