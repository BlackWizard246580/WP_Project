using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using WP_Project.Models;

namespace WP_Project.DAL
{
    public class DBInitializer : CreateDatabaseIfNotExists<ApplicationDbContext>
    {
        public DBInitializer()
        {

        }


        //internal void SeedCustomFieldValue(ApplicationDbContext context)
        //{
        //    CustomField size = context.CustomField.Find(1);
        //    CustomField sauce = context.CustomField.Find(2);
        //    CustomField cheese = context.CustomField.Find(3);
        //    CustomField spicy = context.CustomField.Find(4);
        //    var customfieldvalues = new List<CustomFieldValue>
        //    {
        //    new CustomFieldValue{CustomFieldValueName="Medium", AdditionalPrice=0, CustomField=size},
        //    new CustomFieldValue{CustomFieldValueName="Large", AdditionalPrice=1000, CustomField=size},

        //    new CustomFieldValue{CustomFieldValueName="No Sauce", AdditionalPrice=0, CustomField=size},
        //    new CustomFieldValue{CustomFieldValueName="Tomato Sauce", AdditionalPrice=600, CustomField=size},
        //    new CustomFieldValue{CustomFieldValueName="BBQ Sauce", AdditionalPrice=1000, CustomField=size},
        //    new CustomFieldValue{CustomFieldValueName="Extra Tomato Sauce", AdditionalPrice=1000, CustomField=size},
        //    new CustomFieldValue{CustomFieldValueName="Extra BBQ Sauce", AdditionalPrice=1400, CustomField=size},

        //    new CustomFieldValue{CustomFieldValueName="No Cheese", AdditionalPrice=0, CustomField=cheese},
        //    new CustomFieldValue{CustomFieldValueName="Mozzarela", AdditionalPrice=1500, CustomField=cheese},
        //    new CustomFieldValue{CustomFieldValueName="Cheddar", AdditionalPrice=1200, CustomField=cheese},

        //    new CustomFieldValue{CustomFieldValueName="1", AdditionalPrice=0, CustomField=spicy},
        //    new CustomFieldValue{CustomFieldValueName="2", AdditionalPrice=0, CustomField=spicy},
        //    new CustomFieldValue{CustomFieldValueName="3", AdditionalPrice=0, CustomField=spicy},
        //    new CustomFieldValue{CustomFieldValueName="4", AdditionalPrice=0, CustomField=spicy},
        //    new CustomFieldValue{CustomFieldValueName="5", AdditionalPrice=0, CustomField=spicy},
        //    };
        //    customfieldvalues.ForEach(s => context.CustomFieldValue.Add(s));
        //    context.SaveChanges();
        //}

        //internal void SeedCustomField(ApplicationDbContext context)
        //{
        //    var customfields = new List<CustomField>
        //    {
        //    new CustomField{CustomFieldName="Size"},
        //    new CustomField{CustomFieldName="Sauce"},
        //    new CustomField{CustomFieldName="Cheese"},
        //    new CustomField{CustomFieldName="Spicy Level"},
        //    };
        //    customfields.ForEach(s => context.CustomField.Add(s));
        //    context.SaveChanges();
        //}

        //internal void SeedItem(ApplicationDbContext context)
        //{
        //    Category pizzas = context.Category.Find(1);
        //    Category sides = context.Category.Find(2);
        //    Category wings = context.Category.Find(3);
        //    Category desserts = context.Category.Find(4);
        //    Category drinks = context.Category.Find(5);
        //    var items = new List<Item>
        //    {
        //    new Item{ItemName="Chicken Supreme", ItemPrice=11000, Category=pizzas},
        //    new Item{ItemName="Cheese Lovers", ItemPrice=6500, Category=pizzas},
        //    new Item{ItemName="Ham Lovers", ItemPrice=11000, Category=pizzas},
        //    new Item{ItemName="BBQ Beef", ItemPrice=13500, Category=pizzas},
        //    new Item{ItemName="Ultimate Hot & Spicy", ItemPrice=9000, Category=pizzas},

        //    new Item{ItemName="Garlic Bread", ItemPrice=2500, Category=sides},
        //    new Item{ItemName="Cheesey Garlic Bread", ItemPrice=3000, Category=sides},
        //    new Item{ItemName="Spud Bites and Tomato Sauce Dip", ItemPrice=2500, Category=sides},
        //    new Item{ItemName="Triple Dippers", ItemPrice=13500, Category=sides},
        //    new Item{ItemName="BBQ Pork Ribs – Quarter Rack", ItemPrice=9000, Category=sides},

        //    new Item{ItemName="Buffalo Wings 6 Pack", ItemPrice=5000, Category=wings},
        //    new Item{ItemName="Naked Wings 6 Pack", ItemPrice=5500, Category=wings},
        //    new Item{ItemName="Seasoned Wings 6 Pack", ItemPrice=5000, Category=wings},
        //    new Item{ItemName="Buffalo Wings 10 Pack", ItemPrice=9000, Category=wings},
        //    new Item{ItemName="Naked Wings 10 Pack", ItemPrice=9500, Category=wings},
        //    new Item{ItemName="Seasoned Wings 10 Pack", ItemPrice=9000, Category=wings},

        //    new Item{ItemName="Cookie Pizza", ItemPrice=3000, Category=desserts},
        //    new Item{ItemName="Chocolate Lava Cake", ItemPrice=2650, Category=desserts},
        //    new Item{ItemName="Chocolate Mousse", ItemPrice=2000, Category=desserts},
        //    new Item{ItemName="Vanilla Ice-Cream", ItemPrice=2500, Category=desserts},

        //    new Item{ItemName="Pepsi 1.25L", ItemPrice=450, Category=drinks},
        //    new Item{ItemName="Pepsi 375ML", ItemPrice=600, Category=drinks},
        //    new Item{ItemName="Mountain Dew 1.25L", ItemPrice=550, Category=drinks},
        //    new Item{ItemName="Mountain Dew 375ML", ItemPrice=700, Category=drinks},
        //    new Item{ItemName="Sunkist 1.25L", ItemPrice=450, Category=drinks},
        //    new Item{ItemName="Sunkist 375ML", ItemPrice=600, Category=drinks},
        //    new Item{ItemName="Alphine 1L", ItemPrice=300, Category=drinks},

        //    };
        //    items.ForEach(s => context.Item.Add(s));
        //    context.SaveChanges();
        //}

        //internal void SeedCategory(ApplicationDbContext context)
        //{
        //    var categories = new List<Category>
        //    {
        //    new Category{CategoryName="Pizzas"},
        //    new Category{CategoryName="Sides"},
        //    new Category{CategoryName="Wings"},
        //    new Category{CategoryName="Desserts"},
        //    new Category{CategoryName="Drinks"},
        //    };
        //    categories.ForEach(s => context.Category.Add(s));
        //    context.SaveChanges();
        //}

        internal void SeedCustomFieldItems(ApplicationDbContext context)
        {
            CustomField size = context.CustomField.Find(1);
            CustomField sauce = context.CustomField.Find(2);
            CustomField cheese = context.CustomField.Find(3);
            CustomField spicy = context.CustomField.Find(4);

            Item chicken_supreme = context.Item.Find(1);
            chicken_supreme.CustomFields.Add(size);
            chicken_supreme.CustomFields.Add(sauce);
            chicken_supreme.CustomFields.Add(cheese);

            Item cheese_lover = context.Item.Find(2);
            cheese_lover.CustomFields.Add(size);
            cheese_lover.CustomFields.Add(cheese);

            Item ham_lover = context.Item.Find(3);
            ham_lover.CustomFields.Add(size);
            ham_lover.CustomFields.Add(sauce);

            Item bbq_beef = context.Item.Find(4);
            ham_lover.CustomFields.Add(size);
            ham_lover.CustomFields.Add(sauce);

            Item hot_spicy = context.Item.Find(5);
            hot_spicy.CustomFields.Add(size);
            hot_spicy.CustomFields.Add(sauce);
            hot_spicy.CustomFields.Add(spicy);

            Item cheesy_garlic_bread = context.Item.Find(7);
            cheese_lover.CustomFields.Add(cheese);

            Item tripple_dippers = context.Item.Find(9);
            tripple_dippers.CustomFields.Add(sauce);
            tripple_dippers.CustomFields.Add(spicy);

            context.SaveChanges();
        }
    }
}