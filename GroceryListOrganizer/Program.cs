using System;   
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace GroceryListOrganizer
{
    class Program
    {
        private static readonly List<string> _groceryList = new List<string>();
        private static DataAccess _dao;

        static void Main(string[] args)
        {
            AskForList();
            _dao = new DataAccess();
            var items = GetItemsForGroceryList();
            PrintOutContents(items); 
        }

        private static void AskForList()
        {
            Console.WriteLine("Welcome to Grocery List Organizer.");
            Console.WriteLine("Please enter your items. Enter sort to print the list.");

            string input = null;
            while (input != "sort")
            {
                input = Console.ReadLine();
                if (input != "sort")
                {
                    _groceryList.Add(input);
                }
            }
            Console.Clear();
        }

        private static Dictionary<string, Item> GetItemsForGroceryList()
        {
            var items = new Dictionary<string, Item>();
            foreach(var groceryListEntry in _groceryList)
            {
                var foundItem = LocateItemForGroceryListEntry(groceryListEntry.ToLower());
                items.Add(groceryListEntry, foundItem);
            }

            return items;
        }

        private static Item LocateItemForGroceryListEntry(string groceryListItem)
        {
            Item foundItem = null;
            
            foundItem = _dao.GetItemByName(groceryListItem);
            if (foundItem != null) return foundItem;

            //If that didn't work, tokenize the item, and try to see if the tokenized words in the list item are in the data layer.
            var tokenizedItemStrings = groceryListItem.Split(' ');

            foundItem = LookupItemFromTokenizedStrings(tokenizedItemStrings);
            if (foundItem != null) return foundItem;

            //If we still haven't found it, depluralize the last token if possible, and try again. 
            //We only try this with the last token because it is assumed that will be the pluralized word.
            foundItem = DepluralizeStringAndLookForItem(tokenizedItemStrings.Last());
  
            //If we return null at this point, we couldn't find this item. It will be handled as an unknown when printing.
            return foundItem;
        }

        private static Item DepluralizeStringAndLookForItem(string lastStringInListItem)
        {
            Item foundItem = null;
            string depluralizedString = null;
            if (lastStringInListItem.EndsWith("es"))
            {
                depluralizedString = lastStringInListItem
                    .Substring(0, lastStringInListItem.Length - 2);
            }
            else if (lastStringInListItem.EndsWith("s"))
            {
                depluralizedString = lastStringInListItem
                    .Substring(0, lastStringInListItem.Length - 1);
            }

            if (depluralizedString != null)
            {
                foundItem = _dao.GetItemByName(depluralizedString);
                if (foundItem != null) return foundItem;

                //Special case: Sometimes a depluralized string isn't a word any more if it ended in -es. For example: apples -> appl. Re-add an e and see if we get a hit.
                depluralizedString += "e";
                foundItem = _dao.GetItemByName(depluralizedString);
            }

            return foundItem;
        }

        private static Item LookupItemFromTokenizedStrings(ICollection<string> tokenizedItemStrings)
        {
            Item foundItem = null;
            var foundItemsFromTokenizedStrings = new List<Item>();
            foreach (var tokenizedItemString in tokenizedItemStrings)
            {
                var retrievedItem = _dao.GetItemByName(tokenizedItemString);
                if (retrievedItem != null)
                {
                    foundItemsFromTokenizedStrings.Add(retrievedItem);
                }
            }

            if (foundItemsFromTokenizedStrings.Count > 0)
            {
                //Find the item with the highest score
                foreach (var item in foundItemsFromTokenizedStrings)
                {
                    if (foundItem == null)
                    {
                        foundItem = item;
                    }
                    else if (item.Score < foundItem.Score)
                    {
                        foundItem = item;
                    }
                }
            }

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
