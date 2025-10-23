using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QuanLyPhongKham_Admin.Models;

namespace QuanLyPhongKham_Admin.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BacSiController : ControllerBase
    {
        private QuanLyPhongKhamContext db = null;

        public BacSiController(IConfiguration configuration)
        {
            db = new QuanLyPhongKhamContext(configuration);
        }

        
    }
}
