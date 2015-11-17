using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engefibra.Bll
{
    public static class EstoqueMovimento
    {
        #region .: CRUD :.

        public static Data.Models.EstoqueMovimento Get(int id)
        {
            var model = new Data.Models.EstoqueMovimento();

            using(var db = new Data.Context.AppContext())
            {
                model = db.EstoqueMovimento.Include("Produto").Include("Estoque").Where(e => e.Id == id).FirstOrDefault();
            }

            return model;
        }

        public static List<Data.Models.EstoqueMovimento> GetAll(bool orderBy = false)
        {
            var model = new List<Data.Models.EstoqueMovimento>();

            using (var db = new Data.Context.AppContext())
            {
                model = db.EstoqueMovimento.Include("Produto").Include("Estoque").ToList();
                if (orderBy)
                    model = model.OrderByDescending(c => c.Id).ToList();
            }

            return model;
        }

        public static bool Add(Data.Models.EstoqueMovimento model)
        {
            bool sucesso = true;

            using (var db = new Data.Context.AppContext())
            {
                // Caso a movimentação de saida seja maior que o Saldo Atual, não deixar que ela ocorra, futuramente, retornar mensagem de erro
                if(model.MovimentoTipo == -1)
                {
                    var produtoSaldo = Bll.EstoqueMovimento.GetSaldoAtualProduto(model.ProdutoId);
                    if(produtoSaldo + (model.Quantidade * model.MovimentoTipo) < 0)
                    {
                        //TODO: Mensagem de erro
                        return false;
                    }
                }

                db.EstoqueMovimento.Add(model);
                db.SaveChanges();

                var saldoAtualProduto = Bll.EstoqueMovimento.GetSaldoAtualProduto(model.ProdutoId);
                if (saldoAtualProduto < Convert.ToInt32(Configuration.GetValue("QuantidadeEstoqueMinima")))
                {
                    Bll.EstoqueMovimento.NotificacaoSaldoProduto(model.ProdutoId);
                }
            }
            return sucesso;
        }

        public static void Delete(int id)
        {
            using (var db = new Data.Context.AppContext())
            {
                Data.Models.EstoqueMovimento model = db.EstoqueMovimento.Find(id);
                db.EstoqueMovimento.Remove(model);
                db.SaveChanges();

                var saldoAtualProduto = Bll.EstoqueMovimento.GetSaldoAtualProduto(model.ProdutoId);
                if (saldoAtualProduto < Convert.ToInt32(Configuration.GetValue("QuantidadeEstoqueMinima")))
                {
                    Bll.EstoqueMovimento.NotificacaoSaldoProduto(model.ProdutoId);
                }
            }
        }

        #endregion

        /// <summary>
        /// Retorna todas movimentações de um produto
        /// </summary>
        /// <param name="produtoId">Identificador do produto</param>
        /// <returns></returns>
        public static List<Data.Models.EstoqueMovimento> GetMovimentacaoProduto(int produtoId)
        {
            using (var db = new Data.Context.AppContext())
            {
                return db.EstoqueMovimento
                    .Include("Estoque")
                    .Where(x => x.ProdutoId == produtoId) 
                    .OrderByDescending(x => x.Id).ToList();
            }
        }

        /// <summary>
        /// Recupera o saldo atual do produto baseado nas suas movimentações
        /// </summary>
        /// <param name="produtoId">Identificador do produto.</param>
        /// <returns></returns>
        public static int GetSaldoAtualProduto(int produtoId)
        {
            using (var db = new Data.Context.AppContext())
            {
                int saldoProduto = 0;

                if (db.EstoqueMovimento.Where(x => x.ProdutoId == produtoId).ToList().Count() > 0)
                {
                    saldoProduto = db.EstoqueMovimento.Where(x => x.ProdutoId == produtoId)
                        .Sum(c => (c.Quantidade * c.MovimentoTipo));
                }
                return saldoProduto;
            }
        }

        /// <summary>
        /// Dispara a notificação do produto que o saldo está abaixo do parametro configurado
        /// </summary>
        /// <param name="produtoId">Identificador do produto</param>
        public static void NotificacaoSaldoProduto(int produtoId)
        {
            using (var db = new Data.Context.AppContext())
            {
                var produto = db.Produto.Find(produtoId);
                var destinatarios = db.Pessoa.Where(x => x.ObraNotificacao == true).Select(x => x.Email).ToList();

                var mailer = new Mailer();

                mailer.assunto = "[Estoque] Situação de estoque abaixo do mínimo: " + produto.Nome;
                mailer.destinatarios = destinatarios;
                mailer.tipoEnvioEmail = MailerSendType.Produtos;

                mailer.corpo =
                    "Após um resumo das últimas movimentações, foi constatado que este produto está abaixo da quantidade minima de estoque [" + Configuration.GetValue("QuantidadeEstoqueMinima") + "] unidades.<br />" +
                    "O produto: " + produto.Nome + " de cód. fornecedor [" + produto.CodFornecedor + "] possui saldo [" + EstoqueMovimento.GetSaldoAtualProduto(produtoId) + "] no sistema, por favor verificar.<br /><br />" +
                    "<strong>Fornecedor:</strong> " + produto.Fornecedor + ", entre em contato para consultar preços especiais.<br/>" +
                    "<strong>Observações:</strong> " + produto.Observacao;
                mailer.corpoHtml = true;

                mailer.Send();
            }
        }
    }
}