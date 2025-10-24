using QuanLyPhongKham_Admin.DAL;

namespace QuanLyPhongKham_Admin.BLL
{
    public class ThongKeBLL
    {
        private readonly ThongKeDAL _dal;

        public ThongKeBLL(ThongKeDAL dal)
        {
            _dal = dal;
        }

        public async Task<object> LayThongKeTongHopAsync()
        {
            return await _dal.LayThongKeTongHopAsync();
        }
    }
}
