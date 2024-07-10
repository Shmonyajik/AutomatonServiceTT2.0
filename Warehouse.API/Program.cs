using Serilog;
using Serilog.Events;
using Warehouse.API.Extensions;
using Warehouse.API.Hubs;
using Warehouse.DAL;

// Logger
// Log.Logger = new LoggerConfiguration()
//     .MinimumLevel.Override("Microsoft", LogEventLevel.Information)
//     .WriteTo.Console()
//     .CreateBootstrapLogger();

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseSerilog((context, services, configuration) => configuration
    .ReadFrom.Configuration(context.Configuration)
    .ReadFrom.Services(services)
    .WriteTo.Console());

// Add services to the container.
builder.Services
    .AddDatabase(builder.Configuration)
    .AddServices()
    .AddAutoMapperProfiles();

builder.Services.AddControllers();
builder.Services.AddSignalR();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseDeveloperExceptionPage();
    app.UseWebAssemblyDebugging();
}

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;

    var context = services.GetRequiredService<WarehouseDbContext>();
    context.Database.EnsureCreated();
    DbInitializer.Initialize(context);
}

app.UseBlazorFrameworkFiles();
app.UseStaticFiles();

app.UseRouting();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.MapHub<StorageHub>("/storageHub");

app.UseEndpoints(endpoints =>
{
    endpoints.MapFallbackToFile("index.html");
});


app.Run(async context =>
{
    context.Response.Cookies.Append("id", Guid.NewGuid().ToString());
});

app.Run();