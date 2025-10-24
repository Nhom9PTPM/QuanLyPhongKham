using Microsoft.AspNetCore.Mvc;
using QuanLyPhongKham_Admin.BLL;

namespace QuanLyPhongKham_Admin.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuditLogController : ControllerBase
    {
        private readonly AuditLogBLL _bll;

        public AuditLogController(AuditLogBLL bll)
        {
            _bll = bll;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var data = await _bll.LayTatCa();
            return Ok(new { success = true, data });
        }
    }
}
