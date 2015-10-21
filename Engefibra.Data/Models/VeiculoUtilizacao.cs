using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engefibra.Data.Models
{
    [Table("VeiculoUtilizacao")]
    public class VeiculoUtilizacao
    {
        [Key]
        public int Id { get; set; }
        [Display(Name = "Veiculo")]
        [ForeignKey("Veiculo")]
        public int VeiculoId { get; set; }
        public Veiculo Veiculo { get; set; }
        [Display(Name = "Funcionário")]
        [ForeignKey("Pessoa")]
        public int PessoaId { get; set; }
        public Pessoa Pessoa { get; set; }
        [Display(Name = "Observação")]
        public string Observacao { get; set; }
        [Display(Name = "Km. Final")]
        public double KmFinal { get; set; }
        [Display(Name = "Abastecimento?")]
        public bool Abastecimento { get; set; }
        [Display(Name = "Situação Uso")]
        [ForeignKey("VeiculoUtilizacaoStatus")]
        public int VeiculoUtilizacaoStatusId { get; set; }
        public VeiculoUtilizacaoStatus VeiculoUtilizacaoStatus { get; set; }
        [Display(Name = "Data de Criação")]
        public DateTime DataCriacao { get; set; }
        [Display(Name = "Data de atualização")]
        public DateTime DataAlteracao { get; set; }
        // Novos campos
        [Display(Name = "Usuário Criação")]
        public int UsuarioCriacao { get; set; }
        [Display(Name = "Usuario Alteração")]
        public int UsuarioAlteracao { get; set; }
    }
}
