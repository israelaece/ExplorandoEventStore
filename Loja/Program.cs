using Dominio;
using EventStore.ClientAPI;
using Newtonsoft.Json;
using System;
using System.Text;

namespace Loja
{
    class Program
    {
        private const string RelacaoDePedidos = "Ecommerce.Pedidos";

        static void Main(string[] args)
        {
            Console.Title = "LOJA";

            Console.ReadLine();

            var novoPedido = new NovoPedidoCriado(new Pedido()
            {
                NomeDoCliente = "Israel Aece",
                ValorTotal = 1200
            });

            using (var conn = EventStoreConnection.Create(new Uri("tcp://localhost:1113")))
            {
                conn.ConnectAsync().Wait();
                conn.DeleteStreamAsync(RelacaoDePedidos, ExpectedVersion.Any).Wait();

                conn.AppendToStreamAsync(RelacaoDePedidos, ExpectedVersion.Any, GerarEvento(novoPedido)).Wait();
            }
        }

        private static EventData GerarEvento(Evento evento)
        {
            return new EventData(evento.Id, evento.GetType().Name, true, Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(evento)), null);
        }
    }
}