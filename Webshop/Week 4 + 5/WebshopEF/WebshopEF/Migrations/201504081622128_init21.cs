namespace WebshopEF.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class init21 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Orders",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Naam = c.String(nullable: false),
                        Voornaam = c.String(nullable: false),
                        Adres = c.String(nullable: false),
                        Postcode = c.String(nullable: false),
                        Zipcode = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.OrderLines",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Basket_ID = c.Int(),
                        Order_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Baskets", t => t.Basket_ID)
                .ForeignKey("dbo.Orders", t => t.Order_ID)
                .Index(t => t.Basket_ID)
                .Index(t => t.Order_ID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.OrderLines", "Order_ID", "dbo.Orders");
            DropForeignKey("dbo.OrderLines", "Basket_ID", "dbo.Baskets");
            DropIndex("dbo.OrderLines", new[] { "Order_ID" });
            DropIndex("dbo.OrderLines", new[] { "Basket_ID" });
            DropTable("dbo.OrderLines");
            DropTable("dbo.Orders");
        }
    }
}
