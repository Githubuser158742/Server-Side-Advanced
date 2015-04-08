namespace WebshopEF.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class init2 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Baskets",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        User = c.String(),
                        Timestamp = c.DateTime(nullable: false),
                        Aantal = c.Int(nullable: false),
                        IsOrdered = c.Boolean(nullable: false),
                        Device_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Devices", t => t.Device_ID)
                .Index(t => t.Device_ID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Baskets", "Device_ID", "dbo.Devices");
            DropIndex("dbo.Baskets", new[] { "Device_ID" });
            DropTable("dbo.Baskets");
        }
    }
}
