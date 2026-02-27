using Microsoft.EntityFrameworkCore;
using OnlineHelpDesk_ASP_NET_CORE.Services;
using OnlineHelpDesk_ASP_NET_CORE.Models; // Đảm bảo có nếu thiếu

var builder = WebApplication.CreateBuilder(args);

// 1. Cấu hình dịch vụ
builder.Services.AddControllersWithViews();
builder.Services.AddHttpContextAccessor();
builder.Services.AddSession();

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection"),
        sql => sql.EnableRetryOnFailure()
    )
);

// 2. Inject Service
builder.Services.AddScoped<NhanVienService, NhanVienServiceImpl>();
builder.Services.AddScoped<YeuCauService, YeuCauServiceImpl>();
builder.Services.AddHttpContextAccessor();
var app = builder.Build();

// 3. Middleware pipeline
app.UseStaticFiles();       // Truy cập wwwroot (ảnh, js, css)
app.UseRouting();
app.UseSession();           // Cho phép sử dụng HttpContext.Session

// 4. Cấu hình route
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Account}/{action=Login}/{username?}");

// 5. Khởi động ứng dụng
app.Run();
