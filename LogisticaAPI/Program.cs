

using LogisticaAPI.ApplicationServices.Profiles;
using LogisticaAPI.Configurations;
using LogisticaAPI.Infra.Context;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
// adicionando configuração para a documentação swagger
builder.Services.AddSwaggerGen(opt =>
{
    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    opt.IncludeXmlComments(xmlPath);
});

builder.Services.AddSwagger();
builder.Services.AddRepository();
builder.Services.AddServices();
builder.Services.AddApplication();
builder.Services.AddTokenConfiguration(configuration);

var connectionString = configuration.GetConnectionString("Default");
builder.Services.AddDbContext<UnitOfWork>(options => options.UseSqlServer(connectionString));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(s => s.SwaggerEndpoint("/swagger/v1/swagger.json", "LogisticaAPI v1"));
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
