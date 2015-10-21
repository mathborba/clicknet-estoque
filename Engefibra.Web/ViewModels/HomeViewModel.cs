using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Engefibra.Data.Models;

namespace Engefibra.Web.ViewModels
{
    public class HomeViewModel
    {
        public List<Obra> Obras { get; set; }
        public List<Veiculo> Veiculo { get; set; }
        public List<Produto> Produtos { get; set; }
        public List<EstoqueMovimento> Movimentacoes { get; set; }

        public HomeViewModel()
        {
            Obras = new List<Obra>();
            Veiculo = new List<Veiculo>();
            Produtos = new List<Produto>();
            Movimentacoes = new List<EstoqueMovimento>();
        }
    }
}