using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RabbitMQ_Demo.Services;
namespace RabbitMQ_Demo
{
    class RabbitMQ
    {
        static void Main(string[] args)
        {
            MessageBrockerSvc brockerSvc = new MessageBrockerSvc();

            brockerSvc.Send("IDG", "Hello World!");
            brockerSvc.Receive("IDG");
            Console.ReadLine();
        }
    }
}
