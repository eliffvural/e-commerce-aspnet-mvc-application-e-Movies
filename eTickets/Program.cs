using eTickets.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Servisleri ekleme

// DbContext yapýlandýrmasý
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnectionString")));

// MVC servislerini ekleme
builder.Services.AddControllersWithViews();

// Session servislerini ekleme (eðer kullanýyorsanýz)
builder.Services.AddSession();

// Authentication & Authorization servislerini ekleme (eðer kullanýyorsanýz)
// Örneðin:
// builder.Services.AddAuthentication(/* seçenekler */)
//     .AddCookie(/* seçenekler */);

var app = builder.Build();

// HTTP istek boru hattýný yapýlandýrma
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}
else
{
    app.UseDeveloperExceptionPage();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

// Session middleware'i kullanma (eðer kullanýyorsanýz)
app.UseSession();

// Authentication & Authorization middleware'lerini kullanma (eðer kullanýyorsanýz)
app.UseAuthentication();
app.UseAuthorization();

// Varsayýlan route ayarýný güncelleme
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Movies}/{action=Index}/{id?}");

app.Run();
