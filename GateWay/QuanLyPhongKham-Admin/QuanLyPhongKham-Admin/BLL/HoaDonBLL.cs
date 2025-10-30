using QuanLyPhongKham_Admin.DAL;
using QuanLyPhongKham_Admin.Models;
using QuanLyPhongKham_Admin.Models.DTOs;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.IO;

namespace QuanLyPhongKham_Admin.BLL
{
    public class HoaDonBLL
    {
        private readonly HoaDonDAL _hoaDonDAL;

        public HoaDonBLL(HoaDonDAL hoaDonDAL)
        {
            _hoaDonDAL = hoaDonDAL;
        }

        // ======================================================
        // 6.1 - CÁC CHỨC NĂNG CRUD CƠ BẢN
        // ======================================================

        public async Task<List<HoaDon>> LayTatCa()
        {
            return await _hoaDonDAL.GetAllAsync();
        }

        public async Task<HoaDon?> LayChiTiet(int id)
        {
            return await _hoaDonDAL.GetByIdAsync(id);
        }

        public async Task Them(HoaDon hoaDon)
        {
            hoaDon.TrangThai ??= "Chưa thanh toán";
            hoaDon.NgayLap = DateTime.Now;
            await _hoaDonDAL.AddAsync(hoaDon);
        }

        public async Task CapNhat(HoaDon hoaDon)
        {
            await _hoaDonDAL.UpdateAsync(hoaDon);
        }

        public async Task Xoa(int id)
        {
            await _hoaDonDAL.DeleteAsync(id);
        }


        // ======================================================
        // 6.2 - THU PHÍ & CẬP NHẬT TRẠNG THÁI THANH TOÁN
        // ======================================================

        public async Task<List<HoaDon>> LayHoaDonChuaThanhToan()
        {
            return await _hoaDonDAL.GetChuaThanhToanAsync();
        }

        public async Task<(bool Success, string Message, object? Data)> ThanhToanHoaDonAsync(ThanhToanRequest request)
        {
            var hoaDon = await _hoaDonDAL.GetByIdAsync(request.MaHoaDon);
            if (hoaDon == null)
                return (false, "Không tìm thấy hóa đơn.", null);

            if (hoaDon.TrangThai == "Đã thanh toán" || hoaDon.TrangThai == "Da thanh toan")
                return (false, "Hóa đơn này đã được thanh toán trước đó.", null);

            if (hoaDon.TongTien <= 0)
                return (false, "Tổng tiền không hợp lệ để thu phí.", null);

            if (string.IsNullOrWhiteSpace(request.PhuongThucThanhToan))
                return (false, "Vui lòng chọn phương thức thanh toán.", null);

            hoaDon.TrangThai = "Đã thanh toán";
            hoaDon.PhuongThucThanhToan = request.PhuongThucThanhToan;
            hoaDon.MaNguoiThu = request.MaNguoiThu;
            hoaDon.NgayLap = DateTime.Now;
            hoaDon.GhiChu = request.GhiChu;

            await _hoaDonDAL.UpdateAsync(hoaDon);

            var data = new
            {
                hoaDon.MaHoaDon,
                hoaDon.TrangThai,
                hoaDon.PhuongThucThanhToan,
                hoaDon.TongTien,
                hoaDon.NgayLap
            };

            return (true, "Thanh toán thành công.", data);
        }

        // ======================================================
        // 6.4 - XUẤT HÓA ĐƠN RA FILE PDF
        // ======================================================

        public async Task<byte[]> XuatHoaDonPdfAsync(int id)
        {
            var hoaDon = await _hoaDonDAL.GetHoaDonChiTietAsync(id);
            if (hoaDon == null)
                throw new Exception("Không tìm thấy hóa đơn.");

            using (var ms = new MemoryStream())
            {
                var document = new iTextSharp.text.Document(iTextSharp.text.PageSize.A4, 40f, 40f, 40f, 40f);
                PdfWriter.GetInstance(document, ms);
                document.Open();

                // ======= TIÊU ĐỀ =======
                var fontTitle = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 18);
                var fontNormal = FontFactory.GetFont(FontFactory.HELVETICA, 12);

                document.Add(new Paragraph("PHÒNG KHÁM ĐA KHOA", fontTitle) { Alignment = Element.ALIGN_CENTER });
                document.Add(new Paragraph("HÓA ĐƠN THANH TOÁN", fontTitle) { Alignment = Element.ALIGN_CENTER });
                document.Add(new Paragraph(" "));

                document.Add(new Paragraph($"Mã hóa đơn: {hoaDon.MaHoaDon}", fontNormal));
                document.Add(new Paragraph($"Ngày lập: {hoaDon.NgayLap:dd/MM/yyyy HH:mm}", fontNormal));
                document.Add(new Paragraph($"Bệnh nhân: {hoaDon.BenhNhan?.HoTen}", fontNormal));
                document.Add(new Paragraph($"SĐT: {hoaDon.BenhNhan?.SoDienThoai}", fontNormal));
                document.Add(new Paragraph($"Địa chỉ: {hoaDon.BenhNhan?.DiaChi}", fontNormal));
                document.Add(new Paragraph(" "));

                // ======= BẢNG CHI TIẾT =======
                PdfPTable table = new PdfPTable(4) { WidthPercentage = 100 };
                table.AddCell("Tên thuốc / Dịch vụ");
                table.AddCell("Số lượng");
                table.AddCell("Đơn giá");
                table.AddCell("Thành tiền");

                double tongTien = 0;
                if (hoaDon.ChiTietHoaDon != null)
                {
                    foreach (var item in hoaDon.ChiTietHoaDon)
                    {
                        var tenThuoc = item.Thuoc?.TenThuoc ?? "Khác";
                        var soLuong = item.SoLuong ?? 0;
                        var donGia = item.DonGia ?? 0; // ✅ Lấy từ ChiTietHoaDon
                        var thanhTien = item.ThanhTien ?? (soLuong * donGia);
                        tongTien += thanhTien;

                        table.AddCell(tenThuoc);
                        table.AddCell(soLuong.ToString());
                        table.AddCell($"{donGia:N0} đ");
                        table.AddCell($"{thanhTien:N0} đ");
                    }

                }

                document.Add(table);
                document.Add(new Paragraph(" "));
                document.Add(new Paragraph($"Tổng tiền: {tongTien:N0} đ", fontTitle));
                document.Add(new Paragraph($"Phương thức thanh toán: {hoaDon.PhuongThucThanhToan}", fontNormal));
                document.Add(new Paragraph($"Người thu: {hoaDon.MaNguoiThu}", fontNormal));
                document.Add(new Paragraph(" "));
                document.Add(new Paragraph("Cảm ơn quý khách đã sử dụng dịch vụ!", fontNormal)
                {
                    Alignment = Element.ALIGN_CENTER
                });

                document.Close();
                return ms.ToArray();
            }
        }
    }
}
