using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engefibra.Data.Models
{
    [Table("ObraBobinaDiario")]
    public class ObraBobinaDiario
    {
        [Key]
        public int Id { get; set; }
        [ForeignKey("Obra")]
        public int ObraId { get; set; }
        public Obra Obra { get; set; }
        [Display(Name="Data do evento")]
        public DateTime DataRegistro { get; set; }
        public string Supervisor { get; set; }
        public string Observacao { get; set; }
        [Display(Name = "Data de Criação")]
        public DateTime DataCriacao { get; set; }
        [Display(Name = "Usuário Criação")]
        public int UsuarioCriacao { get; set; }
    }
}
