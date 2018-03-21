using System;
using System.Text;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using RabbitMQ.Client;
using web.Controllers;

namespace web
{
  public class DeveloperService
  {
    private readonly string _routingKey;
    private readonly IModel _channel;

    public DeveloperService(IConfiguration config)
    {
      string host = config["Rabbit:Host"];
      int port = int.Parse(config["Rabbit:Port"]);
      string queueName = config["QueueName"];
      _routingKey = queueName;

      try
      {
        var factory = new ConnectionFactory() { HostName = host, Port = port };
        var connection = factory.CreateConnection();
          _channel = connection.CreateModel();
          _channel.QueueDeclare(queue: queueName,
            durable: false,
            exclusive: false,
            autoDelete: false,
            arguments: null);
      }
      catch (Exception)
      {
        Console.WriteLine("Exception trying to connect attempt");
      }
    }

    public bool AddDeveloper(Developer developer)
    {
      var message = JsonConvert.SerializeObject(developer);
     // string message = "Hello World!";
      var body = Encoding.UTF8.GetBytes(message);

      try
      {
        _channel.BasicPublish(exchange: "",
          routingKey: _routingKey,
          basicProperties: null,
          body: body);
      }
      catch (Exception e)
      {
        Console.WriteLine(e);
        return false;
      }
      return true;
    }
  }
}
