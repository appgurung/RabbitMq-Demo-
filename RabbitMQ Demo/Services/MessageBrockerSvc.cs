using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RabbitMQ_Demo.Services
{
    public class MessageBrockerSvc
    {
        ConnectionFactory connectionFactory = new ConnectionFactory();

        IConnection connection;
           
        public MessageBrockerSvc()
        {
            connection = connectionFactory.CreateConnection();
        }

        //If RabbitMQ server is on a remote PC
        //public IConnection GetConnection(string hostName, string userName, string password)
        //{
        //    ConnectionFactory connectionFactory = new ConnectionFactory();
        //    connectionFactory.HostName = hostName;
        //    connectionFactory.UserName = userName;
        //    connectionFactory.Password = password;
        //    return connectionFactory.CreateConnection();
        //}

        public void Send(string queue, string data)
        {
            using (IConnection connection = new ConnectionFactory().CreateConnection())
            {
                using (IModel channel = connection.CreateModel())
                {
                    channel.QueueDeclare(queue, false, false, false, null);
                    channel.BasicPublish(string.Empty, queue, null, Encoding.UTF8.GetBytes(data));
                }
            }
        }

        public void Receive(string queue)
        {
            using (IConnection connection = new ConnectionFactory().CreateConnection())
            {
                using (IModel channel = connection.CreateModel())
                {
                    channel.QueueDeclare(queue, false, false, false, null);
                    var consumer = new EventingBasicConsumer(channel);
                    BasicGetResult result = channel.BasicGet(queue, true);
                    if (result != null)
                    {
                        string data =
                        Encoding.UTF8.GetString(result.Body);
                        Console.WriteLine(data);
                    }
                }
            }
        }
    }
}
