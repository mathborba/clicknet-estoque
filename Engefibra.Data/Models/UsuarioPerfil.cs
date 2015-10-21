using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engefibra.Data.Models
{
    [Table("UsuarioPerfil")]
    public class UsuarioPerfil
    {
        [Key]
        public int Id { get; set; }
        [ForeignKey("Usuario")]
        [Display(Name = "Usuário")]
        public int UsuarioId { get; set; }
        [ForeignKey("Perfil")]
        [Display(Name = "Perfil")]
        public int PerfilId { get; set; }


        [Display(Name = "Usuário Criação")]
        public int UsuarioCriacao { get; set; }
        [Display(Name = "Usuario Alteração")]
        public int UsuarioAlteracao { get; set; }

        public Usuario Usuario { get; set; }
        public Perfil Perfil { get; set; }
    }
}