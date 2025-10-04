using LichHenService.DAL;
using LichHenService.Models;

namespace LichHenService.BLL
{
    public class NhacLichBLL
    {
        private readonly NhacLichDAL _dal;

        public NhacLichBLL(IConfiguration config)
        {
            _dal = new NhacLichDAL(config);
        }

        public List<NhacLich> GetAllWithDetail(DateTime? from = null, DateTime? to = null, int? maBenhNhan = null)
        {
            return _dal.GetAllWithDetail(from, to, maBenhNhan);
        }

        public void Create(NhacLich nhac)
        {
            nhac.ngayGioGui = DateTime.Now; 
            _dal.Insert(nhac);
        }
    }
}
