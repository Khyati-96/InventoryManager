namespace InventoryMasterNew.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ModuleAddedNew : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Aisles",
                c => new
                    {
                        AisleId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Desc = c.String(),
                        AisleCap = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.AisleId);
            
            CreateTable(
                "dbo.Items",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ItemName = c.String(),
                        ItemType = c.Int(nullable: false),
                        ItemCount = c.Int(nullable: false),
                        BBD = c.DateTime(nullable: false, storeType: "date"),
                        AisleId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Aisles", t => t.AisleId, cascadeDelete: true)
                .Index(t => t.AisleId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Items", "AisleId", "dbo.Aisles");
            DropIndex("dbo.Items", new[] { "AisleId" });
            DropTable("dbo.Items");
            DropTable("dbo.Aisles");
        }
    }
}
