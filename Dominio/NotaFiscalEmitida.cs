namespace Dominio
{
    public class NotaFiscalEmitida : Evento
    {
        public NotaFiscalEmitida(string nomeDoCliente, decimal valorTotal, string chaveDaNFe)
        {
            this.NomeDoCliente = nomeDoCliente;
            this.ValorTotal = valorTotal;
            this.ChaveDaNotaFiscalEletronica = chaveDaNFe;
        }

        public string NomeDoCliente { get; set; }

        public decimal ValorTotal { get; set; }

        public string ChaveDaNotaFiscalEletronica { get; set; }
    }
}