using eTickets.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileSystemGlobbing.Internal.Patterns;

var builder = WebApplication.CreateBuilder(args);

// Servisleri ekleme

// DbContext yap�land�rmas�
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnectionString")));

// MVC servislerini ekleme
builder.Services.AddControllersWithViews();

// Session servislerini ekleme (e�er kullan�yorsan�z)
builder.Services.AddSession();

// Authentication & Authorization servislerini ekleme (e�er kullan�yorsan�z)
// �rne�in:
// builder.Services.AddAuthentication(/* se�enekler */)
//     .AddCookie(/* se�enekler */);

var app = builder.Build();

// HTTP istek boru hatt�n� yap�land�rma
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

// Session middleware'i kullanma (e�er kullan�yorsan�z)
app.UseSession();

// Authentication & Authorization middleware'lerini kullanma (e�er kullan�yorsan�z)
app.UseAuthentication();
app.UseAuthorization();

//Seed database
AppDbInitializer.Seed(app);



// Varsay�lan route ayar�n� g�ncelleme
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Movies}/{action=Index}/{id?}");




app.Run();
