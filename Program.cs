using System.Configuration;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
ConfigureService();
// Add services to the container.
//   using (var scope = builder.Services.CreateScope()) {
//         InsertTestArticle.InsertArticle(scope.ServiceProvider);
//     }
void ConfigureService()
{
    builder.Services.AddRazorPages();

    builder.Services.AddDbContext<ArticleContext>(options =>
    {
        var connectionString = builder.Configuration.GetConnectionString("ArticleContext");
        options.UseSqlServer(connectionString);
    });

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

app.UseAuthorization();

app.MapRazorPages();


app.Run();
