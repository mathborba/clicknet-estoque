using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engefibra.Data.Models
{
    [Table("PessoaTipo")]
    public class PessoaTipo
    {
        [Key]
        public int Id { get; set; }
        public string Nome { get; set; }
        // Novos campos
        [Display(Name = "Usuário Criação")]
        public int UsuarioCriacao { get; set; }
        [Display(Name = "Usuario Alteração")]
        public int UsuarioAlteracao { get; set; }
    }
}
