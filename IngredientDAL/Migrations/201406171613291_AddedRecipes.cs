namespace IngredientDAL.Migrations
{
    using System.Data.Entity.Migrations;
    
    public partial class AddedRecipes : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.RecipeItem",
                c => new
                    {
                        RecipeItemID = c.Int(nullable: false, identity: true),
                        StepID = c.Int(nullable: false),
                        IngredientID = c.Int(nullable: false),
                        IngredientQuantity = c.Double(nullable: false),
                        IngredientUnit = c.String(),
                    })
                .PrimaryKey(t => t.RecipeItemID)
                .ForeignKey("dbo.Ingredient", t => t.IngredientID, cascadeDelete: true)
                .ForeignKey("dbo.Step", t => t.StepID, cascadeDelete: true)
                .Index(t => t.StepID)
                .Index(t => t.IngredientID);
            
            CreateTable(
                "dbo.Step",
                c => new
                    {
                        StepID = c.Int(nullable: false, identity: true),
                        RecipeID = c.Int(nullable: false),
                        StepNum = c.Int(nullable: false),
                        StepInstructions = c.String(),
                    })
                .PrimaryKey(t => t.StepID)
                .ForeignKey("dbo.Recipe", t => t.RecipeID, cascadeDelete: true)
                .Index(t => t.RecipeID);
            
            CreateTable(
                "dbo.Recipe",
                c => new
                    {
                        RecipeID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.RecipeID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.RecipeItem", "StepID", "dbo.Step");
            DropForeignKey("dbo.Step", "RecipeID", "dbo.Recipe");
            DropForeignKey("dbo.RecipeItem", "IngredientID", "dbo.Ingredient");
            DropIndex("dbo.Step", new[] { "RecipeID" });
            DropIndex("dbo.RecipeItem", new[] { "IngredientID" });
            DropIndex("dbo.RecipeItem", new[] { "StepID" });
            DropTable("dbo.Recipe");
            DropTable("dbo.Step");
            DropTable("dbo.RecipeItem");
        }
    }
}
