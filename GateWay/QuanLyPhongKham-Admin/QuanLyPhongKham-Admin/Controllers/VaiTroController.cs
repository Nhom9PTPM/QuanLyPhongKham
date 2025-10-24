using Microsoft.AspNetCore.Mvc;
using QuanLyPhongKham_Admin.Models;
using Microsoft.Extensions.Configuration;

namespace QuanLyPhongKham_Admin.Controllers
{
    public class VaiTroController : Controller
    {
        private QuanLyPhongKhamContext db = null;

        public VaiTroController(IConfiguration configuration)
        {
            db = new QuanLyPhongKhamContext(configuration);
        }

        public IActionResult Index()
        {
            var data = db.VaiTros.Where(x => x.DaXoa == false).ToList();
            return View(data);
        }

        public IActionResult Create()
        {
            return View(new VaiTro());
        }

        [HttpPost]
        public IActionResult Create(VaiTro model)
        {
            model.NgayTao = DateTime.Now;
            db.VaiTros.Add(model);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public IActionResult Edit(int id)
        {
            var vtro = db.VaiTros.SingleOrDefault(x => x.MaVaiTro == id);
            return View(vtro);
        }

        [HttpPost]
        public IActionResult Edit(VaiTro model)
        {
            db.VaiTros.Update(model);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public IActionResult Delete(int id)
        {
            var vtro = db.VaiTros.SingleOrDefault(x => x.MaVaiTro == id);
            vtro.DaXoa = true; // xóa mềm
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
