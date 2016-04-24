using System;   
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GroceryListOrganizer
{
    class Program
    {
        
        private static List<string> _groceryList = new List<string> {"null", "tissues", "eggs", "jicama", "cherry tomatoes", "celery", "bell pepper", "zucchini", "1.5 lbs raw shrimp", "2 small hot chili-peppers", "8 cups lower-sodium-chicken-broth", "medium amount bean-sprouts", "4 lbs chicken breasts", "8 oz chorizo", "1 small jar roasted-red-peppers", "english breakfast tea", "apples", "bananas", "fruit for oatmeal" };

        private static DataAccess _dao;      
        static void Main(string[] args)
        {
            _dao = new DataAccess();
            var items = GetItemsForGroceryList();
            PrintOutContents(items); 
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

        private static void PrintOutContents(Dictionary<string,Item> itemsToPrint)
        {
            List<string> cermakProduce = new List<string>();
            List<string> cermakBakery = new List<string>();
            List<string> cermakAisles = new List<string>();
            List<string> cermakMeat = new List<string>();
            List<string> cermakSeafood = new List<string>();
            List<string> cermakDairy = new List<string>();
            List<string> cermakDeli = new List<string>();
            List<string> cermakFrozen = new List<string>();

            List<string> pickNSaveProduce = new List<string>();
            List<string> pickNSaveBakery = new List<string>();
            List<string> pickNSaveAisles = new List<string>();
            List<string> pickNSaveMeat = new List<string>();
            List<string> pickNSaveSeafood = new List<string>();
            List<string> pickNSaveDairy = new List<string>();
            List<string> pickNSaveDeli = new List<string>();
            List<string> pickNSaveFrozen = new List<string>();

            List<string> unknown = new List<string>();

            //TODO: For each store, build lists for each area. Print them in the order I would like.
            foreach (var printableText in itemsToPrint.Keys)
            {
                Item item = null;
                itemsToPrint.TryGetValue(printableText, out item);
                if (item != null)
                {
                    if(item.PreferredStore == Store.Cermak)
                    {
                        switch (item.AreaInStore)
                        {
                            case StoreArea.Aisles:
                                cermakAisles.Add(printableText);
                                break;
                            case StoreArea.Bakery:
                                cermakBakery.Add(printableText);
                                break;
                            case StoreArea.Dairy:
                                cermakDairy.Add(printableText);
                                break;
                            case StoreArea.Deli:
                                cermakDeli.Add(printableText);
                                break;
                            case StoreArea.Frozen:
                                cermakFrozen.Add(printableText);
                                break;
                            case StoreArea.Meat:
                                cermakMeat.Add(printableText);
                                break;
                            case StoreArea.Produce:
                                cermakProduce.Add(printableText);
                                break;
                            case StoreArea.Seafood:
                                cermakSeafood.Add(printableText);
                                break;
                        }
                    }
                    else if(item.PreferredStore == Store.PickNSave)
                    {
                        switch (item.AreaInStore)
                        {
                            case StoreArea.Aisles:
                                pickNSaveAisles.Add(printableText);
                                break;
                            case StoreArea.Bakery:
                                pickNSaveBakery.Add(printableText);
                                break;
                            case StoreArea.Dairy:
                                pickNSaveDairy.Add(printableText);
                                break;
                            case StoreArea.Deli:
                                pickNSaveDeli.Add(printableText);
                                break;
                            case StoreArea.Frozen:
                                pickNSaveFrozen.Add(printableText);
                                break;
                            case StoreArea.Meat:
                                pickNSaveMeat.Add(printableText);
                                break;
                            case StoreArea.Produce:
                                pickNSaveProduce.Add(printableText);
                                break;
                            case StoreArea.Seafood:
                                pickNSaveSeafood.Add(printableText);
                                break;
                        }
                    }
                }
                else
                {
                    //Item was null. We don't know about this. Add it to the unknown list
                    unknown.Add(printableText);
                }
            }

            Console.WriteLine("Cermak Fresh Market Items:");
            Console.WriteLine("Produce:");
            PrintList(cermakProduce);
            Console.WriteLine();

            Console.WriteLine("Bakery:");
            PrintList(cermakBakery);
            Console.WriteLine();

            Console.WriteLine("Aisles:");
            PrintList(cermakAisles);
            Console.WriteLine();

            Console.WriteLine("Meat:");
            PrintList(cermakMeat);
            Console.WriteLine();

            Console.WriteLine("Seafood:");
            PrintList(cermakSeafood);
            Console.WriteLine();

            Console.WriteLine("Dairy:");
            PrintList(cermakDairy);
            Console.WriteLine();

            Console.WriteLine("Deli:");
            PrintList(cermakDeli);
            Console.WriteLine();

            Console.WriteLine("Frozen:");
            PrintList(cermakFrozen);
            Console.WriteLine();

            Console.WriteLine("Pick N Save Items:");
            Console.WriteLine("Produce:");
            PrintList(pickNSaveProduce);
            Console.WriteLine();

            Console.WriteLine("Bakery:");
            PrintList(pickNSaveBakery);
            Console.WriteLine();

            Console.WriteLine("Deli:");
            PrintList(pickNSaveDeli);
            Console.WriteLine();

            Console.WriteLine("Seafood:");
            PrintList(pickNSaveSeafood);
            Console.WriteLine();

            Console.WriteLine("Meat:");
            PrintList(pickNSaveMeat);
            Console.WriteLine();

            Console.WriteLine("Aisles:");
            PrintList(pickNSaveAisles);
            Console.WriteLine();

            Console.WriteLine("Dairy:");
            PrintList(pickNSaveDairy);
            Console.WriteLine();

            Console.WriteLine("Frozen:");
            PrintList(pickNSaveFrozen);
            Console.WriteLine();

            Console.WriteLine("Unknown items:");
            PrintList(unknown);

            Console.ReadLine();
        }

        private static void PrintList(List<string> list)
        {
            foreach(var item in list)
            {
                Console.WriteLine(item);
            }
        }
    }
}
