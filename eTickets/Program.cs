using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

var builder = WebApplication.CreateBuilder(args);

// Session Servislerini Ekleyin
builder.Services.AddDistributedMemoryCache(); // Gerekli oturum depolama
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30); // Oturumun zaman aþýmý süresi
    options.Cookie.HttpOnly = true; // Güvenlik için HTTPOnly
    options.Cookie.IsEssential = true; // Çerezlerin gerekli olduðunu belirtiyor
});

// Diðer servisleri ekleyin
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Middleware yapýlandýrmasý
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseSession(); // Session Middleware'i ekleyin
app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
