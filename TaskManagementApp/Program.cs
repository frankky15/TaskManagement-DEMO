using TaskManagementApp.Data;
using TaskManagementApp.Interfaces;
using TaskManagementApp.Repository;
using TaskManagementApp.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddHttpContextAccessor();
builder.Services.AddSession();
// Data Access Layer
builder.Services.AddTransient<IUserJsonService, UserJsonService>();
builder.Services.AddTransient<IChoreJsonService, ChoreJsonService>();
builder.Services.AddSingleton<InMemoryDB>();
builder.Services.AddTransient<IUserRepository, UserRepository>();
builder.Services.AddTransient<IChoreRepository, ChoreRepository>();
// Service Layer
builder.Services.AddTransient<IUserService, UserService>();
builder.Services.AddTransient<IChoreService, ChoreService>();
builder.Services.AddTransient<IAuthService, AuthService>();


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
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
