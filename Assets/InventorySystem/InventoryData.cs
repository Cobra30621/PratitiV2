using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DessertSystem;
using Newtonsoft.Json;

namespace InventorySystem
{
    [System.Serializable]
    public class InventoryData : MonoBehaviour
    {
        static InventoryData instance;
        public static List<Item> ingredientInventory;
        public  List<Item> ShowIngredientInventory;
        
        public static List<Item> dessertInventory;
        public List<Item> ShowDessertInventory;
        
        public static List<Item> otherInventory;
        public  List<Item> ShowOtherInventory;


        //public Dictionary<string,int> inventory;
        private static CookBook myCookBook;
        

        public void Awake()
        {
            Debug.Log("Awake!!!!");
            if (instance != null)
                return;
            instance = this;
            ingredientInventory = new List<Item>();    
            dessertInventory = new List<Item>();    
            otherInventory = new List<Item>();
            myCookBook = new CookBook();

            List<Item> recipe = new List<Item>();

            

            ShowItem();

            myCookBook.GetCookBook().Add(
                Item.ItemName.布丁, new Recipe(
                    new Item { itemName = Item.ItemName.雞蛋, quantity = 1 },
                    new Item { itemName = Item.ItemName.糖, quantity = 1 }, recipe)
            );
        }


        private void ShowItem()
        {
            ShowIngredientInventory = ingredientInventory;
            ShowDessertInventory = dessertInventory;
            ShowOtherInventory = otherInventory;
        }


        public void AddItem(Item item)
        {
            // 甜點增加

            bool itemExist = false;

            foreach (Item iL in GetInventory(item))
            {
                Debug.Log("判斷甜點數" + item.itemName + item.quantity);
                if (item.itemName == iL.itemName)
                {
                    itemExist = true;
                    Debug.Log("甜點數增加" + item.itemName + item.quantity);
                    Item.ItemName iN = item.itemName;
                    FindItem(new Item(iN, 0)).quantity += item.quantity ;
                    foreach(Item it in ingredientInventory)
                    {
                        Debug.Log(it.itemName.ToString() + " has " + it.quantity.ToString());
                    }
                    Debug.Log(FindItem(new Item(iN, 0)).quantity);
                    //FindItem(item).quantity += item.quantity;
                    //iL.quantity += item.quantity;
                    Debug.Log("引入的ithem甜點數變成" + item.itemName + item.quantity);
                    Debug.Log("甜點數變成" + iL.itemName + iL.quantity);
                    break;
                }
            }

            if (!itemExist)
            {
                Debug.Log("甜點" + item.itemName + item.quantity);
                GetInventory(item).Add(item);
            }

            // 顯示目前甜點
            ShowItem();
        }



        public void RemoveItem(Item item)
        {
            if (FindItem(item) != null && FindItem(item).quantity >= item.quantity)
            {
                FindItem(item).quantity -= item.quantity;
                if(FindItem(item).quantity == 0)
                {
                    GetInventory(item).Remove(FindItem(item));
                }
            }
        }

        public List<Item> GetInventory(Item item)
        {
            switch (Item.ItemType(item))
            {
                case 0:
                    return ingredientInventory;
                case 1:
                    return dessertInventory;
                case 2:
                    return otherInventory;
                default:
                    Debug.Log("can not find inventory");
                    return null;
            }
        }

        public Item FindItem(Item item)
        {
            foreach (Item iL in GetInventory(item))
            {
                if (iL.itemName == item.itemName)
                    return iL;
            }
            Debug.Log("cant find item");
            return null;
        }

        public List<Item> GetIngredientInventory()
        {
            return ingredientInventory;
        }

        public List<Item> GetDessertInventory()
        {
            return dessertInventory;
        }

        public List<Item> GetOtherInventory()
        {
            return otherInventory;
        }

        public Dictionary<Item.ItemName, Recipe> GetCookBook()
        {
            return myCookBook.GetCookBook();
        }
    }
}
