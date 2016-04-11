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
        private static List<string> _frozen = new List<string>();
        private static List<string> _seafood = new List<string>();
        private static List<string> _unknown = new List<string>();
        //private static List<string> _unsortedList = new List<string> { "milk", "jicama", "cherry tomatoes", "bell pepper", "cucumber", "zuchini", "celery", "2 small avocadoes", "1 med onion",
        //    "1 large red bell pepper", "fresh rosemary", "6 small red potatoes", "4c veg stock", "kale 1 bunch", "1 cup fava beans", "1 lb bowties", "1 c part-skim ricotta cheese",
        //    "grated parmesan", "fresh mint", "28 oz crushed tomatoes", "1 large onion", "2 lb ground lamb", "1 med onion", "ground cumin", "tomato paste in a tube", "garbage bags",
        //    "emergen-C"};

        //private static List<string> _unsortedList = new List<string> { "apples", "bananas", "eggs", "3 small avocados", "bell pepper", "cucumber", "snap peas", "jicama", "cherry tomatoes", "fruit for oatmeal", "yoghurt", "celery", "2 28oz cans no salt added spicy diced-tomatoes", "raw cashews", "sliced-cheese for grilled cheese", "sliced-bread for grilled cheese"};
        //private static List<string> _unsortedList = new List<string> { "almonds", "hummus", "black beans", "1 bunch kale", "portabella mushrooms", "cilantro", "small cucumber", "3 limes", "2 tomatillos", "4 small avocadoes", "chicken", "mozzarella sticks", "english muffins", "bell pepper", "snap peas", "dryer sheets", "sliced-bread for grilled cheese"};
        private static List<string> _unsortedList = new List<string> { "tissues", "eggs", "jicama", "cherry tomatoes", "celery", "bell pepper", "zucchini", "1.5 lbs raw shrimp", "2 small hot chili-peppers", "8 cups lower-sodium-chicken-broth", "medium amount bean-sprouts", "4 lbs chicken breasts", "8 oz chorizo", "1 small jar roasted-red-peppers", "english breakfast tea" };
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
            { "emergen-c", StoreArea.Aisles },
            { "zucchini", StoreArea.Produce },
            { "apple", StoreArea.Produce },//TODO: Depluralizing apples might make appl with our algorithm, which means I need to modify it to handle a special case potentially
            { "apples", StoreArea.Produce },
            { "banana", StoreArea.Produce },
            { "eggs", StoreArea.Dairy },
            { "snap peas", StoreArea.Produce },
            { "fruit for oatmeal", StoreArea.Produce },
            { "yoghurt", StoreArea.Dairy },
            { "diced-tomatoes", StoreArea.Aisles }, //TODO make the algorithm handle partle token matches so I don't need a dash in diced-tomatoes
            { "raw cashews", StoreArea.Produce },
            { "sliced-cheese", StoreArea.Deli },
            { "sliced-bread", StoreArea.Bakery },
            { "almonds", StoreArea.Produce },
            { "hummus", StoreArea.Produce }, //Pick N Save specific item
            { "black beans", StoreArea.Aisles },
            { "portabella mushrooms", StoreArea.Produce },
            { "cilantro", StoreArea.Produce },
            { "lime", StoreArea.Produce },
            { "limes", StoreArea.Produce },//TODO: Depluralizing Limes has the same problem as apples
            { "tomatillos", StoreArea.Produce },
            { "mozzarella sticks", StoreArea.Dairy }, //PnS
            { "english muffins", StoreArea.Frozen },//PnS
            { "dryer sheets", StoreArea.Aisles },
            { "tissues", StoreArea.Aisles },//Pns
            { "shrimp", StoreArea.Seafood },
            { "chili-peppers", StoreArea.Produce },
            { "lower-sodium-chicken-broth", StoreArea.Aisles },
            { "bean-sprouts", StoreArea.Produce },
            { "roasted-red-peppers", StoreArea.Aisles },
            { "tea", StoreArea.Aisles }
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
                    case StoreArea.Frozen:
                        _frozen.Add(item);
                        break;
                    case StoreArea.Seafood:
                        _seafood.Add(item);
                        break;
                    case StoreArea.Unknown:
                        _unknown.Add(item);
                        break;
                }
            }
        }

        private static StoreArea DetermineStoreArea(string item)
        {
            //Convert to lowercase. All dictionary keys are lower case
            item = item.ToLower();

            //See if the whole item is a key in the dictionary. If it is, stop, we've found our store area.
            var area = _knownItems.Where(i => i.Key.Equals(item)).Select(i => i.Value).FirstOrDefault();
            if (area != StoreArea.Unknown) return area;

            //If that didn't work, tokenize the item. See if the individual words in the item are in the dictionary
            var tokenizedItemStrings = item.Split(' ');

            foreach(var s in tokenizedItemStrings)
            {
                area = _knownItems.Where(i => i.Key.Equals(s)).Select(i => i.Value).FirstOrDefault();
                if (area != StoreArea.Unknown) break;
            }

            //If we still haven't found it, depluralize the last token if possible, and try again. 
            //We only try this with the last token because it is assumed that will be the pluralized word.
            if (area == StoreArea.Unknown)
            {
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
                    area = _knownItems.Where(i => i.Key.Equals(depluralizedString)).Select(i => i.Value).FirstOrDefault();
                }
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
            Frozen

            Pick N Save Tosa:
            Produce
            Deli
            Bakery
            Meat
            Aisles
            Dairy
            Frozen*/

            //Writing out areas based on Cermak for now
            Console.WriteLine("Produce:");
            PrintList(_produce);
            Console.WriteLine();

            Console.WriteLine("Bakery:");
            PrintList(_bakery);
            Console.WriteLine();

            Console.WriteLine("Aisles:");
            PrintList(_aisles);
            Console.WriteLine();

            Console.WriteLine("Meat:");
            PrintList(_meat);
            Console.WriteLine();

            Console.WriteLine("Seafood:");
            PrintList(_seafood);
            Console.WriteLine();

            Console.WriteLine("Dairy:");
            PrintList(_dairy);
            Console.WriteLine();

            Console.WriteLine("Deli:");
            PrintList(_deli);
            Console.WriteLine();

            Console.WriteLine("Frozen:");
            PrintList(_frozen);
            Console.WriteLine();

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
