using Newtonsoft.Json;
using RabbitMQ.Client;
using System.Text;

namespace RabbitmqProducer
{
    internal class Program
    {
        static void Main(string[] args)
        {
            ConnectionFactory factory = new ConnectionFactory();
            factory.Uri = new System.Uri("amqp://guest:guest@localhost:5672/");
            //Create the RabbitMQ connection using connection factory details as i mentioned above
            var connection = factory.CreateConnection();
            //Here we create channel with session and model
            using var channel = connection.CreateModel();
            channel.ExchangeDeclare("chandani Fanout exchange", ExchangeType.Fanout, true, false);
            //declare the queue after mentioning name and a few property related to that
            //Serialize the message
            var json = JsonConvert.SerializeObject("hello worldzzzz");
            var body = Encoding.UTF8.GetBytes(json);
            //put the data on to the user queue
            channel.BasicPublish("chandani Fanout exchange", routingKey: " ", body: body);
        }
    }
}