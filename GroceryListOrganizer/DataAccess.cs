using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GroceryListOrganizer
{
    public class DataAccess
    {
        private static List<Item> _items = new List<Item>();
        public DataAccess()
        {
            //For now, I'm just creating a bunch of in memory objects. Will actually store this data somewhere someday.
            _items.Add(new Item("milk", Store.Cermak, StoreArea.Dairy, 1));
            _items.Add(new Item("jicama", Store.Cermak, StoreArea.Produce, 1));
            _items.Add(new Item("cherry tomatoes", Store.Cermak, StoreArea.Produce, 1));
            _items.Add(new Item("bell pepper", Store.Cermak, StoreArea.Produce, 1));
            _items.Add(new Item("cucumber", Store.Cermak, StoreArea.Produce, 1));
            _items.Add(new Item("celery", Store.Cermak, StoreArea.Produce, 1));
            _items.Add(new Item("avocado", Store.Cermak, StoreArea.Produce, 1));
            _items.Add(new Item("onion", Store.Cermak, StoreArea.Produce, 1));
            _items.Add(new Item("red bell pepper", Store.Cermak, StoreArea.Produce, 1));
            _items.Add(new Item("fresh", Store.Cermak, StoreArea.Produce, 2));
            _items.Add(new Item("rosemary", Store.Cermak, StoreArea.Produce, 1));
            _items.Add(new Item("potato", Store.Cermak, StoreArea.Produce, 1));
            _items.Add(new Item("stock", Store.Cermak, StoreArea.Aisles, 2));
            _items.Add(new Item("kale", Store.Cermak, StoreArea.Produce, 1));
            _items.Add(new Item("beans", Store.Cermak, StoreArea.Aisles, 1));
            _items.Add(new Item("bowties", Store.Cermak, StoreArea.Aisles, 1));
            _items.Add(new Item("ricotta cheese", Store.Cermak, StoreArea.Dairy, 1));
            _items.Add(new Item("parmesan", Store.Cermak, StoreArea.Dairy, 1));
            _items.Add(new Item("mint", Store.Cermak, StoreArea.Produce, 1));
            _items.Add(new Item("oz", Store.Cermak, StoreArea.Aisles, 2));
            _items.Add(new Item("lamb", Store.Cermak, StoreArea.Meat, 1));
            _items.Add(new Item("chicken", Store.Cermak, StoreArea.Meat, 1));
            _items.Add(new Item("breasts", Store.Cermak, StoreArea.Meat, 1));
            _items.Add(new Item("turkey", Store.Cermak, StoreArea.Meat, 1));
            _items.Add(new Item("beef", Store.Cermak, StoreArea.Meat, 1));
            _items.Add(new Item("cumin", Store.Cermak, StoreArea.Aisles, 1));
            _items.Add(new Item("tomato paste", Store.Cermak, StoreArea.Aisles, 1));
            _items.Add(new Item("emergen-c", Store.PickNSave, StoreArea.Aisles, 1));
            _items.Add(new Item("zucchini", Store.Cermak, StoreArea.Produce, 1));
            _items.Add(new Item("apple", Store.Cermak, StoreArea.Produce, 1));
            _items.Add(new Item("apples", Store.Cermak, StoreArea.Produce, 1));
            _items.Add(new Item("banana", Store.Cermak, StoreArea.Produce, 1));
            _items.Add(new Item("eggs", Store.Cermak, StoreArea.Dairy, 1));
            _items.Add(new Item("snap peas", Store.PickNSave, StoreArea.Produce, 1));
            _items.Add(new Item("fruit for oatmeal", Store.Cermak, StoreArea.Produce, 1));
            _items.Add(new Item("yoghurt", Store.Cermak, StoreArea.Dairy, 1));
            _items.Add(new Item("diced-tomatoes", Store.Cermak, StoreArea.Aisles, 1));
            _items.Add(new Item("raw cashews", Store.Cermak, StoreArea.Produce, 1));
            _items.Add(new Item("sliced-cheese", Store.Cermak, StoreArea.Deli, 1));
            _items.Add(new Item("sliced-bread", Store.Cermak, StoreArea.Bakery, 1));
            _items.Add(new Item("almonds", Store.Cermak, StoreArea.Produce, 1));
            _items.Add(new Item("hummus", Store.PickNSave, StoreArea.Produce, 1));
            _items.Add(new Item("black beans", Store.Cermak, StoreArea.Aisles, 1));
            _items.Add(new Item("portabella mushrooms", Store.Cermak, StoreArea.Produce, 1));
            _items.Add(new Item("cilantro", Store.Cermak, StoreArea.Produce, 1));
            _items.Add(new Item("lime", Store.Cermak, StoreArea.Produce, 1));
            _items.Add(new Item("limes", Store.Cermak, StoreArea.Produce, 1));
            _items.Add(new Item("tomaillos", Store.Cermak, StoreArea.Produce, 1));
            _items.Add(new Item("mozzarella sticks", Store.PickNSave, StoreArea.Dairy, 1));
            _items.Add(new Item("english muffins", Store.PickNSave, StoreArea.Frozen, 1));
            _items.Add(new Item("dryer sheets", Store.PickNSave, StoreArea.Aisles, 1));
            _items.Add(new Item("tissues", Store.PickNSave, StoreArea.Aisles, 1));
            _items.Add(new Item("shrimp", Store.Cermak, StoreArea.Seafood, 1));
            _items.Add(new Item("chili-peppers", Store.Cermak, StoreArea.Produce, 1));
            _items.Add(new Item("lower-sodium-chicken-broth", Store.Cermak, StoreArea.Aisles, 1));
            _items.Add(new Item("bean-sprouts", Store.Cermak, StoreArea.Produce, 1));
            _items.Add(new Item("roasted-red-peppers", Store.Cermak, StoreArea.Aisles, 1));
            _items.Add(new Item("tea", Store.Cermak, StoreArea.Aisles, 1));
            _items.Add(new Item("chorizo", Store.Cermak, StoreArea.Meat, 1));

        }

        public Item GetItemByName(string name)
        {
            foreach(var item in _items)
            {
                if (item.Name == name)
                {
                    return item;
                }
            }

            return null;
        }
    }
}
