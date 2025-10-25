using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System.Data.Common;

namespace QuanLyPhongKham_Gateway.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        /// <summary>
        /// Thực thi stored procedure dạng thêm/sửa/xóa (không trả dữ liệu)
        /// </summary>
        public async Task ExecuteNonQueryAsync(SqlCommand cmd)
        {
            var connection = Database.GetDbConnection();

            cmd.Connection = (SqlConnection)connection;
            cmd.CommandType = CommandType.StoredProcedure;

            if (connection.State != ConnectionState.Open)
                await connection.OpenAsync();

            await cmd.ExecuteNonQueryAsync();
        }

        /// <summary>
        /// Thực thi stored procedure có trả dữ liệu (SqlDataReader)
        /// </summary>
        public async Task<SqlDataReader> ExecuteReaderAsync(SqlCommand cmd)
        {
            var connection = Database.GetDbConnection();

            cmd.Connection = (SqlConnection)connection;
            cmd.CommandType = CommandType.StoredProcedure;

            if (connection.State != ConnectionState.Open)
                await connection.OpenAsync();

            // Close connection khi reader bị dispose
            return await cmd.ExecuteReaderAsync(CommandBehavior.CloseConnection);
        }

    }
}
