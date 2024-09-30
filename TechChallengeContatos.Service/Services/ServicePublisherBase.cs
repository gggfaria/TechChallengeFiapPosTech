using System.Text;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using RabbitMQ.Client;
using TechChallengeContatos.Domain.Settings;

namespace TechChallengeContatos.Service.Services;

public abstract class ServicePublisherBase
{
    private readonly RabbitMQSettings _rabbitMqSetting;

    public ServicePublisherBase(IOptions<RabbitMQSettings> rabbitMqSetting)
    {
        _rabbitMqSetting = rabbitMqSetting.Value;
    }

    public async Task PublishMessageAsync<TEntity>(TEntity message)
    {
        var factory = new ConnectionFactory
        {
            HostName = _rabbitMqSetting.HostName,
            UserName = _rabbitMqSetting.UserName,
            Password = _rabbitMqSetting.Password
        };

        using var connection = factory.CreateConnection();
        using var channel = connection.CreateModel();
        channel.QueueDeclare(queue: _rabbitMqSetting.Queue, durable: false, exclusive: false, autoDelete: false,
            arguments: null);

        var messageJson = JsonConvert.SerializeObject(message);
        var body = Encoding.UTF8.GetBytes(messageJson);

        await Task.Run(() =>
            channel.BasicPublish(exchange: "", routingKey: _rabbitMqSetting.Queue, basicProperties: null, body: body));
    }
}