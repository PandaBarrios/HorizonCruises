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

var builder = WebApplication.CreateBuilder(args);

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
.WriteTo.Logger(l => l.Filter.ByIncludingOnly(e => e.Level == LogEventLevel.Information).WriteTo.File(@"Logs\Info-.log", shared: true, encoding:
Encoding.ASCII, rollingInterval: RollingInterval.Day))
.WriteTo.Logger(l => l.Filter.ByIncludingOnly(e => e.Level ==
LogEventLevel.Debug).WriteTo.File(@"Logs\Debug-.log", shared: true, encoding:
System.Text.Encoding.ASCII, rollingInterval: RollingInterval.Day))
.WriteTo.Logger(l => l.Filter.ByIncludingOnly(e => e.Level ==
LogEventLevel.Warning).WriteTo.File(@"Logs\Warning-.log", shared: true, encoding:
System.Text.Encoding.ASCII, rollingInterval: RollingInterval.Day))
.WriteTo.Logger(l => l.Filter.ByIncludingOnly(e => e.Level ==
LogEventLevel.Error).WriteTo.File(@"Logs\Error-.log", shared: true, encoding: Encoding.ASCII,
rollingInterval: RollingInterval.Day))
.WriteTo.Logger(l => l.Filter.ByIncludingOnly(e => e.Level ==
LogEventLevel.Fatal).WriteTo.File(@"Logs\Fatal-.log", shared: true, encoding: Encoding.ASCII,
rollingInterval: RollingInterval.Day))
.CreateLogger();
builder.Host.UseSerilog(logger);
//*************************** 
var app = builder.Build();

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

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();



//Estoy haciendo unos cambios
//Solucionando bugs
