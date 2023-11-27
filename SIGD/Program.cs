using Microsoft.EntityFrameworkCore;
using SIGD.Controllers;
using SIGD.Models;
using SIGD.seção;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();


builder.Services.AddDbContext<Contexto>
(options => options.UseSqlServer
("Data Source=DESKTOP-5L7R9DL;Initial Catalog=DB;Integrated Security=True;TrustServerCertificate=True"));

builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

builder.Services.AddScoped<UsuariosController>();
builder.Services.AddScoped<Isecao,Secao>();

builder.Services.AddSession(o =>
{
o.Cookie.HttpOnly=true;
o.Cookie.IsEssential = false;

});


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
app.UseSession();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Login}/{action=Index}/{id?}");





app.Run();


