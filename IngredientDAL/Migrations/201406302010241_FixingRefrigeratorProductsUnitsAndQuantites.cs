namespace IngredientDAL.Migrations
{
    using System.Data.Entity.Migrations;
    
    public partial class FixingRefrigeratorProductsUnitsAndQuantites : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.RefrigeratedProduct", "QuantityLeft", c => c.Double(nullable: false));
            AlterColumn("dbo.RefrigeratedProduct", "UnitsLeft", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.RefrigeratedProduct", "UnitsLeft", c => c.Int(nullable: false));
            AlterColumn("dbo.RefrigeratedProduct", "QuantityLeft", c => c.Int(nullable: false));
        }
    }
}
