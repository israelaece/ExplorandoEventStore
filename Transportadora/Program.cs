using Dominio;
using EventStore.ClientAPI;
using Newtonsoft.Json;
using System;
using System.Text;

namespace Transportadora
{
    class Program
    {
        private const string RelacaoDePedidos = "Ecommerce.Pedidos";

        static void Main(string[] args)
        {
            Console.Title = "TRANSPORTADORA";

            using (var conn = EventStoreConnection.Create(new Uri("tcp://localhost:1113")))
            {
                conn.ConnectAsync().Wait();
                conn.SubscribeToStreamAsync(RelacaoDePedidos, false, (a, e) => Processar(e)).Wait();

                Console.ReadLine();
            }
        }

        internal static void Processar(ResolvedEvent dadosDoEvento)
        {
            if (dadosDoEvento.Event.EventType == "NotaFiscalEmitida")
            {
                var novoPedido = Extrair<NotaFiscalEmitida>(dadosDoEvento);

                Log.Green("NOVO PEDIDO - NOTA FISCAL EMITIDA");
                Log.Green("Id: " + novoPedido.Id, 4);
                Log.Green("Cliente: " + novoPedido.NomeDoCliente, 4);
                Log.Green("Valor: " + novoPedido.ValorTotal.ToString("N2"), 4);
                Log.Green("NF-e: " + novoPedido.ChaveDaNotaFiscalEletronica, 4);
                Log.NewLine();
            }
        }

        private static TEvento Extrair<TEvento>(ResolvedEvent dadosDoEvento) where TEvento : Evento
        {
            return JsonConvert.DeserializeObject<TEvento>(Encoding.UTF8.GetString(dadosDoEvento.Event.Data));
        }
    }
}