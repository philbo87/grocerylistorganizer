using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GroceryListOrganizer
{
    class Program
    {
        private static List<string> produce = new List<string>();
        private static List<string> meat = new List<string>();
        private static List<string> bakery = new List<string>();
        private static List<string> deli = new List<string>();
        private static List<string> dairy = new List<string>();
        private static List<string> aisles = new List<string>();
        private static List<string> unknown = new List<string>();
        private static List<string> unsortedList = new List<string> { "milk", "jicama", "cherry tomatoes", "bell pepper", "cucumber", "zuchini", "celery", "2 small avocadoes", "1 med onion",
            "1 large red bell pepper", "fresh rosemary", "6 small red potatoes", "4c veg stock", "kale 1 bunch", "1 cup fava beans", "1 lb bowties", "1 c part-skim ricotta cheese",
            "grated parmesan", "fresh mint", "28oz crushed tomatoes", "1 large onion", "2 lb ground lamb", "1 med onion", "ground cumin", "tomato paste in a tube", "garbage bags",
            "emergen-C"};

        private static Dictionary<string, StoreArea> knownItems = new Dictionary<string, StoreArea>
        {
            { "milk",StoreArea.Dairy },
            { "jicama", StoreArea.Produce },
            { "cherry tomatoes", StoreArea.Produce },
            { "bell pepper", StoreArea.Produce },
            { "cucumber", StoreArea.Produce },
            { "celery", StoreArea.Produce },
            { "avocado", StoreArea.Produce },
            { "onion", StoreArea.Produce },
            { "red bell pepper", StoreArea.Produce },
            { "fresh", StoreArea.Produce },
            { "rosemary", StoreArea.Produce },
            { "potato", StoreArea.Produce },
            { "stock", StoreArea.Aisles },
            { "kale", StoreArea.Produce },
            { "beans", StoreArea.Aisles },
            { "bowties", StoreArea.Aisles },
            { "ricotta cheese", StoreArea.Dairy },
            { "parmesan", StoreArea.Dairy },
            { "mint", StoreArea.Produce },
            { "oz", StoreArea.Aisles },
            { "lamb", StoreArea.Meat },
            { "chicken", StoreArea.Meat },
            { "breasts", StoreArea.Meat },
            { "turkey", StoreArea.Meat },
            { "beef", StoreArea.Meat },
            { "cumin", StoreArea.Aisles },
            { "tomato paste", StoreArea.Aisles },
            { "bags", StoreArea.Aisles },
            { "emergen-C", StoreArea.Aisles }
        };

        static void Main(string[] args)
        {
            buildStoreAreaLists();
            printOutContents(); 

        }

        private static void buildStoreAreaLists()
        {
            foreach (var item in unsortedList)
            {
                var storeArea = determineStoreArea(item);

                switch (storeArea)
                {
                    case StoreArea.Produce:
                        produce.Add(item);
                        break;
                    case StoreArea.Meat:
                        meat.Add(item);
                        break;
                    case StoreArea.Bakery:
                        bakery.Add(item);
                        break;
                    case StoreArea.Deli:
                        deli.Add(item);
                        break;
                    case StoreArea.Dairy:
                        dairy.Add(item);
                        break;
                    case StoreArea.Aisles:
                        aisles.Add(item);
                        break;
                    case StoreArea.Unknown:
                        unknown.Add(item);
                        break;
                }
            }
        }

        private static StoreArea determineStoreArea(string item)
        {
            //Tokenize the item
            var tokenizedItemStrings = item.Split(new char[] { ' ' });

            //Check against dictionary of known items in each area. If you get a hit on the key, get the value of StoreArea and return it. If it isn't found, return unknown.
            var area = StoreArea.Unknown;

            foreach(var s in tokenizedItemStrings)
            {
                knownItems.TryGetValue(item, out area);
                if (area != StoreArea.Unknown) break;
            }
            return area;
        }

        private static void printOutContents()
        {
            /*
            Cermak:
            Produce
            Bakery
            Aisles
            Meat
            Dairy
            Deli

            Pick N Save Tosa:
            Produce
            Deli
            Bakery
            Meat
            Aisles
            Dairy*/

            //Writing out areas based on Cermak for now
            Console.WriteLine("Produce:");
            printList(produce);

            Console.WriteLine("Bakery:");
            printList(bakery);

            Console.WriteLine("Aisles:");
            printList(aisles);

            Console.WriteLine("Meat:");
            printList(meat);

            Console.WriteLine("Dairy:");
            printList(dairy);

            Console.WriteLine("Deli:");
            printList(deli);
            Console.WriteLine("Unknown items:");
            printList(unknown);
            Console.ReadLine();
        }

        private static void printList(List<string> list)
        {
            foreach(var item in list)
            {
                Console.WriteLine(item);
            }
        }
    }
}
