using System;   
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GroceryListOrganizer
{
    class Program
    {
        private static List<Item> _produce = new List<Item>();
        private static List<Item> _meat = new List<Item>();
        private static List<Item> _bakery = new List<Item>();
        private static List<Item> _deli = new List<Item>();
        private static List<Item> _dairy = new List<Item>();
        private static List<Item> _aisles = new List<Item>();
        private static List<Item> _frozen = new List<Item>();
        private static List<Item> _seafood = new List<Item>();
        private static List<Item> _unknown = new List<Item>();
        private static List<string> _groceryList = new List<string> {"null", "tissues", "eggs", "jicama", "cherry tomatoes", "celery", "bell pepper", "zucchini", "1.5 lbs raw shrimp", "2 small hot chili-peppers", "8 cups lower-sodium-chicken-broth", "medium amount bean-sprouts", "4 lbs chicken breasts", "8 oz chorizo", "1 small jar roasted-red-peppers", "english breakfast tea", "apples", "bananas", "fruit for oatmeal" };

        private static DataAccess _dao;      
        static void Main(string[] args)
        {
            _dao = new DataAccess();
            var items = GetItemsForGroceryList();
            //PrintOutContents(); 
        }

        private static Dictionary<string, Item> GetItemsForGroceryList()
        {
            var items = new Dictionary<string, Item>();
            foreach(var groceryListEntry in _groceryList)
            {
                var foundItem = LocateItemForGroceryListEntry(groceryListEntry);
                items.Add(groceryListEntry, foundItem);
            }

            return items;
        }

        private static Item LocateItemForGroceryListEntry(string groceryListItem)
        {
            //Convert to lowercase. All item names are stored as lower case.
            groceryListItem = groceryListItem.ToLower();

            Item foundItem = null;
            
            foundItem = _dao.GetItemByName(groceryListItem);
            if (foundItem != null) return foundItem;
          
            //If that didn't work, tokenize the item. See if the individual words in the item are in the dictionary
            var tokenizedItemStrings = groceryListItem.Split(' ');
            foreach (var tokenizedItemString in tokenizedItemStrings)
            {
                foundItem = _dao.GetItemByName(tokenizedItemString);
                if (foundItem != null) return foundItem;
            }

            //If we still haven't found it, depluralize the last token if possible, and try again. 
            //We only try this with the last token because it is assumed that will be the pluralized word.
            string depluralizedString = null;
            if (tokenizedItemStrings.Last().EndsWith("es"))
            {
                depluralizedString = tokenizedItemStrings.Last()
                    .Substring(0, tokenizedItemStrings.Last().Length - 2);
            }
            else if (tokenizedItemStrings.Last().EndsWith("s"))
            {
                depluralizedString = tokenizedItemStrings.Last()
                    .Substring(0, tokenizedItemStrings.Last().Length - 1);
            }

            if (depluralizedString != null)
            {
                foundItem = _dao.GetItemByName(depluralizedString);
            }

            //If we return null at this point, we couldn't find this item. It will be handled as an unknown when printing.
            return foundItem;
            

        }

        //private static void PrintOutContents()
        //{
        //    /*
        //    Cermak:
        //    Produce
        //    Bakery
        //    Aisles
        //    Meat
        //    Dairy
        //    Deli
        //    Frozen

        //    Pick N Save Tosa:
        //    Produce
        //    Deli
        //    Bakery
        //    Meat
        //    Aisles
        //    Dairy
        //    Frozen*/

        //    //Writing out areas based on Cermak for now
        //    Console.WriteLine("Produce:");
        //    PrintList(_produce);
        //    Console.WriteLine();

        //    Console.WriteLine("Bakery:");
        //    PrintList(_bakery);
        //    Console.WriteLine();

        //    Console.WriteLine("Aisles:");
        //    PrintList(_aisles);
        //    Console.WriteLine();

        //    Console.WriteLine("Meat:");
        //    PrintList(_meat);
        //    Console.WriteLine();

        //    Console.WriteLine("Seafood:");
        //    PrintList(_seafood);
        //    Console.WriteLine();

        //    Console.WriteLine("Dairy:");
        //    PrintList(_dairy);
        //    Console.WriteLine();

        //    Console.WriteLine("Deli:");
        //    PrintList(_deli);
        //    Console.WriteLine();

        //    Console.WriteLine("Frozen:");
        //    PrintList(_frozen);
        //    Console.WriteLine();

        //    Console.WriteLine("Unknown items:");
        //    PrintList(_unknown);

        //    Console.ReadLine();
        //}

        private static void PrintList(List<string> list)
        {
            foreach(var item in list)
            {
                Console.WriteLine(item);
            }
        }
    }
}
