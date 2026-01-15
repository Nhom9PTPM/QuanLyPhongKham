using Ocelot.DependencyInjection;
using Ocelot.Middleware;

var builder = WebApplication.CreateBuilder(args);

// Đọc cấu hình từ file ocelot.json
builder.Configuration.AddJsonFile("ocelot.json", optional: false, reloadOnChange: true);

// Đăng ký Ocelot vào DI container
builder.Services.AddOcelot();

var app = builder.Build();

// Kích hoạt Ocelot middleware
await app.UseOcelot();

app.Run();
