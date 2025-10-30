using QuanLyPhongKham_Admin.DAL;
using QuanLyPhongKham_Admin.Models.DTOs;

namespace QuanLyPhongKham_Admin.BLL
{
    public class ThongKeBLL
    {
        private readonly ThongKeDAL _dal;

        public ThongKeBLL(ThongKeDAL dal)
        {
            _dal = dal;
        }

       
        // 6.3A - Thống kê tổng hợp (đã có)
        
        public async Task<object> LayThongKeTongHopAsync()
        {
            return await _dal.LayThongKeTongHopAsync();
        }

        
        // 6.3B - Báo cáo doanh thu theo bác sĩ hoặc chuyên khoa
        
        public async Task<List<dynamic>> BaoCaoDoanhThuAsync(BaoCaoDoanhThuRequest request)
        {
            // Gọi DAL và truyền bộ lọc (Từ ngày - Đến ngày - Loại báo cáo)
            return await _dal.BaoCaoDoanhThuAsync(
                request.TuNgay,
                request.DenNgay,
                request.LoaiBaoCao ?? "BacSi"
            );
        }
    }
}
