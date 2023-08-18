namespace InventoryMasterNew.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ModuleAddedNew1 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Items", "ItemType", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Items", "ItemType", c => c.Int(nullable: false));
        }
    }
}
