using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engefibra.Bll
{
    public static class EstoqueMovimento
    {
        public static List<Data.Models.EstoqueMovimento> GetMovimentacaoProduto(int produtoId)
        {
            using (var db = new Data.Context.AppContext())
            {
                return db.EstoqueMovimento
                    .Where(x => x.ProdutoId == produtoId && x.Ativo == true)
                    .OrderByDescending(x => x.Id).ToList();
            }
        }
        public static int GetSaldoAtualProduto(int produtoId)
        {
            using (var db = new Data.Context.AppContext())
            {
                int saldoProduto = db.EstoqueMovimento.Where(x => x.ProdutoId == produtoId && x.Ativo == true)
                    .Sum(x => (x.Quantidade * x.MovimentoTipo));

                return saldoProduto;
            }
        }
    }
}
