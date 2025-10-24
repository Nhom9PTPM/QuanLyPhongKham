using QuanLyPhongKham_Admin.Models;

namespace QuanLyPhongKham_Admin.Code
{
    public class BacSiModels
    {
        public BacSi bacsi { get; set; }
        public List<LichHen> lichhen { get; set; }
    }

    public class BacSiEditModels
    {
        public BacSi bacsi { get; set; }
        public List<LichHenEdit> lichhen { get; set; }
    }

    public class LichHenEdit
    {
        public int MaLichHen { get; set; }
        public int? MaBacSi { get; set; }
        public DateTime? NgayHen { get; set; }
        public int TrangThai { get; set; }
    }
}
