namespace _2013114400_PER.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddInitialModel : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Asientos",
                c => new
                    {
                        AsientoId = c.Int(nullable: false, identity: true),
                        NumSerie = c.String(nullable: false, maxLength: 100),
                        CarroId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.AsientoId)
                .ForeignKey("dbo.Carro", t => t.CarroId, cascadeDelete: true)
                .Index(t => t.CarroId);
            
            CreateTable(
                "dbo.Carro",
                c => new
                    {
                        CarroId = c.Int(nullable: false),
                        EnsambladoraId = c.Int(nullable: false),
                        TipoCarro = c.Int(nullable: false),
                        NumSerieChasis = c.String(nullable: false, maxLength: 100),
                        NumSerieMotor = c.String(nullable: false, maxLength: 100),
                        AutomovilId = c.Int(),
                        TipoAuto = c.Int(),
                        BusId = c.Int(),
                        TipoBus = c.Int(),
                        Discriminator = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.CarroId)
                .ForeignKey("dbo.Ensambladoras", t => t.EnsambladoraId, cascadeDelete: true)
                .ForeignKey("dbo.Propietario", t => t.CarroId)
                .Index(t => t.CarroId)
                .Index(t => t.EnsambladoraId);
            
            CreateTable(
                "dbo.Parabrisas",
                c => new
                    {
                        ParabrisasId = c.Int(nullable: false, identity: true),
                        NumSerie = c.String(nullable: false, maxLength: 100),
                        Carro_CarroId = c.Int(),
                    })
                .PrimaryKey(t => t.ParabrisasId)
                .ForeignKey("dbo.Carro", t => t.Carro_CarroId)
                .Index(t => t.Carro_CarroId);
            
            CreateTable(
                "dbo.Ensambladoras",
                c => new
                    {
                        EnsambladoraId = c.Int(nullable: false, identity: true),
                        Nombre = c.String(),
                    })
                .PrimaryKey(t => t.EnsambladoraId);
            
            CreateTable(
                "dbo.Llantas",
                c => new
                    {
                        LlantaId = c.Int(nullable: false, identity: true),
                        NumSerie = c.String(nullable: false, maxLength: 100),
                        CarroId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.LlantaId)
                .ForeignKey("dbo.Carro", t => t.CarroId, cascadeDelete: true)
                .Index(t => t.CarroId);
            
            CreateTable(
                "dbo.Propietario",
                c => new
                    {
                        PropietarioId = c.Int(nullable: false, identity: true),
                        DNI = c.String(nullable: false, maxLength: 100),
                        Nombres = c.String(nullable: false, maxLength: 100),
                        Apellidos = c.String(nullable: false, maxLength: 100),
                        LicenciaConducir = c.String(nullable: false, maxLength: 100),
                    })
                .PrimaryKey(t => t.PropietarioId);
            
            CreateTable(
                "dbo.Volante",
                c => new
                    {
                        VolanteId = c.Int(nullable: false),
                        NumSerie = c.String(nullable: false, maxLength: 100),
                    })
                .PrimaryKey(t => t.VolanteId)
                .ForeignKey("dbo.Carro", t => t.VolanteId)
                .Index(t => t.VolanteId);
            
            CreateTable(
                "dbo.Cinturones",
                c => new
                    {
                        CinturonId = c.Int(nullable: false),
                        NumSerie = c.String(nullable: false, maxLength: 100),
                        Metraje = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.CinturonId)
                .ForeignKey("dbo.Asientos", t => t.CinturonId)
                .Index(t => t.CinturonId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Cinturones", "CinturonId", "dbo.Asientos");
            DropForeignKey("dbo.Volante", "VolanteId", "dbo.Carro");
            DropForeignKey("dbo.Carro", "CarroId", "dbo.Propietario");
            DropForeignKey("dbo.Llantas", "CarroId", "dbo.Carro");
            DropForeignKey("dbo.Carro", "EnsambladoraId", "dbo.Ensambladoras");
            DropForeignKey("dbo.Asientos", "CarroId", "dbo.Carro");
            DropForeignKey("dbo.Parabrisas", "Carro_CarroId", "dbo.Carro");
            DropIndex("dbo.Cinturones", new[] { "CinturonId" });
            DropIndex("dbo.Volante", new[] { "VolanteId" });
            DropIndex("dbo.Llantas", new[] { "CarroId" });
            DropIndex("dbo.Parabrisas", new[] { "Carro_CarroId" });
            DropIndex("dbo.Carro", new[] { "EnsambladoraId" });
            DropIndex("dbo.Carro", new[] { "CarroId" });
            DropIndex("dbo.Asientos", new[] { "CarroId" });
            DropTable("dbo.Cinturones");
            DropTable("dbo.Volante");
            DropTable("dbo.Propietario");
            DropTable("dbo.Llantas");
            DropTable("dbo.Ensambladoras");
            DropTable("dbo.Parabrisas");
            DropTable("dbo.Carro");
            DropTable("dbo.Asientos");
        }
    }
}
