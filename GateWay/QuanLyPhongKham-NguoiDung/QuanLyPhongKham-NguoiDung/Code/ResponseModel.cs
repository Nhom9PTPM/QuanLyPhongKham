namespace QuanLyPhongKham_NguoiDung.Code
{
    public class ResponseModel
    {
        public int Page { get; set; }
        public int PageSize { get; set; }
        public int TotalItems { get; set; }
        public object? Data { get; set; }

        public ResponseModel()
        {
            Page = 1;
            PageSize = 10;
            TotalItems = 0;
            Data = null;
        }
    }
}
