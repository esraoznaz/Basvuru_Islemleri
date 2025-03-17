using Microsoft.EntityFrameworkCore;

using Urun_Denetim.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Basvurular.Business;
using Basvurular.Entities;
using Basvurular.DataAccess;
using Microsoft.Extensions.Options;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["Token:Issuer"],
            ValidAudience = builder.Configuration["Token:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Token:Key"] ?? string.Empty))  //anahtarlarý bytes a çevirme
        };
        options.MapInboundClaims = false;
    });

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("Admin", policy => policy.RequireClaim("role","Admin"));
    options.AddPolicy("Dagitim", policy => policy.RequireClaim("role","Dagitim"));
    options.AddPolicy("DagitimOrAdmin", policy => policy.RequireClaim("role","Dagitim","Admin"));
    options.AddPolicy("OnayOrAdmin", policy => policy.RequireClaim("role", "Onay", "Admin"));
});


builder.Services.Configure<JwtAyarlari>(builder.Configuration.GetSection("Token"));



//builder.Services.Configure<JwtAyarlari>(builder.Configuration.GetSection("Token"));

//builder.Services.AddScoped<ILoggerService, LoggerService>();

builder.Services.Configure<JwtAyarlari>(builder.Configuration.GetSection("JwtAyarlari"));
builder.Services.AddScoped<ITokenRepository, TokenRepository>();
builder.Services.AddScoped<IRepositoryAdmin, RepositoryAdmin>();
builder.Services.AddScoped<IRepository, Repository>();
builder.Services.AddScoped<IRepositoryOnay, RepositoryOnay>();
builder.Services.AddScoped<IRepositoryDagitim, RepositoryDagitim>();
builder.Services.AddScoped<IilceRepository, ilceRepository>();
builder.Services.AddScoped<IMahalleRepository, MahalleRepository>();





builder.Services.AddScoped<TokenRepository>();


builder.Services.AddScoped<TokenService>();
builder.Services.AddScoped<DagitimService>();
builder.Services.AddScoped<AdminService>();
builder.Services.AddScoped<OnayService>();
builder.Services.AddScoped<KresFormService>();
builder.Services.AddScoped<IlceService>();
builder.Services.AddScoped<MahalleService>();



builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();


// CORS Ayarlarý
builder.Services.AddCors(options => {
    options.AddPolicy("AllowAll", policy => {
        policy.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
    });
});

// Swagger (Opsiyonel)
builder.Services.AddSwaggerGen(); // Swagger ekleyin




builder.Services.AddCors(options => { options.AddPolicy("AllowAll", policy => { policy.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader(); }); });

//builder.WebHost.UseUrls("http://0.0.0.0:80");

// Add services to the container.
builder.Services.AddControllersWithViews();


//builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddDbContext<ApplicationDbContext>(options =>
	options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"),
		b => b.MigrationsAssembly("Urun_Denetim")));


var app = builder.Build();


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}



// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
app.UseCors("AllowAll"); // CORS'u tüm sitelere aç app.UseAuthorization();
app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
