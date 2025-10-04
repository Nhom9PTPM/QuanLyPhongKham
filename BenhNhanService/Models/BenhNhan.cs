using System;
using System.Collections.Generic;


namespace BenhNhanService.Models
{
    public class BenhNhan
    {
        public int maBenhNhan { get; set; }
        public string hoTen { get; set; }
        public DateTime? ngaySinh { get; set; }
        public string gioiTinh { get; set; }
        public string diaChi { get; set; }
        public string soDienThoai { get; set; }
        public string email { get; set; }
        public string tienSuBenh { get; set; }
    }
}
