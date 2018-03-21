using System;
using System.IO;
using System.Linq;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;


namespace developerService
{
  class Program
  {

    public static void Main()
    {
      DoStartUp();

      DoDbMigration();

      CreateDbData();

      string host = Configuration["Rabbit:Host"];
      int port = int.Parse(Configuration["Rabbit:Port"]);
      Console.WriteLine("****** host: " + host);
      Console.WriteLine("****** port: " + port);
      Console.WriteLine("****** connection string: "+ Configuration.GetConnectionString("MyDbContext"));

      int retries = int.Parse(Configuration["Rabbit:Retries"]);
      Console.WriteLine("******* no. retries: " + retries);
      int retrySleep= int.Parse(Configuration["Rabbit:RetrySleep"]);
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

            var consumer = new EventingBasicConsumer(channel);
            consumer.Received += (model, ea) =>
            {
              MyDbContext ctx = ServiceProvider.GetService<MyDbContext>();

              var body = ea.Body;
              var message = Encoding.UTF8.GetString(body);
              Console.WriteLine(" [x] Received {0}", message);

              try
              {
                var developer = JsonConvert.DeserializeObject<Developer>(message);
                ctx.Developers.Add(developer);
                ctx.SaveChanges();
              }
              catch (Exception ex)
              {
                Console.WriteLine("Exception trying to save developer: "+ ex);
              }
            };
            channel.BasicConsume(queue: queueName,
              autoAck: true,
              consumer: consumer);

            Console.WriteLine(" Press [q] to exit.");
            while (Console.ReadLine() != "q")
            { }
          }
        }
        catch (Exception)
        {
          Console.WriteLine("Exception trying to connect attempt {0} ...",i);
          System.Threading.Thread.Sleep(retrySleep);
        }

      }
    }

    private static void DoDbMigration()
    {
      
      for (int i = 0; i < 5; i++)
      {
        Console.WriteLine("Migrating / creating the database...");
        try
        {
          MyDbContext thedb  = new MyDbContext();
          thedb.Database.Migrate();
          Console.WriteLine(" Finished migrating / creating the database");
          break;
        }
        catch (Exception ex)
        {
          Console.WriteLine("Exception trying to connect to db {0} ..." + ex.Message, i);
          System.Threading.Thread.Sleep(5000);
        }
      }
      
    }

    private static void CreateDbData()
    {
      //hacker names from https://9gag.com/gag/aG95YrX/whats-your-hacker-name
      var ctx = ServiceProvider.GetService<MyDbContext>();

      if (!ctx.Developers.Any())
      {
        ctx.Developers.Add(new Developer() { Location = "Stavanger", Name = "Robin", HackerName = "RougeWire" });
        ctx.Developers.Add(new Developer() { Location = "Stavanger", Name = "Doug", HackerName = "M4sterSaint" });
        ctx.Developers.Add(new Developer() { Location = "Oslo", Name = "Robert", HackerName = "RougueRoute" });
        ctx.Developers.Add(new Developer() { Location = "Oslo", Name = "Silje", HackerName = "NinjaPh4ntom" });
        ctx.Developers.Add(new Developer() { Location = "Oslo", Name = "OddMarius", HackerName = "MrBet4" });
        ctx.Developers.Add(new Developer() { Location = "Oslo", Name = "Jo", HackerName = "Crashx" });
        ctx.Developers.Add(new Developer() { Location = "Oslo", Name = "Mustafa", HackerName = "UberBet4" });
        ctx.Developers.Add(new Developer() { Location = "Oslo", Name = "Raul", HackerName = "RogueHydra" });

        ctx.SaveChanges();
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
      var services = new ServiceCollection()
        .AddDbContext<MyDbContext>(options =>
          options.UseSqlServer(Configuration.GetConnectionString("MyDbContext")));

      // create a service provider from the service collection
      ServiceProvider = services.BuildServiceProvider();
    }

    public static IConfigurationRoot Configuration { get; set; }
    public static ServiceProvider ServiceProvider { get; set; }
  }

}
