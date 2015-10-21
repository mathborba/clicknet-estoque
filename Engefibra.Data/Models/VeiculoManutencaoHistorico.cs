using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engefibra.Data.Models
{
    [Table("VeiculoManutencaoHistorico")]
    public class VeiculoManutencaoHistorico
    {
        [Key]
        public int Id { get; set; }
        [Display(Name = "Veiculo")]
        [ForeignKey("Veiculo")]
        public int VeiculoId { get; set; }
        public Veiculo Veiculo { get; set; }
        [Display(Name = "Kilometragem Atual")]
        public double KmAtual { get; set; }
        [Display(Name = "Manutenção Realizada")]
        public bool ManutencaoRealizada { get; set; }
        [Display(Name = "Data Confirmação")]
        public DateTime DataConfirmacao { get; set; }
        [Display(Name = "Data Notificação")]
        public DateTime DataNotificacao { get; set; }
        // Novos campos
        [Display(Name = "Usuário Criação")]
        public int UsuarioCriacao { get; set; }
        [Display(Name = "Usuario Alteração")]
        public int UsuarioAlteracao { get; set; }
    }
}
