using System;

namespace Dominio
{
    public abstract class Evento
    {
        public Evento()
        {
            this.Id = Guid.NewGuid();
        }

        public Guid Id { get; set; }
    }
}