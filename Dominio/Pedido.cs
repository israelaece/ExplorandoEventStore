using System;

namespace Dominio
{
    public class Pedido
    {
        public Pedido()
        {
            this.Data = DateTime.Now;
            this.Id = Guid.NewGuid();
        }

        public string NomeDoCliente { get; set; }

        public DateTime Data { get; private set; }

        public decimal ValorTotal { get; set; }

        public Guid Id { get; private set; }
    }
}