using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engefibra.Data.Models
{
    [Table("Obra")]
    public class Obra
    {
        [Key]
        public int Id { get; set; }
        public string Nome { get; set; }
        [Display(Name = "Encarregado")]
        [ForeignKey("Encarregado")]
        public int EncarregadoId { get; set; }
        public Pessoa Encarregado { get; set; }
        [Display(Name = "Cliente")]
        [ForeignKey("Cliente")]
        public int ClienteId { get; set; }
        public Pessoa Cliente { get; set; }
        public string Endereco { get; set; }
        public string Cep { get; set; }
        public string Cidade { get; set; }
        [Display(Name = "Situação Obra")]
        [ForeignKey("ObraStatus")]
        public int ObraStatusId { get; set; }
        public ObraStatus ObraStatus { get; set; }
        [Display(Name = "Dt. Agendamento")]
        public DateTime DataAgendamento { get; set; }
        [Display(Name = "Observação")]
        public string Observacao { get; set; }
        [Display(Name = "Pendências")]
        public string Pendencias { get; set; }
        [Display(Name = "Grupo Notificação")]
        [ForeignKey("Notificacao")]
        public int NotificacaoId { get; set; }
        public Notificacao Notificacao { get; set; }
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
