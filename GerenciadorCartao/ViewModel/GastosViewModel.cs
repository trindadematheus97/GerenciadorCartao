using GerenciadorCartao.Models;
using System.Collections.Generic;

namespace GerenciadorCartao.ViewModel
{
    public class GastosViewModel
    {
        public int CartaoId { get; set; }

        public string NumeroCartao { get; set; }

        public List<Gasto> ListaGastos { get; set; }

        public int PorcentagemGasta { get; set; }
    }
}
