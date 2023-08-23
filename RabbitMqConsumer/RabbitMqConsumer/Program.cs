using RabbitMQ.Client.Events;
using RabbitMQ.Client;
using System.Text;

namespace RabbitMqConsumer
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //Here we specify the Rabbit MQ Server. we use rabbitmq docker image and use it
            ConnectionFactory factory = new ConnectionFactory();
            factory.Uri = new System.Uri("amqp://guest:guest@localhost:5672/");



            //Create the RabbitMQ connection using connection factory details as i mentioned above
            var connection = factory.CreateConnection();
            //Here we create channel with session and model
            using
            var channel = connection.CreateModel();
            //declare the queue after mentioning name and a few property related to that
            channel.QueueDeclare("users", true, false, false);
            channel.QueueBind("users", "chandani Fanout exchange", "");
            //Set Event object which listen message from chanel which is sent by producer
            var consumer = new EventingBasicConsumer(channel);
            consumer.Received += (model, eventArgs) => {
                var body = eventArgs.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);
                Console.WriteLine($"User message received: {message}");
            };
            //read the message
            channel.BasicConsume(queue: "users", autoAck: true, consumer: consumer);
            Console.ReadKey();
        }
    }
}