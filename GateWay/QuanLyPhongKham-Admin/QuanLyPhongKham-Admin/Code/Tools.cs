using System.IO;
using System.Text;


namespace QuanLyPhongKham_Admin.Code
{
    public class Tools : ITools
    {
        private readonly IWebHostEnvironment _env;

        public Tools(IWebHostEnvironment env)
        {
            _env = env;
        }

        public string CreatePathFile(string filePath)
        {
            string path = Path.Combine(_env.ContentRootPath, filePath);
            string? directory = Path.GetDirectoryName(path);

            if (!Directory.Exists(directory))
                Directory.CreateDirectory(directory!);

            return path;
        }

        public string GenerateRandomCode(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            var random = new Random();
            var result = new StringBuilder(length);
            for (int i = 0; i < length; i++)
                result.Append(chars[random.Next(chars.Length)]);
            return result.ToString();
        }
    }
}
