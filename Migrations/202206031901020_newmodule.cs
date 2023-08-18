namespace InventoryMasterNew.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class newmodule : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ItemDtoes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ItemName = c.String(),
                        ItemType = c.String(),
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
            DropForeignKey("dbo.ItemDtoes", "AisleId", "dbo.Aisles");
            DropIndex("dbo.ItemDtoes", new[] { "AisleId" });
            DropTable("dbo.ItemDtoes");
        }
    }
}
