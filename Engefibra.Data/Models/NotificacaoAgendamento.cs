using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engefibra.Data.Models
{
    [Table("NotificacaoAgendamento")]
    public class NotificacaoAgendamento
    {
        [Key]
        public int Id { get; set; }
        [Display(Name = "Grupo de Notificação")]
        [ForeignKey("Notificacao")]
        public int NotificacaoId { get; set; }
        public Notificacao Notificacao { get; set; }
        [Display(Name = "Agendar em Horas?")]
        public bool AgendamentoHoras { get; set; }
        [Display(Name = "Periodo (Horas ou Dias)")]
        public int AgendamentoTempo { get; set; }
        // Novos campos
        [Display(Name = "Usuário Criação")]
        public int UsuarioCriacao { get; set; }
        [Display(Name = "Usuario Alteração")]
        public int UsuarioAlteracao { get; set; }
    }
}
