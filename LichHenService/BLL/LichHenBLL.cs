using LichHenService.DAL;
using LichHenService.Models;


namespace LichHenService.BLL
{
    public class LichHenBLL
    {
        private readonly LichHenDAL _dal;

        public LichHenBLL(IConfiguration config)
        {
            _dal = new LichHenDAL(config);
        }

        public List<object> GetAllWithDetails() => _dal.GetAllWithDetails();
        public void Insert(LichHen lh) => _dal.Insert(lh);
        public void Update(LichHen lh) => _dal.Update(lh);
        public void Delete(int id) => _dal.Delete(id);
    }
}
