using Microsoft.EntityFrameworkCore;
using QuanLyPhongKham_Admin.DAL;
using QuanLyPhongKham_Admin.BLL; 

var builder = WebApplication.CreateBuilder(args);

//  Cấu hình Controllers + IgnoreCycles chỉ gọi 1 lần
builder.Services.AddControllers()
    .AddJsonOptions(x =>
    {
        x.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles;
        x.JsonSerializerOptions.WriteIndented = true;
    });

//  Kết nối Database
builder.Services.AddDbContext<QuanLyPhongKhamContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))
           .EnableSensitiveDataLogging()
           .LogTo(Console.WriteLine, LogLevel.Information));

//  Đăng ký các lớp DAL & BLL
builder.Services.AddScoped<BenhNhanDAL>();
builder.Services.AddScoped<BenhNhanBLL>();
builder.Services.AddScoped<KhamBenhDAL>();
builder.Services.AddScoped<KhamBenhBLL>();
builder.Services.AddScoped<LichHenDAL>();
builder.Services.AddScoped<LichHenBLL>();
builder.Services.AddScoped<ThongKeDAL>();
builder.Services.AddScoped<ThongKeBLL>();
builder.Services.AddScoped<HoaDonDAL>();
builder.Services.AddScoped<HoaDonBLL>();

//  Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

//  Cấu hình pipeline
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
