using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Engefibra.Data.Models;

namespace Engefibra.Web.ViewModels
{
    public class ProdutoViewModel
    {
        public Produto Produto { get; set; }
        public int SaldoProduto { get; set; }
        public List<EstoqueMovimento> Movimentacoes { get; set; }

        public ProdutoViewModel()
        {
            Produto = new Produto();
            Movimentacoes = new List<EstoqueMovimento>();
        }
    }
}