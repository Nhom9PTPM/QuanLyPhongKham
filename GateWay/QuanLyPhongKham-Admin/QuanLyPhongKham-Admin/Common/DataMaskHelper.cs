namespace QuanLyPhongKham_Admin.Common
{
    public static class DataMaskHelper
    {
        // ✅ Ẩn một phần số điện thoại: 0977555444 -> 0977****44
        public static string MaskPhone(string? soDienThoai)
        {
            if (string.IsNullOrEmpty(soDienThoai) || soDienThoai.Length < 4)
                return soDienThoai ?? "";

            return soDienThoai.Substring(0, 4) + "****" + soDienThoai.Substring(soDienThoai.Length - 2);
        }

        // ✅ Ẩn một phần số CMND: 123456789 -> ******789
        public static string MaskCMT(string? cmt)
        {
            if (string.IsNullOrEmpty(cmt) || cmt.Length < 3)
                return cmt ?? "";

            return new string('*', cmt.Length - 3) + cmt.Substring(cmt.Length - 3);
        }
    }
}
