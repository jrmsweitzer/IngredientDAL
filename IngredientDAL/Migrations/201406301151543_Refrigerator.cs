namespace IngredientDAL.Migrations
{
    using System.Data.Entity.Migrations;
    
    public partial class Refrigerator : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.Product", new[] { "IngredientID" });
            DropIndex("dbo.ReceiptItem", new[] { "ProductID" });
            DropIndex("dbo.RecipeItem", new[] { "StepID" });
            DropIndex("dbo.RecipeItem", new[] { "IngredientID" });
            DropIndex("dbo.Step", new[] { "RecipeID" });
            CreateTable(
                "dbo.RefrigeratedProduct",
                c => new
                    {
                        RefrigeratedProductId = c.Int(nullable: false, identity: true),
                        RefrigeratorId = c.Int(nullable: false),
                        ProductId = c.Int(nullable: false),
                        ExpirationDate = c.DateTime(nullable: false),
                        QuantityLeft = c.Int(nullable: false),
                        UnitsLeft = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.RefrigeratedProductId)
                .ForeignKey("dbo.Product", t => t.ProductId, cascadeDelete: true)
                .ForeignKey("dbo.Refrigerator", t => t.RefrigeratorId, cascadeDelete: true)
                .Index(t => t.RefrigeratorId)
                .Index(t => t.ProductId);
            
            CreateTable(
                "dbo.Refrigerator",
                c => new
                    {
                        RefrigeratorId = c.Int(nullable: false, identity: true),
                    })
                .PrimaryKey(t => t.RefrigeratorId);
            
            CreateIndex("dbo.Product", "IngredientId");
            CreateIndex("dbo.ReceiptItem", "ProductId");
            CreateIndex("dbo.RecipeItem", "StepId");
            CreateIndex("dbo.RecipeItem", "IngredientId");
            CreateIndex("dbo.Step", "RecipeId");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.RefrigeratedProduct", "RefrigeratorId", "dbo.Refrigerator");
            DropForeignKey("dbo.RefrigeratedProduct", "ProductId", "dbo.Product");
            DropIndex("dbo.Step", new[] { "RecipeId" });
            DropIndex("dbo.RecipeItem", new[] { "IngredientId" });
            DropIndex("dbo.RecipeItem", new[] { "StepId" });
            DropIndex("dbo.RefrigeratedProduct", new[] { "ProductId" });
            DropIndex("dbo.RefrigeratedProduct", new[] { "RefrigeratorId" });
            DropIndex("dbo.ReceiptItem", new[] { "ProductId" });
            DropIndex("dbo.Product", new[] { "IngredientId" });
            DropTable("dbo.Refrigerator");
            DropTable("dbo.RefrigeratedProduct");
            CreateIndex("dbo.Step", "RecipeID");
            CreateIndex("dbo.RecipeItem", "IngredientID");
            CreateIndex("dbo.RecipeItem", "StepID");
            CreateIndex("dbo.ReceiptItem", "ProductID");
            CreateIndex("dbo.Product", "IngredientID");
        }
    }
}
