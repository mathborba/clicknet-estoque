namespace Engefibra.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RelatoriosObra : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ObraAtividadeDiaria",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ObraId = c.Int(nullable: false),
                        DataRegistro = c.DateTime(nullable: false),
                        Atividade = c.String(),
                        Supervisor = c.String(),
                        DataCriacao = c.DateTime(nullable: false),
                        UsuarioCriacao = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Obra", t => t.ObraId)
                .Index(t => t.ObraId);
            
            CreateTable(
                "dbo.ObraBobinaDiario",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ObraId = c.Int(nullable: false),
                        DataRegistro = c.DateTime(nullable: false),
                        Supervisor = c.String(),
                        Observacao = c.String(),
                        DataCriacao = c.DateTime(nullable: false),
                        UsuarioCriacao = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Obra", t => t.ObraId)
                .Index(t => t.ObraId);
            
            CreateTable(
                "dbo.ObraGrampeamentoRegulacao",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ObraId = c.Int(nullable: false),
                        DataRegistro = c.DateTime(nullable: false),
                        Supervisor = c.String(),
                        Temperatura = c.Int(nullable: false),
                        FabricanteEsticador = c.String(),
                        FabricanteEsticadorModelo = c.String(),
                        EsticadorDiametroCava = c.Int(nullable: false),
                        EsticadorNumeroParafusos = c.Int(nullable: false),
                        ApertoParafusosEsticador = c.Boolean(nullable: false),
                        Observacao = c.String(),
                        DataCriacao = c.DateTime(nullable: false),
                        UsuarioCriacao = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Obra", t => t.ObraId)
                .Index(t => t.ObraId);
            
            AddColumn("dbo.Pessoa", "Email", c => c.String());
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ObraGrampeamentoRegulacao", "ObraId", "dbo.Obra");
            DropForeignKey("dbo.ObraBobinaDiario", "ObraId", "dbo.Obra");
            DropForeignKey("dbo.ObraAtividadeDiaria", "ObraId", "dbo.Obra");
            DropIndex("dbo.ObraGrampeamentoRegulacao", new[] { "ObraId" });
            DropIndex("dbo.ObraBobinaDiario", new[] { "ObraId" });
            DropIndex("dbo.ObraAtividadeDiaria", new[] { "ObraId" });
            DropColumn("dbo.Pessoa", "Email");
            DropTable("dbo.ObraGrampeamentoRegulacao");
            DropTable("dbo.ObraBobinaDiario");
            DropTable("dbo.ObraAtividadeDiaria");
        }
    }
}
