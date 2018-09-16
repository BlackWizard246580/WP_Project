﻿using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using WP_Project.Models;

namespace WP_Project.DAL
{
    public class DBInitializer
    {
        public DBInitializer()
        {

        }
        public class DbInitializer : CreateDatabaseIfNotExists<ApplicationDbContext>
        {
            public DbInitializer()
            {
            }

        }

        internal void SeedCustomFieldValue(ApplicationDbContext context)
        {
            CustomField size = context.CustomField.Find(1);
            CustomField sauce = context.CustomField.Find(2);
            var customfieldvalues = new List<CustomFieldValue>
            {
            new CustomFieldValue{CustomFieldValueName="Medium", AdditionalPrice=0, CustomField=size},
            new CustomFieldValue{CustomFieldValueName="Large", AdditionalPrice=1000, CustomField=size},

            new CustomFieldValue{CustomFieldValueName="No Sauce", AdditionalPrice=0, CustomField=size},
            new CustomFieldValue{CustomFieldValueName="Tomato Sauce", AdditionalPrice=600, CustomField=size},
            new CustomFieldValue{CustomFieldValueName="BBQ Sauce", AdditionalPrice=1000, CustomField=size},
            new CustomFieldValue{CustomFieldValueName="Extra Tomato Sauce", AdditionalPrice=1000, CustomField=size},
            new CustomFieldValue{CustomFieldValueName="Extra BBQ Sauce", AdditionalPrice=1400, CustomField=size},
            };
            customfieldvalues.ForEach(s => context.CustomFieldValue.Add(s));
            context.SaveChanges();
        }

        internal void SeedCustomField(ApplicationDbContext context)
        {
            var customfields = new List<CustomField>
            {
            new CustomField{CustomFieldName="Size"},
            new CustomField{CustomFieldName="Sauce"},
            };
            customfields.ForEach(s => context.CustomField.Add(s));
            context.SaveChanges();
        }

        internal void SeedItem(ApplicationDbContext context)
        {
            Category pizzas = context.Category.Find(1);
            Category sides = context.Category.Find(2);
            Category wings = context.Category.Find(3);
            Category desserts = context.Category.Find(4);
            Category drinks = context.Category.Find(5);
            var items = new List<Item>
            {
            new Item{ItemName="Chicken Supreme", ItemPrice=11000, Category=pizzas},
            new Item{ItemName="Cheese Lovers", ItemPrice=6500, Category=pizzas},
            new Item{ItemName="Ham Lovers", ItemPrice=11000, Category=pizzas},
            new Item{ItemName="BBQ Beef", ItemPrice=13500, Category=pizzas},
            new Item{ItemName="Ultimate Hot & Spicy", ItemPrice=9000, Category=pizzas},

            new Item{ItemName="Garlic Bread", ItemPrice=2500, Category=sides},
            new Item{ItemName="Cheesey Garlic Bread", ItemPrice=3000, Category=sides},
            new Item{ItemName="Spud Bites and Tomato Sauce Dip", ItemPrice=2500, Category=sides},
            new Item{ItemName="Triple Dippers", ItemPrice=13500, Category=sides},
            new Item{ItemName="BBQ Pork Ribs – Quarter Rack", ItemPrice=9000, Category=sides},

            new Item{ItemName="Buffalo Wings 6 Pack", ItemPrice=5000, Category=wings},
            new Item{ItemName="Naked Wings 6 Pack", ItemPrice=5500, Category=wings},
            new Item{ItemName="Seasoned Wings 6 Pack", ItemPrice=5000, Category=wings},
            new Item{ItemName="Buffalo Wings 10 Pack", ItemPrice=9000, Category=wings},
            new Item{ItemName="Naked Wings 10 Pack", ItemPrice=9500, Category=wings},
            new Item{ItemName="Seasoned Wings 10 Pack", ItemPrice=9000, Category=wings},

            new Item{ItemName="Cookie Pizza", ItemPrice=3000, Category=desserts},
            new Item{ItemName="Chocolate Lava Cake", ItemPrice=2650, Category=desserts},
            new Item{ItemName="Chocolate Mousse", ItemPrice=2000, Category=desserts},
            new Item{ItemName="Vanilla Ice-Cream", ItemPrice=2500, Category=desserts},

            new Item{ItemName="Pepsi 1.25L", ItemPrice=450, Category=drinks},
            new Item{ItemName="Pepsi 375ML", ItemPrice=600, Category=drinks},
            new Item{ItemName="Mountain Dew 1.25L", ItemPrice=550, Category=drinks},
            new Item{ItemName="Mountain Dew 375ML", ItemPrice=700, Category=drinks},
            new Item{ItemName="Sunkist 1.25L", ItemPrice=450, Category=drinks},
            new Item{ItemName="Sunkist 375ML", ItemPrice=600, Category=drinks},
            new Item{ItemName="Alphine 1L", ItemPrice=300, Category=drinks},

            };
            items.ForEach(s => context.Item.Add(s));
            context.SaveChanges();
        }

        internal void SeedCategory(ApplicationDbContext context)
        {
            var categories = new List<Category>
            {
            new Category{CategoryName="Pizzas"},
            new Category{CategoryName="Sides"},
            new Category{CategoryName="Wings"},
            new Category{CategoryName="Desserts"},
            new Category{CategoryName="Drinks"},
            };
            categories.ForEach(s => context.Category.Add(s));
            context.SaveChanges();
        }
    }
}