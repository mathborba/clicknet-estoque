using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engefibra.Data.Models
{
    [Table("ObraGrampeamentoRegulacao")]
    public class ObraGrampeamentoRegulacao
    {
        [Key]
        public int Id { get; set; }
        [ForeignKey("Obra")]
        public int ObraId { get; set; }
        public Obra Obra { get; set; }
        [Display(Name="Data do evento")]
        public DateTime DataRegistro { get; set; }
        public string Supervisor { get; set; }
        public int Temperatura { get; set; }
        [Display(Name="Fabricante Esticador")]
        public string FabricanteEsticador { get; set; }
        [Display(Name = "Modelo Esticador")]
        public string FabricanteEsticadorModelo { get; set; }
        [Display(Name = "Diâmetro de Cava")]
        public int EsticadorDiametroCava { get; set; }
        [Display(Name = "Nº Parafusos")]
        public int EsticadorNumeroParafusos { get; set; }
        [Display(Name = "Aperto dos parafusos (do centro / das extremidades)")]
        public bool ApertoParafusosEsticador { get; set; }
        public string Observacao { get; set; }
        [Display(Name = "Data de Criação")]
        public DateTime DataCriacao { get; set; }
        [Display(Name = "Usuário Criação")]
        public int UsuarioCriacao { get; set; }
    }
}
