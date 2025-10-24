using Microsoft.EntityFrameworkCore;
using QuanLyPhongKham_Admin.DAL;
using QuanLyPhongKham_Admin.BLL; // nhớ thêm dòng này

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

// 🔗 Kết nối database
builder.Services.AddDbContext<QuanLyPhongKhamContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Đăng ký các lớp DAL & BLL
builder.Services.AddScoped<BenhNhanDAL>();
builder.Services.AddScoped<BenhNhanBLL>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
