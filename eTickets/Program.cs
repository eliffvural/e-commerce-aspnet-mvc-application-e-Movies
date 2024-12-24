using eTickets.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Servisleri ekleme

// DbContext yapılandırması
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnectionString")));

// MVC servislerini ekleme
builder.Services.AddControllersWithViews();

// Session servislerini ekleme (eğer kullanıyorsanız)
builder.Services.AddSession();

// Authentication & Authorization servislerini ekleme (eğer kullanıyorsanız)
// Örneğin:
// builder.Services.AddAuthentication(/* seçenekler */)
//     .AddCookie(/* seçenekler */);

var app = builder.Build();

// HTTP istek boru hattını yapılandırma
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

// Session middleware'i kullanma (eğer kullanıyorsanız)
app.UseSession();

// Authentication & Authorization middleware'lerini kullanma (eğer kullanıyorsanız)
app.UseAuthentication();
app.UseAuthorization();

// Varsayılan route ayarını güncelleme
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Movies}/{action=Index}/{id?}");

app.Run();
