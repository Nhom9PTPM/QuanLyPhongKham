using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace QuanLyPhongKham.Models
{
    [Table("DonThuoc")]
    public partial class DonThuoc
    {
        public DonThuoc()
        {
            ChiTietDonThuocs = new HashSet<ChiTietDonThuoc>();
        }

        public int MaDonThuoc { get; set; }
        public int? MaKham { get; set; }
        public int? MaBacSi { get; set; }
        public DateTime NgayKe { get; set; }
        public string? GhiChu { get; set; }
        public string? NguoiKe { get; set; }
        public bool DaXoa { get; set; }

        public virtual KhamBenh? MaKhamNavigation { get; set; }
        public virtual BacSi? MaBacSiNavigation { get; set; }

        public virtual ICollection<ChiTietDonThuoc> ChiTietDonThuocs { get; set; }
    }
}
