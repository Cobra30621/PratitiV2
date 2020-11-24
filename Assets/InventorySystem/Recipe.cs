 using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using InventorySystem;

namespace DessertSystem
{
    public class Recipe
    {
        public List<Item> recipe;
        public Item item1;
        public Item item2;
        public Item item3;

        public Recipe(Item item1, Item item2, Item item3, List<Item> recipe)
        {
            this.item1 = item1;
            recipe.Add(item1);
            this.item2 = item2;
            recipe.Add(item2);
            this.item3 = item3;
            recipe.Add(item3);
            this.recipe = recipe;
        }
        public Recipe(Item item1, Item item2 , List<Item> recipe)
        {
            this.item1 = item1;
            this.item2 = item2;
            recipe.Add(item1);
            recipe.Add(item2);
            this.recipe = recipe;
        }


    }
}
