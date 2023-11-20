using CsvProcessor.Controllers;
using CsvProcessor.Models;
using CsvProcessor.Models.Interfaces;
using CsvProcessor.Models.Interfaces.Implementation;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddSingleton<IFileProcessorRepository, FileProcessorRepository>();
builder.Services.AddScoped<IFileProcessorService, FileProcessorServiceService>();
builder.Services.AddScoped<IApplicationFileProvider, LocalFileProvider>();
builder.Services.AddScoped<IApplicationFileProvider, AzureBlobFileProvider>();
builder.Services.Configure<ConnectionInformation>(builder.Configuration.GetSection("ConnectionInformation"));

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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
