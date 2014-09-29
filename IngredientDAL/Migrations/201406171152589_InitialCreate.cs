namespace IngredientDAL.Migrations
{
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Ingredient",
                c => new
                    {
                        IngredientID = c.Int(nullable: false, identity: true),
                        IngredientName = c.String(),
                    })
                .PrimaryKey(t => t.IngredientID);
            
            CreateTable(
                "dbo.Product",
                c => new
                    {
                        ProductID = c.Int(nullable: false, identity: true),
                        IngredientID = c.Int(nullable: false),
                        BrandName = c.String(),
                        ProductQuantity = c.Double(nullable: false),
                        ProductUnit = c.String(),
                    })
                .PrimaryKey(t => t.ProductID)
                .ForeignKey("dbo.Ingredient", t => t.IngredientID, cascadeDelete: true)
                .Index(t => t.IngredientID);
            
            CreateTable(
                "dbo.ReceiptItem",
                c => new
                    {
                        ReceiptItemID = c.Int(nullable: false, identity: true),
                        StoreName = c.String(),
                        ReceiptDate = c.DateTime(nullable: false),
                        ProductID = c.Int(nullable: false),
                        IngredientPrice = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.ReceiptItemID)
                .ForeignKey("dbo.Product", t => t.ProductID, cascadeDelete: true)
                .Index(t => t.ProductID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ReceiptItem", "ProductID", "dbo.Product");
            DropForeignKey("dbo.Product", "IngredientID", "dbo.Ingredient");
            DropIndex("dbo.ReceiptItem", new[] { "ProductID" });
            DropIndex("dbo.Product", new[] { "IngredientID" });
            DropTable("dbo.ReceiptItem");
            DropTable("dbo.Product");
            DropTable("dbo.Ingredient");
        }
    }
}
