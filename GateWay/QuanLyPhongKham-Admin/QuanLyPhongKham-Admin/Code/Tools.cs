using System.IO;


namespace QuanLyPhongKham_Admin.Code
{
    public class Tools : ITools
    {
        private readonly IWebHostEnvironment _env;

        public Tools(IWebHostEnvironment env)
        {
            _env = env;
        }

        public string CreatePathFile(string relativePath)
        {
            var fullPath = Path.Combine(_env.WebRootPath ?? Path.Combine(Directory.GetCurrentDirectory(), "wwwroot"), relativePath);
            var dir = Path.GetDirectoryName(fullPath);
            if (!Directory.Exists(dir))
            {
                Directory.CreateDirectory(dir);
            }
            return fullPath;
        }

        public bool DeleteFile(string relativePath)
        {
            try
            {
                var fullPath = Path.Combine(_env.WebRootPath ?? Path.Combine(Directory.GetCurrentDirectory(), "wwwroot"), relativePath);
                if (File.Exists(fullPath))
                {
                    File.Delete(fullPath);
                    return true;
                }
                return false;
            }
            catch
            {
                return false;
            }
        }
    }
}
