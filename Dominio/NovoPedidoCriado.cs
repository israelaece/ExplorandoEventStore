using System;

namespace Dominio
{
    public class NovoPedidoCriado : Evento
    {
        public NovoPedidoCriado(Pedido pedido)
        {
            this.NomeDoCliente = pedido.NomeDoCliente;
            this.Data = pedido.Data;
            this.ValorTotal = pedido.ValorTotal;
        }

        public string NomeDoCliente { get; set; }

        public DateTime Data { get; set; }

        public decimal ValorTotal { get; set; }

        public NovoPedidoCriado() { }
    }
}