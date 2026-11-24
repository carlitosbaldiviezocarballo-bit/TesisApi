using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;
using TesisApi.Data;
using TesisApi.Factories;
using TesisApi.Contracts.Factories;
using TesisApi.Contracts.Repositories;
using TesisApi.Contracts.Services;
using TesisApi.Middleware;
using TesisApi.Repositories;
using TesisApi.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<AppDbContext>(o =>
    o.UseSqlServer(builder.Configuration.GetConnectionString("ConexionDB")));

builder.Services.AddControllers()
    .AddJsonOptions(o => o.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter()));
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Repositorios
builder.Services.AddScoped<ITutorRepository, TutorRepository>();
builder.Services.AddScoped<ITesisRepository, TesisRepository>();
// Factories
builder.Services.AddSingleton<ITutorFactory, TutorFactory>();
builder.Services.AddSingleton<ITesisFactory, TesisFactory>();
// Servicios
builder.Services.AddScoped<ITutorService, TutorService>();
builder.Services.AddScoped<ITesisService, TesisService>();
builder.Services.AddSingleton<IImageStorageService, ImageStorageService>();

var app = builder.Build();

// Crea la base DBexamen (si no existe) y carga las 10 semillas
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    db.Database.EnsureCreated();
    DbSeeder.Seed(db);
}

app.UseMiddleware<ExceptionMiddleware>();
app.UseSwagger();
app.UseSwaggerUI();

app.UseDefaultFiles();   // sirve wwwroot/index.html (frontend)
app.UseStaticFiles();    // sirve wwwroot/uploads/*

app.MapControllers();
app.Run();
