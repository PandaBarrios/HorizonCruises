using HorizonCruises.Application.Profiles;
using HorizonCruises.Application.Services.Implementations;
using HorizonCruises.Application.Services.Interfaces;
using HorizonCruises.Infraestructure.Data;
using HorizonCruises.Infraestructure.Repository.Implementations;
using HorizonCruises.Infraestructure.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using Serilog.Events;
using Serilog;
using System.Text;
using HorizonCruises.web.Middleware;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Globalization;
using Rotativa.AspNetCore;
using HorizonCruises.Application.Config;
using HorizonCruises.Web.Controllers;

var builder = WebApplication.CreateBuilder(args);

// Mapeo de la clase AppConfig para leer appsettings.json
builder.Services.Configure<AppConfig>(builder.Configuration);

builder.Services.AddHttpClient<UsuarioController>();

// Add services to the container.
builder.Services.AddControllersWithViews();

// Configurar D.I.

//Repository 
builder.Services.AddTransient<IRepositoryBarco, RepositoryBarco>();
builder.Services.AddTransient<IRepositoryCrucero, RepositoryCrucero>();
builder.Services.AddTransient<IRepositoryHabitacion, RepositoryHabitacion>();
builder.Services.AddTransient<IRepositoryRol, RepositoryRol>();
builder.Services.AddTransient<IRepositoryCliente, RepositoryCliente>();
builder.Services.AddTransient<IRepositoryReserva, RepositoryReserva>();
builder.Services.AddTransient<IRepositoryPuerto, RepositoryPuerto>();
builder.Services.AddTransient<IRepositoryBarcoHabitaciones, RepositoryBarcoHabitaciones>();
builder.Services.AddTransient<IRepositoryItinerario, RepositoryItinerario>();
builder.Services.AddTransient<IRepositoryFechaCrucero, RepositoryFechaCrucero>();
builder.Services.AddTransient<IRepositoryPrecioHabitacion, RepositoryPrecioHabitacion>();
builder.Services.AddTransient<IRepositoryUsuarioHuesped, RepositoryUsuarioHuesped>();
builder.Services.AddTransient<IRepositoryHuesped, RepostoryHuesped>();
builder.Services.AddTransient<IRepositoryComplemento, RepositoryComplemento>();
builder.Services.AddTransient<IRepositoryTarjeta, RepositoryTarjeta>();

//Services 
builder.Services.AddTransient<IServiceBarco, ServiceBarco>();
builder.Services.AddTransient<IServiceCrucero, ServiceCrucero>();
builder.Services.AddTransient<IServiceHabitacion, ServiceHabitacion>();
builder.Services.AddTransient<IServiceRol, ServiceRol>();
builder.Services.AddTransient<IServiceCliente, ServiceCliente>();
builder.Services.AddTransient<IServiceReserva, ServiceReserva>();
builder.Services.AddTransient<IServicePuerto, ServicePuerto>();
builder.Services.AddTransient<IServiceBarcoHabitaciones, ServiceBarcoHabitaciones>();
builder.Services.AddTransient<IServiseItinerario, ServiceItinerario>();
builder.Services.AddTransient<IServiceFechaCrucero, ServiceFechaCrucero>();
builder.Services.AddTransient<IServicePrecioHabitacion, ServicePrecioHabitacion>();
builder.Services.AddTransient<IServiceUsuarioHuesped, ServiceUsuarioHuesped>();
builder.Services.AddTransient<IServiceHuesped, ServiceHuesped>();  
builder.Services.AddTransient<IServiceComplemento, ServiceComplemento>();
builder.Services.AddTransient<IServiceTarjeta, ServiceTarjeta>();

//Seguridad
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/Login/Index";  // Página de inicio de sesión
        options.AccessDeniedPath = "/Login/Forbidden"; // Página de acceso denegado
        options.ExpireTimeSpan = TimeSpan.FromMinutes(30); // Tiempo de expiración de la sesión
    });

builder.Services.AddAuthorization();

//Configurar Automapper 
builder.Services.AddAutoMapper(config =>
{
    config.AddProfile<BarcoHabitacionesProfile>();
    config.AddProfile<BarcoProfile>();
    config.AddProfile<ClienteProfile>();
    config.AddProfile<ComplementoProfile>();
    config.AddProfile<CruceroProfile>();
    config.AddProfile<DestinoProfile>();
    config.AddProfile<FechaCruceroProfile>();
    config.AddProfile<HabitacionProfile>();
    config.AddProfile<HuespedProfile>();
    config.AddProfile<ItinerarioProfile>();
    config.AddProfile<PrecioHabitacionProfile>();
    config.AddProfile<PuertoProfile>();
    config.AddProfile<ReservaComplementoProfile>();
    config.AddProfile<ReservaHabitacionProfile>();
    config.AddProfile<ReservaProfile>();
    config.AddProfile<RolProfile>();
    config.AddProfile<TarjetaProfile>();
    config.AddProfile<TelefonoProfile>();
    config.AddProfile<UsuarioHuespedProfile>();
});

// Configuar Conexión a la Base de Datos SQL 
builder.Services.AddDbContext<HorizonCruisesContext>(options =>
{
    // it read appsettings.json file
    options.UseSqlServer(builder.Configuration.GetConnectionString("SqlServerDataBase"));
    if (builder.Environment.IsDevelopment())
        options.EnableSensitiveDataLogging();
});

//Configuración Serilog 
// Logger. P.E. Verbose = muestra SQl Statement 
var logger = new LoggerConfiguration()
                // Limitar la información de depuración
                .MinimumLevel.Override("Microsoft", LogEventLevel.Error) 
                .Enrich.FromLogContext()
                // Log LogEventLevel.Verbose muestra mucha información, pero no es necesaria solo para el proceso de depuración 
                .WriteTo.Console(LogEventLevel.Information)
                .WriteTo.Logger(l => l.Filter.ByIncludingOnly(e => e.Level == LogEventLevel.Information).WriteTo.File(@"Logs\Info-.log", shared: true, encoding:Encoding.ASCII, rollingInterval: RollingInterval.Day))
                .WriteTo.Logger(l => l.Filter.ByIncludingOnly(e => e.Level == LogEventLevel.Debug).WriteTo.File(@"Logs\Debug-.log", shared: true, encoding:System.Text.Encoding.ASCII, rollingInterval: RollingInterval.Day))
                .WriteTo.Logger(l => l.Filter.ByIncludingOnly(e => e.Level == LogEventLevel.Warning).WriteTo.File(@"Logs\Warning-.log", shared: true, encoding:System.Text.Encoding.ASCII, rollingInterval: RollingInterval.Day))
                .WriteTo.Logger(l => l.Filter.ByIncludingOnly(e => e.Level ==LogEventLevel.Error).WriteTo.File(@"Logs\Error-.log", shared: true, encoding: Encoding.ASCII,rollingInterval: RollingInterval.Day))
                .WriteTo.Logger(l => l.Filter.ByIncludingOnly(e => e.Level ==LogEventLevel.Fatal).WriteTo.File(@"Logs\Fatal-.log", shared: true, encoding: Encoding.ASCII,rollingInterval: RollingInterval.Day))
                .CreateLogger();

builder.Host.UseSerilog(logger);
//*************************** 

//Permite colocar el simbolo del colon de manera global
var cultureInfo = new CultureInfo("es-CR");
CultureInfo.DefaultThreadCurrentCulture = cultureInfo;
CultureInfo.DefaultThreadCurrentUICulture = cultureInfo;


var app = builder.Build();

// Middleware
//app.UseAuthentication();


// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
else{
    // Error control Middleware 
    app.UseMiddleware<ErrorHandlingMiddleware>();
}

// Error control Middleware 
app.UseMiddleware<ErrorHandlingMiddleware>();

//Activar soporte a la solicitud de registro con SERILOG 
app.UseSerilogRequestLogging();

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

// Activar Antiforgery  
app.UseAntiforgery();

//app.MapControllerRoute(
//    name: "default",
//    pattern: "{controller=Home}/{action=Index}/{id?}");

//app.Run();

//Importante para crear un PDF
RotativaConfiguration.Setup(app.Environment.WebRootPath, "Rotativa");

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Login}/{action=Index}/{id?}");

app.Run();

