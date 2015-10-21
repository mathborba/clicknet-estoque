using Engefibra.Data.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engefibra.Data.Context
{
    public class AppContext : DbContext
    {
        public AppContext()
            : base("EngefibraAppContext")
        {
            Database.SetInitializer<AppContext>(new CreateDatabaseIfNotExists<AppContext>());
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            // Remover Um-para-muitos deletar em cascata
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
            modelBuilder.Entity<Pessoa>()
                .HasKey(t => t.Id);

            modelBuilder.Entity<Obra>()
                .HasRequired(f => f.Encarregado);

            modelBuilder.Entity<Obra>()
                .HasRequired(f => f.Cliente);

            // Propriedades
            modelBuilder.Properties<decimal>().Configure(prop => prop.HasPrecision(15, 2));
        }

        public DbSet<Contato> Contato { get; set; }
        public DbSet<Estoque> Estoque { get; set; }
        public DbSet<EstoqueMovimento> EstoqueMovimento { get; set; }
        public DbSet<Notificacao> Notificacao { get; set; }
        public DbSet<NotificacaoAgendamento> NotificacaoAgendamento { get; set; }
        public DbSet<NotificacaoTipo> NotificacaoTipo { get; set; }
        public DbSet<Obra> Obra { get; set; }
        public DbSet<ObraNotificacao> ObraNotificacao { get; set; }
        public DbSet<ObraStatus> ObraStatus { get; set; }
        public DbSet<ObraTipo> ObraTipo { get; set; }
        public DbSet<Perfil> Perfil { get; set; }
        public DbSet<Pessoa> Pessoa { get; set; }
        public DbSet<PessoaContato> PessoaContato { get; set; }
        public DbSet<PessoaEndereco> PessoaEndereco { get; set; }
        public DbSet<PessoaTipo> PessoaTipo { get; set; }
        public DbSet<Produto> Produto { get; set; }
        public DbSet<Usuario> Usuario { get; set; }
        public DbSet<UsuarioPerfil> UsuarioPerfil { get; set; }
        public DbSet<Veiculo> Veiculo { get; set; }
        public DbSet<VeiculoAlerta> VeiculoAlertas { get; set; }
        public DbSet<VeiculoUtilizacao> VeiculoUtilizacao { get; set; }
        public DbSet<VeiculoManutencaoHistorico> VeiculoManutencaoHistorico { get; set; }
        public DbSet<VeiculoUtilizacaoStatus> VeiculoUtilizacaoStatus { get; set; }
    }
}
