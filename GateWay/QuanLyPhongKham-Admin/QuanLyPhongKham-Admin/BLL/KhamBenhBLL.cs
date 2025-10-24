using QuanLyPhongKham_Admin.DAL;
using QuanLyPhongKham_Admin.Models;

namespace QuanLyPhongKham_Admin.BLL
{
    public class KhamBenhBLL
    {
        private readonly KhamBenhDAL _dal;

        public KhamBenhBLL(KhamBenhDAL dal)
        {
            _dal = dal;
        }

        public async Task<List<KhamBenh>> LayDanhSach()
        {
            return await _dal.GetAllAsync();
        }

        public async Task<KhamBenh?> LayChiTiet(int id)
        {
            return await _dal.GetByIdAsync(id);
        }

        public async Task Them(KhamBenh kb)
        {
            if (kb.MaBenhNhan <= 0)
                throw new Exception("Phải chọn bệnh nhân.");

            await _dal.AddAsync(kb);
        }

        public async Task CapNhat(KhamBenh kb)
        {
            await _dal.UpdateAsync(kb);
        }

        public async Task Xoa(int id)
        {
            await _dal.DeleteAsync(id);
        }
    }
}
