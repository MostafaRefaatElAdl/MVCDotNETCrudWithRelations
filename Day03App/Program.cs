using Day03App.Models;
using Day04App.Data;
using Day04App.Models;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddScoped<IDepartment, DepartmentDB>();
builder.Services.AddScoped<IStudent, StudentDB>();
builder.Services.AddScoped<ICourse, CourseDB>();
builder.Services.AddDbContext<DatabaseITI>(x =>
{
    x.UseSqlServer("Server=.;Database=MVCDotNETRelations;Trusted_Connection=True");
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

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=StudentCourses}/{action=Index}/{id?}");

app.Run();
