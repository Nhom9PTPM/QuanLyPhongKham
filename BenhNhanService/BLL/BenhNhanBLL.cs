using BenhNhanService.DAL;
using BenhNhanService.Models;


namespace BenhNhanService.BLL
{
    public class BenhNhanBLL
    {
        private readonly BenhNhanDAL _dal;
        public BenhNhanBLL(IConfiguration config)
        {
            _dal = new BenhNhanDAL(config);
        }

        public List<BenhNhan> GetAll() => _dal.GetAll();
        public void Insert(BenhNhan bn) => _dal.Insert(bn);
        public void Update(BenhNhan bn) => _dal.Update(bn);
        public void Delete(int id) => _dal.Delete(id);
    }
}
