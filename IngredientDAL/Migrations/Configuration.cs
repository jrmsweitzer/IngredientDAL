namespace IngredientDAL.Migrations
{
    using Bots;
    using DAL;
    using System.Data.Entity.Migrations;

    internal sealed class Configuration : DbMigrationsConfiguration<IngredientContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            AutomaticMigrationDataLossAllowed = true;
        }

        protected override void Seed(IngredientContext context)
        {
            #region tutorial stuff
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //
            #endregion

            var dalbot = new DalBot(new IngredientContext());

            dalbot.AddIngredient("Tomato");
            dalbot.AddIngredient("Rice Chex");
            dalbot.AddIngredient("Vanilla Chex");
            dalbot.AddIngredient("Quinoa");
        }
    }
}
