using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using InventorySystem;
using System;

namespace DessertSystem
{
    public class CookBook
    {
        private  Dictionary<Item.ItemName, Recipe> cookBook = new Dictionary<Item.ItemName, Recipe>();

        public CookBook()
        {
            cookBook = new Dictionary<Item.ItemName, Recipe>() ;
        }

        public Dictionary <Item.ItemName, Recipe> GetCookBook()
        {
            return cookBook;
        }
    }
}
