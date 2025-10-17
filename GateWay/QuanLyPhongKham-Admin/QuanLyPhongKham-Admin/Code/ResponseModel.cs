namespace QuanLyPhongKham_Admin.Code
{
    public class ResponseModel
    {
        public int TotalItems { get; set; }
        public int Page { get; set; }
        public int PageSize { get; set; }
        public object? Data { get; set; }
    }
}
