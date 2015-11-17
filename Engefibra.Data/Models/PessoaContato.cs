using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engefibra.Data.Models
{
    [Table("PessoaContato")]
    public class PessoaContato
    {
        [Key]
        public int Id { get; set; }
        [Display(Name = "Pessoa")]
        [ForeignKey("Pessoa")]
        public int PessoaId { get; set; }
        public Pessoa Pessoa { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        [Display(Name = "Tel. Fixo")]
        public string TelefoneFixo { get; set; }
        public string Ramal { get; set; }
        public string Celular { get; set; }
        // Novos campos
        [Display(Name = "Usuário Criação")]
        public int UsuarioCriacao { get; set; }
        [Display(Name = "Usuario Alteração")]
        public int UsuarioAlteracao { get; set; }
    }
}
