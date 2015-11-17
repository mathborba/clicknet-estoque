namespace Engefibra.Data.Migrations
{
    using Engefibra.Data.Models;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<Engefibra.Data.Context.AppContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            ContextKey = "Engefibra.Data.Context.AppContext";
        }

        protected override void Seed(Engefibra.Data.Context.AppContext context)
        {
        }
    }
}