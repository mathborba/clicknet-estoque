namespace Engefibra.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Inicial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Contato",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Nome = c.String(),
                        Referencia = c.String(),
                        Telefone = c.String(),
                        Email = c.String(),
                        Observacao = c.String(),
                        Ativo = c.Boolean(nullable: false),
                        DataCriacao = c.DateTime(nullable: false),
                        DataAlteracao = c.DateTime(nullable: false),
                        UsuarioCriacao = c.Int(nullable: false),
                        UsuarioAlteracao = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Estoque",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Nome = c.String(),
                        Ativo = c.Boolean(nullable: false),
                        DataCriacao = c.DateTime(nullable: false),
                        DataAlteracao = c.DateTime(nullable: false),
                        UsuarioCriacao = c.Int(nullable: false),
                        UsuarioAlteracao = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.EstoqueMovimento",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        EstoqueId = c.Int(nullable: false),
                        MovimentoTipo = c.Int(nullable: false),
                        ProdutoId = c.Int(nullable: false),
                        Quantidade = c.Int(nullable: false),
                        Ativo = c.Boolean(nullable: false),
                        DataCriacao = c.DateTime(nullable: false),
                        DataAlteracao = c.DateTime(nullable: false),
                        UsuarioCriacao = c.Int(nullable: false),
                        UsuarioAlteracao = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Estoque", t => t.EstoqueId)
                .ForeignKey("dbo.Produto", t => t.ProdutoId)
                .Index(t => t.EstoqueId)
                .Index(t => t.ProdutoId);
            
            CreateTable(
                "dbo.Produto",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CodFornecedor = c.String(),
                        Nome = c.String(),
                        Marca = c.String(),
                        Fornecedor = c.String(),
                        Observacao = c.String(),
                        Ativo = c.Boolean(nullable: false),
                        DataCriacao = c.DateTime(nullable: false),
                        DataAlteracao = c.DateTime(nullable: false),
                        UsuarioCriacao = c.Int(nullable: false),
                        UsuarioAlteracao = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Notificacao",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Nome = c.String(),
                        Ativo = c.Boolean(nullable: false),
                        DataCriacao = c.DateTime(nullable: false),
                        DataAlteracao = c.DateTime(nullable: false),
                        UsuarioCriacao = c.Int(nullable: false),
                        UsuarioAlteracao = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.NotificacaoAgendamento",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        NotificacaoId = c.Int(nullable: false),
                        AgendamentoHoras = c.Boolean(nullable: false),
                        AgendamentoTempo = c.Int(nullable: false),
                        UsuarioCriacao = c.Int(nullable: false),
                        UsuarioAlteracao = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Notificacao", t => t.NotificacaoId)
                .Index(t => t.NotificacaoId);
            
            CreateTable(
                "dbo.NotificacaoTipo",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Nome = c.String(),
                        Ativo = c.Boolean(nullable: false),
                        UsuarioCriacao = c.Int(nullable: false),
                        UsuarioAlteracao = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Obra",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Nome = c.String(),
                        EncarregadoId = c.Int(nullable: false),
                        ClienteId = c.Int(nullable: false),
                        Endereco = c.String(),
                        Cep = c.String(),
                        Cidade = c.String(),
                        ObraStatusId = c.Int(nullable: false),
                        DataAgendamento = c.DateTime(nullable: false),
                        Observacao = c.String(),
                        Pendencias = c.String(),
                        NotificacaoId = c.Int(nullable: false),
                        Ativo = c.Boolean(nullable: false),
                        DataCriacao = c.DateTime(nullable: false),
                        DataAlteracao = c.DateTime(nullable: false),
                        UsuarioCriacao = c.Int(nullable: false),
                        UsuarioAlteracao = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Pessoa", t => t.ClienteId)
                .ForeignKey("dbo.Pessoa", t => t.EncarregadoId)
                .ForeignKey("dbo.Notificacao", t => t.NotificacaoId)
                .ForeignKey("dbo.ObraStatus", t => t.ObraStatusId)
                .Index(t => t.ClienteId)
                .Index(t => t.EncarregadoId)
                .Index(t => t.NotificacaoId)
                .Index(t => t.ObraStatusId);
            
            CreateTable(
                "dbo.Pessoa",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Nome = c.String(),
                        Sobrenome = c.String(),
                        PessoaFisica = c.Boolean(nullable: false),
                        RgIE = c.String(),
                        CpfCnpj = c.String(),
                        PessoaTipoId = c.Int(nullable: false),
                        ObraNotificacao = c.Boolean(nullable: false),
                        VeiculoNotificacao = c.Boolean(nullable: false),
                        UsuarioCriacao = c.Int(nullable: false),
                        UsuarioAlteracao = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.PessoaTipo", t => t.PessoaTipoId)
                .Index(t => t.PessoaTipoId);
            
            CreateTable(
                "dbo.PessoaTipo",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Nome = c.String(),
                        UsuarioCriacao = c.Int(nullable: false),
                        UsuarioAlteracao = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.ObraStatus",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Nome = c.String(),
                        UsuarioCriacao = c.Int(nullable: false),
                        UsuarioAlteracao = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.ObraNotificacao",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ObraId = c.Int(nullable: false),
                        Ativo = c.Boolean(nullable: false),
                        DataCriacao = c.DateTime(nullable: false),
                        DataAlteracao = c.DateTime(nullable: false),
                        UsuarioCriacao = c.Int(nullable: false),
                        UsuarioAlteracao = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Obra", t => t.ObraId)
                .Index(t => t.ObraId);
            
            CreateTable(
                "dbo.ObraTipo",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Nome = c.String(),
                        UsuarioCriacao = c.Int(nullable: false),
                        UsuarioAlteracao = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Perfil",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Nome = c.String(),
                        Ativo = c.Boolean(nullable: false),
                        UsuarioCriacao = c.Int(nullable: false),
                        UsuarioAlteracao = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.PessoaContato",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        PessoaId = c.Int(nullable: false),
                        Nome = c.String(),
                        Email = c.String(),
                        TelefoneFixo = c.String(),
                        Ramal = c.String(),
                        Celular = c.String(),
                        UsuarioCriacao = c.Int(nullable: false),
                        UsuarioAlteracao = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Pessoa", t => t.PessoaId)
                .Index(t => t.PessoaId);
            
            CreateTable(
                "dbo.PessoaEndereco",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        PessoaId = c.Int(nullable: false),
                        Referencia = c.String(),
                        CEP = c.String(),
                        Logradouro = c.String(),
                        Numero = c.String(),
                        Cidade = c.String(),
                        UF = c.String(),
                        UsuarioCriacao = c.Int(nullable: false),
                        UsuarioAlteracao = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Pessoa", t => t.PessoaId)
                .Index(t => t.PessoaId);
            
            CreateTable(
                "dbo.Usuario",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        PessoaId = c.Int(nullable: false),
                        Login = c.String(),
                        Senha = c.String(),
                        Ativo = c.Boolean(nullable: false),
                        DataCriacao = c.DateTime(nullable: false),
                        DataAlteracao = c.DateTime(nullable: false),
                        UsuarioCriacao = c.Int(nullable: false),
                        UsuarioAlteracao = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Pessoa", t => t.PessoaId)
                .Index(t => t.PessoaId);
            
            CreateTable(
                "dbo.UsuarioPerfil",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UsuarioId = c.Int(nullable: false),
                        PerfilId = c.Int(nullable: false),
                        UsuarioCriacao = c.Int(nullable: false),
                        UsuarioAlteracao = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Perfil", t => t.PerfilId)
                .ForeignKey("dbo.Usuario", t => t.UsuarioId)
                .Index(t => t.PerfilId)
                .Index(t => t.UsuarioId);
            
            CreateTable(
                "dbo.Veiculo",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Modelo = c.String(),
                        AnoFabricacao = c.String(),
                        Cor = c.String(),
                        Placa = c.String(),
                        Marca = c.String(),
                        KmInicial = c.Double(nullable: false),
                        Observacao = c.String(),
                        AlertarManutencao = c.Boolean(nullable: false),
                        AlertarTrocaOleo = c.Boolean(nullable: false),
                        AlertarManutencaoImediata = c.Boolean(nullable: false),
                        VeiculoAlertaId = c.Int(nullable: false),
                        UsuarioCriacao = c.Int(nullable: false),
                        UsuarioAlteracao = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.VeiculoAlerta", t => t.VeiculoAlertaId)
                .Index(t => t.VeiculoAlertaId);
            
            CreateTable(
                "dbo.VeiculoAlerta",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Nome = c.String(),
                        KmOleo = c.Double(nullable: false),
                        KmManutencao = c.Double(nullable: false),
                        Ativo = c.Boolean(nullable: false),
                        UsuarioCriacao = c.Int(nullable: false),
                        UsuarioAlteracao = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.VeiculoManutencaoHistorico",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        VeiculoId = c.Int(nullable: false),
                        KmAtual = c.Double(nullable: false),
                        ManutencaoRealizada = c.Boolean(nullable: false),
                        DataConfirmacao = c.DateTime(nullable: false),
                        DataNotificacao = c.DateTime(nullable: false),
                        UsuarioCriacao = c.Int(nullable: false),
                        UsuarioAlteracao = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Veiculo", t => t.VeiculoId)
                .Index(t => t.VeiculoId);
            
            CreateTable(
                "dbo.VeiculoUtilizacao",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        VeiculoId = c.Int(nullable: false),
                        PessoaId = c.Int(nullable: false),
                        Observacao = c.String(),
                        KmFinal = c.Double(nullable: false),
                        Abastecimento = c.Boolean(nullable: false),
                        VeiculoUtilizacaoStatusId = c.Int(nullable: false),
                        DataCriacao = c.DateTime(nullable: false),
                        DataAlteracao = c.DateTime(nullable: false),
                        UsuarioCriacao = c.Int(nullable: false),
                        UsuarioAlteracao = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Pessoa", t => t.PessoaId)
                .ForeignKey("dbo.Veiculo", t => t.VeiculoId)
                .ForeignKey("dbo.VeiculoUtilizacaoStatus", t => t.VeiculoUtilizacaoStatusId)
                .Index(t => t.PessoaId)
                .Index(t => t.VeiculoId)
                .Index(t => t.VeiculoUtilizacaoStatusId);
            
            CreateTable(
                "dbo.VeiculoUtilizacaoStatus",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Nome = c.String(),
                        UsuarioCriacao = c.Int(nullable: false),
                        UsuarioAlteracao = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.VeiculoUtilizacao", "VeiculoUtilizacaoStatusId", "dbo.VeiculoUtilizacaoStatus");
            DropForeignKey("dbo.VeiculoUtilizacao", "VeiculoId", "dbo.Veiculo");
            DropForeignKey("dbo.VeiculoUtilizacao", "PessoaId", "dbo.Pessoa");
            DropForeignKey("dbo.VeiculoManutencaoHistorico", "VeiculoId", "dbo.Veiculo");
            DropForeignKey("dbo.Veiculo", "VeiculoAlertaId", "dbo.VeiculoAlerta");
            DropForeignKey("dbo.UsuarioPerfil", "UsuarioId", "dbo.Usuario");
            DropForeignKey("dbo.UsuarioPerfil", "PerfilId", "dbo.Perfil");
            DropForeignKey("dbo.Usuario", "PessoaId", "dbo.Pessoa");
            DropForeignKey("dbo.PessoaEndereco", "PessoaId", "dbo.Pessoa");
            DropForeignKey("dbo.PessoaContato", "PessoaId", "dbo.Pessoa");
            DropForeignKey("dbo.ObraNotificacao", "ObraId", "dbo.Obra");
            DropForeignKey("dbo.Obra", "ObraStatusId", "dbo.ObraStatus");
            DropForeignKey("dbo.Obra", "NotificacaoId", "dbo.Notificacao");
            DropForeignKey("dbo.Obra", "EncarregadoId", "dbo.Pessoa");
            DropForeignKey("dbo.Obra", "ClienteId", "dbo.Pessoa");
            DropForeignKey("dbo.Pessoa", "PessoaTipoId", "dbo.PessoaTipo");
            DropForeignKey("dbo.NotificacaoAgendamento", "NotificacaoId", "dbo.Notificacao");
            DropForeignKey("dbo.EstoqueMovimento", "ProdutoId", "dbo.Produto");
            DropForeignKey("dbo.EstoqueMovimento", "EstoqueId", "dbo.Estoque");
            DropIndex("dbo.VeiculoUtilizacao", new[] { "VeiculoUtilizacaoStatusId" });
            DropIndex("dbo.VeiculoUtilizacao", new[] { "VeiculoId" });
            DropIndex("dbo.VeiculoUtilizacao", new[] { "PessoaId" });
            DropIndex("dbo.VeiculoManutencaoHistorico", new[] { "VeiculoId" });
            DropIndex("dbo.Veiculo", new[] { "VeiculoAlertaId" });
            DropIndex("dbo.UsuarioPerfil", new[] { "UsuarioId" });
            DropIndex("dbo.UsuarioPerfil", new[] { "PerfilId" });
            DropIndex("dbo.Usuario", new[] { "PessoaId" });
            DropIndex("dbo.PessoaEndereco", new[] { "PessoaId" });
            DropIndex("dbo.PessoaContato", new[] { "PessoaId" });
            DropIndex("dbo.ObraNotificacao", new[] { "ObraId" });
            DropIndex("dbo.Obra", new[] { "ObraStatusId" });
            DropIndex("dbo.Obra", new[] { "NotificacaoId" });
            DropIndex("dbo.Obra", new[] { "EncarregadoId" });
            DropIndex("dbo.Obra", new[] { "ClienteId" });
            DropIndex("dbo.Pessoa", new[] { "PessoaTipoId" });
            DropIndex("dbo.NotificacaoAgendamento", new[] { "NotificacaoId" });
            DropIndex("dbo.EstoqueMovimento", new[] { "ProdutoId" });
            DropIndex("dbo.EstoqueMovimento", new[] { "EstoqueId" });
            DropTable("dbo.VeiculoUtilizacaoStatus");
            DropTable("dbo.VeiculoUtilizacao");
            DropTable("dbo.VeiculoManutencaoHistorico");
            DropTable("dbo.VeiculoAlerta");
            DropTable("dbo.Veiculo");
            DropTable("dbo.UsuarioPerfil");
            DropTable("dbo.Usuario");
            DropTable("dbo.PessoaEndereco");
            DropTable("dbo.PessoaContato");
            DropTable("dbo.Perfil");
            DropTable("dbo.ObraTipo");
            DropTable("dbo.ObraNotificacao");
            DropTable("dbo.ObraStatus");
            DropTable("dbo.PessoaTipo");
            DropTable("dbo.Pessoa");
            DropTable("dbo.Obra");
            DropTable("dbo.NotificacaoTipo");
            DropTable("dbo.NotificacaoAgendamento");
            DropTable("dbo.Notificacao");
            DropTable("dbo.Produto");
            DropTable("dbo.EstoqueMovimento");
            DropTable("dbo.Estoque");
            DropTable("dbo.Contato");
        }
    }
}
