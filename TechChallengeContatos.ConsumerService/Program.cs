using System.Reflection;
using Microsoft.EntityFrameworkCore;
using TechChallengeContatos.ConsumerService;
using TechChallengeContatos.Domain.Repositories;
using TechChallengeContatos.Domain.Settings;
using TechChallengeContatos.Infra.Context;
using TechChallengeContatos.Infra.Repositories;
using MediatR;

var builder = WebApplication.CreateBuilder(args);


builder.Services.Configure<RabbitMQSettings>(
    builder.Configuration.GetSection(nameof(RabbitMQSettings)));

//context
builder.Services.AddDbContext<ContatosDbContext>(
    opt => opt.UseNpgsql(builder.Configuration.GetConnectionString("SqlConnection"))
);
 
//mediatr
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));

//Repo
builder.Services.AddScoped<IContatoRepository, ContatoRepository>();


builder.Services.AddHostedService<QueueConsumerService>();

var app = builder.Build();

app.Run();

