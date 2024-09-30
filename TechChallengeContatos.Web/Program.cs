using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.EntityFrameworkCore;
using TechChallengeContatos.Domain.Repositories;
using TechChallengeContatos.Domain.Settings;
using TechChallengeContatos.Infra.Context;
using TechChallengeContatos.Infra.Repositories;
using TechChallengeContatos.Service.Interfaces;
using TechChallengeContatos.Service.Services;
using TechChallengeContatos.Web.Filters;

var builder = WebApplication.CreateBuilder(args);


//Repo
builder.Services.AddScoped<IContatoRepository, ContatoRepository>();

//Services
builder.Services.AddScoped<IContatoService, ContatoService>();

//mapper
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.Configure<RabbitMQSettings>(
    builder.Configuration.GetSection(nameof(RabbitMQSettings)));

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

//context
builder.Services.AddDbContext<ContatosDbContext>(
    opt => opt.UseNpgsql(builder.Configuration.GetConnectionString("SqlConnection"))
);


var app = builder.Build();

using var serviceScope = app.Services.CreateScope();
serviceScope.ServiceProvider.GetService<ContatosDbContext>().Database.Migrate();

// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
app.UseSwagger();
app.UseSwaggerUI();
//}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();