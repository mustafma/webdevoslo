using System;
using System.IO;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RabbitMQ.Client;

namespace messageSender
{
  class Sender
  {
    static void Main(string[] args)
    {
      DoStartUp();

      string host = Configuration["Rabbit:Host"];
      int port = int.Parse(Configuration["Rabbit:Port"]);
      Console.WriteLine("****** host: " + host);
      Console.WriteLine("****** port: " + port);
      Console.WriteLine("****** connection string: " + Configuration.GetConnectionString("MyDbContext"));

      int retries = int.Parse(Configuration["Rabbit:Retries"]);
      Console.WriteLine("******* no. retries: " + retries);
      int retrySleep = int.Parse(Configuration["Rabbit:RetrySleep"]);
      Console.WriteLine("******* retry sleep time: " + retrySleep);

      string queueName = Configuration["QueueName"];
      Console.WriteLine("******* queue name: " + queueName);

      var factory = new ConnectionFactory() { HostName = host, Port = port };

      for (int i = 0; i < retries; i++)
      {
        try
        {
          using (var connection = factory.CreateConnection())
          using (var channel = connection.CreateModel())
          {
            channel.QueueDeclare(queue: queueName,
                                 durable: false,
                                 exclusive: false,
                                 autoDelete: false,
                                 arguments: null);

            string message = "Hello World!";
            var body = Encoding.UTF8.GetBytes(message);

            channel.BasicPublish(exchange: "",
                                 routingKey: "hello",
                                 basicProperties: null,
                                 body: body);
            Console.WriteLine(" [x] Sent {0}", message);
          }

          Console.WriteLine(" Press [q] to exit.");
          while (Console.ReadLine() != "q")
          { }

        }
        catch (Exception)
        {
          Console.WriteLine("Exception trying to connect attempt {0} ...", i);
          System.Threading.Thread.Sleep(retrySleep);
        }

      }
    }

    private static void DoStartUp()
    {
      var cbuilder = new ConfigurationBuilder()
        .SetBasePath(Directory.GetCurrentDirectory())
        .AddJsonFile("appsettings.json")
        .AddEnvironmentVariables();

      Configuration = cbuilder.Build();

      // configure services
      var services = new ServiceCollection();
      // .AddDbContext<MyDbContext>(options =>
      //   options.UseSqlServer(Configuration.GetConnectionString("MyDbContext")));

      // create a service provider from the service collection
      ServiceProvider = services.BuildServiceProvider();
    }

    public static IConfigurationRoot Configuration { get; set; }
    public static ServiceProvider ServiceProvider { get; set; }
  }

}

