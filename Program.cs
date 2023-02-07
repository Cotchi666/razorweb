using System.Configuration;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using razorwebef.Services.Mail;
using  Microsoft.AspNetCore.Identity.UI.Services;

var builder = WebApplication.CreateBuilder(args);
ConfigureService();
// Add services to the container.
//   using (var scope = builder.Services.CreateScope()) {
//         InsertTestArticle.InsertArticle(scope.ServiceProvider);
//     }
void ConfigureService()
{
    builder.Services.AddOptions();
    var emailSetting = builder.Configuration.GetSection("MailSettings");
    builder.Services.Configure<MailSettings>(emailSetting);
    builder.Services.AddTransient<IEmailSender, SendMailService>();

    builder.Services.AddRazorPages();

    builder.Services.AddDbContext<ArticleContext>(options =>
    {
        var connectionString = builder.Configuration.GetConnectionString("ArticleContext");
        options.UseSqlServer(connectionString);
    });
    // builder.Services.AddIdentity<AppUser, IdentityRole>()
    //           .AddEntityFrameworkStores<ArticleContext>()
    //           .AddDefaultTokenProviders();

    builder.Services.AddDefaultIdentity<AppUser>()
                   .AddEntityFrameworkStores<ArticleContext>()
                   .AddDefaultTokenProviders();
    builder.Services.Configure<IdentityOptions>(options =>
    {
        // Thiết lập về Password
        options.Password.RequireDigit = false; // Không bắt phải có số
        options.Password.RequireLowercase = false; // Không bắt phải có chữ thường
        options.Password.RequireNonAlphanumeric = false; // Không bắt ký tự đặc biệt
        options.Password.RequireUppercase = false; // Không bắt buộc chữ in
        options.Password.RequiredLength = 3; // Số ký tự tối thiểu của password
        options.Password.RequiredUniqueChars = 1; // Số ký tự riêng biệt

        // Cấu hình Lockout - khóa user
        options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5); // Khóa 5 phút
        options.Lockout.MaxFailedAccessAttempts = 3; // Thất bại 3 lầ thì khóa
        options.Lockout.AllowedForNewUsers = true;

        // Cấu hình về User.
        options.User.AllowedUserNameCharacters = // các ký tự đặt tên user
            "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
        options.User.RequireUniqueEmail = true;  // Email là duy nhất


        // Cấu hình đăng nhập.
        options.SignIn.RequireConfirmedEmail = true;            // Cấu hình xác thực địa chỉ email (email phải tồn tại)
        options.SignIn.RequireConfirmedPhoneNumber = false;     // Xác thực số điện thoại
        options.SignIn.RequireConfirmedAccount = true;

    });

    // builder.Services.ConfigureApplicationCookie(options =>
    // {
    //     options.LoginPath = "/login/";
    //     options.LogoutPath = "/logout/";
    //     options.AccessDeniedPath = "/khongduoctruycap.html";
    // });

}
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
using (var scope = app.Services.CreateScope())
{
    InsertTestArticle.Initialize(scope.ServiceProvider);
}
app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseAuthentication();   // Phục hồi thông tin đăng nhập (xác thực)
app.UseAuthorization();   // Phục hồi thông tinn về quyền của User
app.MapRazorPages();


app.Run();
