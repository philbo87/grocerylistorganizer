﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GroceryListOrganizer
{
    public class Item
    {
        public Item(string name, Store preferredStore, StoreArea areaInStore)
        {
            Name = name;
            PreferredStore = preferredStore;
            AreaInStore = areaInStore;
        }

        public string Name { get; set; }

        public Store PreferredStore { get; set; }

        public StoreArea AreaInStore { get; set; }
    }
}
