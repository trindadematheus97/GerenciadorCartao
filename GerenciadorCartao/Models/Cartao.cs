using System.Collections.Generic;

namespace GerenciadorCartao.Models
{
    public class Cartao
    {
        public int CartaoId { get; set; }

        public string NomeBanco { get; set; }

        public string NumeroCartao { get; set; }

        public double Limite { get; set; }

        public ICollection<Gasto> Gastos { get; set; }
    }
}
