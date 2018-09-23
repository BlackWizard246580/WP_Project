namespace WP_Project.Migrations
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using WP_Project.DAL;
    using WP_Project.Models;

    internal sealed class Configuration : DbMigrationsConfiguration<WP_Project.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            ContextKey = "WP_Project.Models.ApplicationDbContext";
        }

        protected override void Seed(WP_Project.Models.ApplicationDbContext context)
        {
            var dbInit = new DBInitializer();
            //dbInit.SeedCategory(context);
            //dbInit.SeedItem(context);

            //dbInit.SeedCustomField(context);
            //dbInit.SeedCustomFieldValue(context);
            //dbInit.SeedCustomFieldItems(context);

        }
    }
    }
