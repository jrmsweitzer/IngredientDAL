namespace IngredientDAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedNutritionColumnsToIngredientTable : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Ingredient", "ServingSizeQuantity", c => c.Double(nullable: false));
            AddColumn("dbo.Ingredient", "ServingSizeUnits", c => c.String());
            AddColumn("dbo.Ingredient", "CaloriesPerServing", c => c.Double(nullable: false));
            AddColumn("dbo.Ingredient", "FatPerServing", c => c.Double(nullable: false));
            AddColumn("dbo.Ingredient", "CholesterolPerServing", c => c.Double(nullable: false));
            AddColumn("dbo.Ingredient", "SodiumPerServing", c => c.Double(nullable: false));
            AddColumn("dbo.Ingredient", "PotassiumPerServing", c => c.Double(nullable: false));
            AddColumn("dbo.Ingredient", "CarbohydratesPerServing", c => c.Double(nullable: false));
            AddColumn("dbo.Ingredient", "SugarsPerServing", c => c.Double(nullable: false));
            AddColumn("dbo.Ingredient", "ProteinPerServing", c => c.Double(nullable: false));
            AddColumn("dbo.Ingredient", "HasFoundNutrients", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Ingredient", "HasFoundNutrients");
            DropColumn("dbo.Ingredient", "ProteinPerServing");
            DropColumn("dbo.Ingredient", "SugarsPerServing");
            DropColumn("dbo.Ingredient", "CarbohydratesPerServing");
            DropColumn("dbo.Ingredient", "PotassiumPerServing");
            DropColumn("dbo.Ingredient", "SodiumPerServing");
            DropColumn("dbo.Ingredient", "CholesterolPerServing");
            DropColumn("dbo.Ingredient", "FatPerServing");
            DropColumn("dbo.Ingredient", "CaloriesPerServing");
            DropColumn("dbo.Ingredient", "ServingSizeUnits");
            DropColumn("dbo.Ingredient", "ServingSizeQuantity");
        }
    }
}
