using Microsoft.EntityFrameworkCore;
using QuanLyPhongKham_Admin.DAL;
using QuanLyPhongKham_Admin.BLL; // nhớ thêm dòng này

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddDbContext<QuanLyPhongKhamContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))
           .EnableSensitiveDataLogging() // 👈 thêm dòng này
           .LogTo(Console.WriteLine, LogLevel.Information));


// Đăng ký các lớp DAL & BLL
builder.Services.AddScoped<BenhNhanDAL>();
builder.Services.AddScoped<BenhNhanBLL>();
builder.Services.AddScoped<KhamBenhDAL>();
builder.Services.AddScoped<KhamBenhBLL>();
builder.Services.AddScoped<LichHenDAL>();
builder.Services.AddScoped<LichHenBLL>();


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
