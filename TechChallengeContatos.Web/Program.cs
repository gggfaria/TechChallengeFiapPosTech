using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.EntityFrameworkCore;
using TechChallengeContatos.Domain.Repositories;
using TechChallengeContatos.Infra.Context;
using TechChallengeContatos.Infra.Repositories;
using TechChallengeContatos.Service.Interfaces;
using TechChallengeContatos.Service.Services;
using TechChallengeContatos.Web.Filters;
using TechChallengeContatos.Web.Profiles;

var builder = WebApplication.CreateBuilder(args);

// Repo
builder.Services.AddScoped<IContatoRepository, ContatoRepository>();

// Services
builder.Services.AddScoped<IContatoService, ContatoService>();

// Mapper
builder.Services.AddAutoMapper(typeof(ContatoProfile).Assembly);

builder.Services.AddControllers(
    options =>
    {
        options.OutputFormatters.RemoveType<StringOutputFormatter>();
        options.Filters.Add(typeof(CustomExceptionFilter));
    }).AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.DefaultIgnoreCondition
            = JsonIgnoreCondition.WhenWritingNull;
    });

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Context
builder.Services.AddDbContext<ContatosDbContext>(
    opt => opt.UseNpgsql(builder.Configuration.GetConnectionString("SqlConnection"))
);

var app = builder.Build();

using var serviceScope = app.Services.CreateScope();
serviceScope.ServiceProvider.GetService<ContatosDbContext>().Database.Migrate();

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
