using System;   
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GroceryListOrganizer
{
    class Program
    {
        private static List<string> _produce = new List<string>();
        private static List<string> _meat = new List<string>();
        private static List<string> _bakery = new List<string>();
        private static List<string> _deli = new List<string>();
        private static List<string> _dairy = new List<string>();
        private static List<string> _aisles = new List<string>();
        private static List<string> _unknown = new List<string>();
        private static List<string> _unsortedList = new List<string> { "milk", "jicama", "cherry tomatoes", "bell pepper", "cucumber", "zuchini", "celery", "2 small avocadoes", "1 med onion",
            "1 large red bell pepper", "fresh rosemary", "6 small red potatoes", "4c veg stock", "kale 1 bunch", "1 cup fava beans", "1 lb bowties", "1 c part-skim ricotta cheese",
            "grated parmesan", "fresh mint", "28 oz crushed tomatoes", "1 large onion", "2 lb ground lamb", "1 med onion", "ground cumin", "tomato paste in a tube", "garbage bags",
            "emergen-C"};

        private static Dictionary<string, StoreArea> _knownItems = new Dictionary<string, StoreArea>
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
            { "emergen-C", StoreArea.Aisles },
            { "zuchini", StoreArea.Produce }
        };

        static void Main(string[] args)
        {
            BuildStoreAreaLists();
            PrintOutContents(); 

        }

        private static void BuildStoreAreaLists()
        {
            foreach (var item in _unsortedList)
            {
                var storeArea = DetermineStoreArea(item);

                switch (storeArea)
                {
                    case StoreArea.Produce:
                        _produce.Add(item);
                        break;
                    case StoreArea.Meat:
                        _meat.Add(item);
                        break;
                    case StoreArea.Bakery:
                        _bakery.Add(item);
                        break;
                    case StoreArea.Deli:
                        _deli.Add(item);
                        break;
                    case StoreArea.Dairy:
                        _dairy.Add(item);
                        break;
                    case StoreArea.Aisles:
                        _aisles.Add(item);
                        break;
                    case StoreArea.Unknown:
                        _unknown.Add(item);
                        break;
                }
            }
        }

        private static StoreArea DetermineStoreArea(string item)
        {
            //First, see if the whole item is a key in the dictionary. If it is, stop, we've found our store area.
            var area = _knownItems.Where(i => i.Key.Equals(item)).Select(i => i.Value).FirstOrDefault();
            if (area != StoreArea.Unknown) return area;

            //If that didn't work, tokenize the item. See if the individual words in the item are in the dictionary
            var tokenizedItemStrings = item.Split(new char[] { ' ' });

            foreach(var s in tokenizedItemStrings)
            {
                area = _knownItems.Where(i => i.Key.Equals(s)).Select(i => i.Value).FirstOrDefault();
                if (area != StoreArea.Unknown) break;
            }
            return area;
        }

        private static void PrintOutContents()
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
            PrintList(_produce);

            Console.WriteLine("Bakery:");
            PrintList(_bakery);

            Console.WriteLine("Aisles:");
            PrintList(_aisles);

            Console.WriteLine("Meat:");
            PrintList(_meat);

            Console.WriteLine("Dairy:");
            PrintList(_dairy);

            Console.WriteLine("Deli:");
            PrintList(_deli);

            Console.WriteLine("Unknown items:");
            PrintList(_unknown);

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
