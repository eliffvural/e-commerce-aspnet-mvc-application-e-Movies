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
    options.IdleTimeout = TimeSpan.FromMinutes(30); // Oturumun zaman a��m� s�resi
    options.Cookie.HttpOnly = true; // G�venlik i�in HTTPOnly
    options.Cookie.IsEssential = true; // �erezlerin gerekli oldu�unu belirtiyor
});

// Di�er servisleri ekleyin
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Middleware yap�land�rmas�
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
