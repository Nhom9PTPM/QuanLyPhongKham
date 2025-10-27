using Yarp.ReverseProxy;

var builder = WebApplication.CreateBuilder(args);

// Đăng ký YARP Reverse Proxy
builder.Services.AddReverseProxy()
    .LoadFromConfig(builder.Configuration.GetSection("ReverseProxy"));

var app = builder.Build();

// Cho phép HTTPS
app.UseHttpsRedirection();

// Ánh xạ proxy
app.MapReverseProxy();

app.Run();
