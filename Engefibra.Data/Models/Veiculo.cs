using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engefibra.Data.Models
{
    [Table("Veiculo")]
    public class Veiculo
    {
        [Key]
        public int Id { get; set; }
        public string Modelo { get; set; }
        [Display(Name="Ano de Fabricação")]
        public string AnoFabricacao { get; set; }
        public string Cor { get; set; }
        public string Placa { get; set; }
        public string Marca { get; set; }
        [Display(Name="Kilometragem Inicial")]
        public double KmInicial { get; set; }
        [Display(Name = "Observação")]
        public string Observacao { get; set; }
        [Display(Name = "Notif. Manutenção")]
        public bool AlertarManutencao { get; set; }
        [Display(Name = "Notif. Troca de Óleo")]
        public bool AlertarTrocaOleo { get; set; }
        [Display(Name = "Notif. Manutenção Imediata")]
        public bool AlertarManutencaoImediata { get; set; }
        [Display(Name="Alerta")]
        [ForeignKey("VeiculoAlerta")]
        public int VeiculoAlertaId { get; set; }
        public VeiculoAlerta VeiculoAlerta { get; set; }
        // Novos campos
        [Display(Name = "Usuário Criação")]
        public int UsuarioCriacao { get; set; }
        [Display(Name = "Usuario Alteração")]
        public int UsuarioAlteracao { get; set; }
    }
}
