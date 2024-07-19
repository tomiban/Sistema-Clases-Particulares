using System.Runtime.InteropServices;
using DinkToPdf;
using DinkToPdf.Contracts;
using Microsoft.EntityFrameworkCore;
using TeddyMVC.Data;
using TeddyMVC.Interfaces;
using TeddyMVC.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContextFactory<ApplicationDbContext>(opt =>
    opt.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddScoped<IViewRenderService, ViewRenderService>();
builder.Services.AddSingleton(typeof(IConverter), new SynchronizedConverter(new PdfTools()));
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Agenda}/{action=Index}/{id?}");
/* IWebHostEnvironment env = app.Environment;

// Configurar Rotativa para Arch Linux
var rotativaPath = Path.Combine("rotativa", "arch", "bin");
Rotativa.AspNetCore.RotativaConfiguration.Setup(env.WebRootPath, rotativaPath); */

IWebHostEnvironment env = app.Environment;

// Detectar el sistema operativo
string os = Environment.OSVersion.Platform.ToString().ToLower();

// Configurar Rotativa según el sistema operativo
if (os.Contains("win32nt"))
{
    // Configurar Rotativa para Windows
    var rotativaPath = Path.Combine("rotativa", "windows");
    Rotativa.AspNetCore.RotativaConfiguration.Setup(env.WebRootPath, rotativaPath);
}
else if (os.Contains("unix"))
{
    // Configurar Rotativa para Arch Linux
    var rotativaPath = Path.Combine("rotativa", "arch", "bin");
    Rotativa.AspNetCore.RotativaConfiguration.Setup(env.WebRootPath, rotativaPath);
}
else
{
    throw new PlatformNotSupportedException("Este sistema operativo no es compatible con Rotativa.");
}
app.Run();
