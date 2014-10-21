namespace IngredientDAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ReceiptTextOnProduct : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Product", "ProductReceiptText", c => c.String());
            DropColumn("dbo.ReceiptItem", "ProductReceiptText");
        }
        
        public override void Down()
        {
            AddColumn("dbo.ReceiptItem", "ProductReceiptText", c => c.String());
            DropColumn("dbo.Product", "ProductReceiptText");
        }
    }
}
