using Dominio;
using EventStore.ClientAPI;
using Newtonsoft.Json;
using System;
using System.Linq;
using System.Text;

namespace NotaFiscal
{
    class Program
    {
        private const string RelacaoDePedidos = "Ecommerce.Pedidos";
        private static int UltimoEventoProcessado = 0;

        static void Main(string[] args)
        {
            Console.Title = "NOTAS FISCAIS";

            using (var conn = EventStoreConnection.Create(new Uri("tcp://localhost:1113")))
            {
                conn.ConnectAsync().Wait();

                while ((Console.ReadLine() != null))
                {
                    var itens = conn.ReadStreamEventsForwardAsync(RelacaoDePedidos, UltimoEventoProcessado + 1, 200, false).Result;

                    if (itens.Events.Any())
                    {
                        foreach (var e in itens.Events)
                            Processar(conn, e);
                    }
                    else
                    {
                        Log.Yellow("Não há eventos para processar.");
                    }
                }
            }
        }

        internal static void Processar(IEventStoreConnection conexao, ResolvedEvent dadosDoEvento)
        {
            if (dadosDoEvento.Event.EventType == "NovoPedidoCriado")
            {
                var novoPedido = Extrair<NovoPedidoCriado>(dadosDoEvento);

                Log.Green("NOVO PEDIDO");
                Log.Green("Id: " + novoPedido.Id, 4);
                Log.Green("Data: " + novoPedido.Data, 4);
                Log.Green("Cliente: " + novoPedido.NomeDoCliente, 4);
                Log.Green("Valor: " + novoPedido.ValorTotal.ToString("N2"), 4);
                Log.Yellow("Emitindo a Nota Fiscal do Pedido", 4);
                Log.NewLine();

                conexao.AppendToStreamAsync(
                    RelacaoDePedidos, 
                    ExpectedVersion.Any, 
                    GerarEvento(new NotaFiscalEmitida(novoPedido.NomeDoCliente, novoPedido.ValorTotal, "0001.0292.2999-2881-9918.11.9999/99"))).Wait();

                UltimoEventoProcessado = dadosDoEvento.Event.EventNumber;
            }
        }

        private static EventData GerarEvento(Evento evento)
        {
            return new EventData(evento.Id, evento.GetType().Name, true, Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(evento)), null);
        }

        private static TEvento Extrair<TEvento>(ResolvedEvent dadosDoEvento) where TEvento : Evento
        {
            return JsonConvert.DeserializeObject<TEvento>(Encoding.UTF8.GetString(dadosDoEvento.Event.Data));
        }
    }
}