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

        //  Hàm tổng hợp
        public async Task<object?> LayThongTinDayDu(int maBenhNhan)
        {
            var bn = await _benhNhanDAL.GetByIdAsync(maBenhNhan);
            if (bn is null) return null;
            var hoSoList = await _benhNhanDAL.GetHoSoByBenhNhanId(maBenhNhan);

            var ketQua = new
            {
                BenhNhan = new
                {
                    bn.MaBenhNhan,
                    bn.HoTen,
                    bn.NgaySinh,
                    bn.GioiTinh,
                    bn.SoDienThoai,
                    bn.Email,
                    bn.DiaChi,
                    bn.CMTND_NV
                },
                HoSoBenhAn = hoSoList.Select(h => new
                {
                    h.MaHoSo,
                    h.NgayLap,
                    ChanDoan = h.ChanDoanChinh,
                    TomTat = h.TomTatBenhLy,
                    LichSuBenhLy = h.LichSuBenhLy,
                    NguoiLap = h.NguoiLap,

                    DanhSachKham = h.DanhSachKham?.Select(k => new
                    {
                        k.NgayKham,
                        k.ChanDoan,
                        k.GhiChu,
                        DonThuoc = k.DonThuoc?.Select(d => new
                        {
                            d.MaDonThuoc,
                            d.GhiChu,
                            d.NguoiKe
                        })
                    }),

                    TapTin = h.TapTin?.Select(t => new
                    {
                        t.TenTapTin,
                        t.DuongDan
                    })
                })
            };

            return ketQua;
        }
    }
}
