using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engefibra.Data.Models
{
    [Table("Produto")]
    public class Produto
    {
        [Key]
        public int Id { get; set; }
        [Display(Name = "Cod. Fornecedor")]
        public string CodFornecedor { get; set; }
        public string Nome { get; set; }
        public string Marca { get; set; }
        public string Fornecedor { get; set; }
        [Display(Name = "Observação")]
        public string Observacao { get; set; }
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

        [NotMapped]
        public int SaldoAtual { get; set; }
    }
}
