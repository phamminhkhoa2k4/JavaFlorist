using JavaFlorist.Models.Domain;
using JavaFlorist.Repositories.Abstract;
using JavaFlorist.Repositories.Implementation;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddScoped<IUserAuthenticationService, UserAuthenticationService>();
builder.Services.AddScoped<IFileService, FileService>();
builder.Services.AddScoped<IBouquetService, BouquetService>();
builder.Services.AddScoped<IDiscountService, DiscountService>();
builder.Services.AddScoped<IBlogService, BlogService>();
builder.Services.AddScoped<IContactService, ContactService>();
builder.Services.AddScoped<IWishlistService, WishlistService>();
//builder.Services.AddScoped<ICartManager, SessionCartManager>();
builder.Services.AddMvc();



 
    builder.Services.AddSession(options =>
    {
        options.IdleTimeout = TimeSpan.FromMinutes(30);
        options.Cookie.HttpOnly = true;
        options.Cookie.IsEssential = true;
    });

  
  


builder.Services.AddDbContext<DatabaseContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("conn")));

// For Identity  
builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
    .AddEntityFrameworkStores<DatabaseContext>()
    .AddDefaultTokenProviders();

builder.Services.ConfigureApplicationCookie(options => options.LoginPath = "/UserAuthentication/Login");


//builder.Services.AddAuthentication(options =>
//{
//    options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
//    options.DefaultChallengeScheme = GoogleDefaults.AuthenticationScheme;
//})
//.AddCookie()
//.AddGoogle(options =>
//{
//    options.ClientId = "24365608018-kvfstghddkqgfpmie5odhb5b2qs8ehb6.apps.googleusercontent.com";
//    options.ClientSecret = "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";
//    options.CallbackPath = new PathString("/google-response");
//});



var app = builder.Build();

//app.MapGet("/signin-google", (HttpContext context) =>
//{
//    var redirectUrl = context.Request.PathBase + "/google-response";
//    var properties = new AuthenticationProperties { RedirectUri = redirectUrl };
//    return context.ChallengeAsync("Google", properties);
//}).RequireAuthorization();


//app.MapGet("/google-response", (HttpContext context) =>
//{
//    var result = context.AuthenticateAsync("Google").Result;
//    if (!result.Succeeded)
//    {
       
//    }

  

//    return Results.Redirect("/");
//}).RequireAuthorization();



// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();
            
app.MapControllerRoute(
    name: "areas",
    pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
    );
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}"
    );

app.Run();
