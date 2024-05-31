using DesignPartternDemo.Builder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignPartternDemo.Builder
{
    public interface Item
    {
        public String name();
        public Packing packing();
        public float price();
    }

    public interface Packing
    {
        public String pack();
    }

    public class Wrapper : Packing
    {
        public String pack()
        {
            return "Wrapper";
        }
    }

    public class Bottle : Packing
    {
        public String pack()
        {
            return "Bottle";
        }
    }


    public abstract class Burger : Item
    {
        public string name()
        {
            throw new NotImplementedException();
        }

        public Packing packing()
        {
            return new Wrapper();
        }

        public abstract float price();
    }

    public abstract class ColdDrink : Item
    {
        public string name()
        {
            throw new NotImplementedException();
        }

        public Packing packing()
        {
            return new Bottle();
        }

        public abstract float price();
    }

    public class VegBurger : Burger
    {


        public override float price()
        {
            return 25.0f;
        }

        public String name()
        {
            return "Veg Burger";
        }       
    }

    public class ChickenBurger : Burger
    {
        public override float price()
        {
            return 50.5f;
        }

        public String name()
        {
            return "Chicken Burger";
        }
    }

    public class Coke : ColdDrink
    {
        public override float price()
        {
            return 30.0f;
        }

        public String name()
        {
            return "Coke";
        }
    }

    public class Pepsi : ColdDrink
    {

        public override float price()
        {
            return 35.0f;
        }

        public String name()
        {
            return "Pepsi";
        }
    }



    public class Meal
    {
        private List<Item> items = new List<Item>();

        public void addItem(Item item)
        {
            items.Add(item);
        }

        public float getCost()
        {
            float cost = 0.0f;
            foreach (Item item in items)
            {
                cost += item.price();
            }
            return cost;
        }

        public void showItems()
        {
            foreach (Item item in items)
            {
                Console.WriteLine("Item : " + item.name());
                Console.WriteLine(", Packing : " + item.packing().pack());
                Console.WriteLine(", Price : " + item.price());
            }
        }
    }

    public class MealBuilder
    {

        public Meal prepareVegMeal()
        {
            Meal meal = new Meal();
            meal.addItem(new VegBurger());
            meal.addItem(new Coke());
            return meal;
        }

        public Meal prepareNonVegMeal()
        {
            Meal meal = new Meal();
            meal.addItem(new ChickenBurger());
            meal.addItem(new Pepsi());
            return meal;
        }
    }

    public class BuilderPatternDemo
    {
        public static void main(String[] args)
        {
            MealBuilder mealBuilder = new MealBuilder();

            Meal vegMeal = mealBuilder.prepareVegMeal();
            Console.WriteLine("Veg Meal");
            vegMeal.showItems();
            Console.WriteLine("Total Cost: " + vegMeal.getCost());

            Meal nonVegMeal = mealBuilder.prepareNonVegMeal();
            Console.WriteLine("\n\nNon-Veg Meal");
            nonVegMeal.showItems();
            Console.WriteLine("Total Cost: " + nonVegMeal.getCost());
        }
    }
}
