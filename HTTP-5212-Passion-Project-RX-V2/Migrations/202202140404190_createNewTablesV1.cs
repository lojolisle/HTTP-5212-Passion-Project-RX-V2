namespace HTTP_5212_Passion_Project_RX_V2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class createNewTablesV1 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Drugs",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        DrugName = c.String(),
                        Dosage = c.String(),
                        Formulation = c.Int(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.PrescriptionDrugs",
                c => new
                    {
                        PrescriptionID = c.Int(nullable: false),
                        DrugID = c.Int(nullable: false),
                        Quantity = c.Int(nullable: false),
                        Repeat = c.Int(nullable: false),
                        Sig = c.String(),
                    })
                .PrimaryKey(t => new { t.PrescriptionID, t.DrugID })
                .ForeignKey("dbo.Drugs", t => t.DrugID, cascadeDelete: true)
                .ForeignKey("dbo.Prescriptions", t => t.PrescriptionID, cascadeDelete: true)
                .Index(t => t.PrescriptionID)
                .Index(t => t.DrugID);
            
            CreateTable(
                "dbo.Prescriptions",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        FirstName = c.String(),
                        LastName = c.String(),
                        DoctorName = c.String(),
                        CreatedDate = c.DateTime(nullable: false),
                        Status = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.PrescriptionDrugs", "PrescriptionID", "dbo.Prescriptions");
            DropForeignKey("dbo.PrescriptionDrugs", "DrugID", "dbo.Drugs");
            DropIndex("dbo.PrescriptionDrugs", new[] { "DrugID" });
            DropIndex("dbo.PrescriptionDrugs", new[] { "PrescriptionID" });
            DropTable("dbo.Prescriptions");
            DropTable("dbo.PrescriptionDrugs");
            DropTable("dbo.Drugs");
        }
    }
}
