using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engefibra.Data.Models
{
    [Table("Pessoa")]
    public class Pessoa
    {
        [Key]
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Sobrenome { get; set; }
        [Display(Name = "Pessoa Fisica?")]
        public bool PessoaFisica { get; set; }
        [Display(Name = "Rg/IE")]
        public string RgIE { get; set; }
        [Display(Name = "Cpf/Cnpj")]
        public string CpfCnpj { get; set; }
        [Display(Name = "Tipo Pessoa")]
        [ForeignKey("PessoaTipo")]
        public int PessoaTipoId { get; set; }
        public PessoaTipo PessoaTipo { get; set; }
        [Display(Name = "Notificar Obra")]
        public bool ObraNotificacao { get; set; }
        [Display(Name = "Notificar Veiculo")]
        public bool VeiculoNotificacao { get; set; }
        // Novos campos
        [Display(Name = "Usuário Criação")]
        public int UsuarioCriacao { get; set; }
        [Display(Name = "Usuario Alteração")]
        public int UsuarioAlteracao { get; set; }
    }
}
