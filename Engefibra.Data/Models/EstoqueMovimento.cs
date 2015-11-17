using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engefibra.Data.Models
{
    [Table("EstoqueMovimento")]
    public class EstoqueMovimento
    {
        [Key]
        public int Id { get; set; }
        [Display(Name = "Estoque")]
        [ForeignKey("Estoque")]
        public int EstoqueId { get; set; }
        public Estoque Estoque { get; set; }
        [Display(Name = "Movimentação")]
        public int MovimentoTipo { get; set; }
        [Display(Name = "Produto")]
        [ForeignKey("Produto")]
        public int ProdutoId { get; set; }
        public Produto Produto { get; set; }
        public int Quantidade { get; set; }
        public bool Ativo { get; set; }
        [Display(Name = "Data de Criação")]
        public DateTime DataCriacao { get; set; }
        [Display(Name = "Data de Alteração")]
        public DateTime DataAlteracao { get; set; }
        // Novos campos
        [Display(Name = "Usuário Criação")]
        public int UsuarioCriacao { get; set; }
        [Display(Name = "Usuario Alteração")]
        public int UsuarioAlteracao { get; set; }
    }
}
