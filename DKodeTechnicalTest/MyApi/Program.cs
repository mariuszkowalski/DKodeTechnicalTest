using MyApi.Helpers;
using MyApi.Helpers.Factories;
using MyApi.Helpers.Factories.Interfaces;
using MyApi.Helpers.Handlers;
using MyApi.Helpers.Handlers.Interfaces;
using MyApi.Helpers.Interfaces;
using MyApi.Models.DataAccess;
using MyApi.Models.DataAccess.Interfaces;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var userAgent = builder.Configuration.GetValue<string>("UserAgent");
var clientName = builder.Configuration.GetValue<string>("httpClientName");
builder.Services.AddHttpClient(
    clientName ?? "default",
    client =>
    {
        client.DefaultRequestHeaders.Add("User-Agent", userAgent);
    });

// Serilog.
var serilog = new LoggerConfiguration()
    .MinimumLevel.Verbose()
    .MinimumLevel.Override("Microsoft", Serilog.Events.LogEventLevel.Warning)
    .Enrich.FromLogContext()
    .WriteTo.File(Path.Combine(AppContext.BaseDirectory, "Logs/Log_.txt"),
        encoding: System.Text.Encoding.UTF8,
        rollingInterval: RollingInterval.Day,
        retainedFileCountLimit: 22)
    .CreateLogger();
builder.Logging.AddSerilog(serilog, true);

builder.Services.AddTransient<IFileDownloader, FileDownloader>();
builder.Services.AddTransient<IRequestProcessor, RequestProcessor>();
builder.Services.AddTransient<IHandler, DownloadHandler>();
builder.Services.AddTransient<IHandler, FileReaderHandler>();

builder.Services.AddTransient<IProductsDao, ProductsDao>();
builder.Services.AddTransient<IStockDao, StockDao>();
builder.Services.AddTransient<IPricesDao, PricesDao>();

builder.Services.AddTransient<IFileProcessorFactory, FileProcessorFactory>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}



app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
