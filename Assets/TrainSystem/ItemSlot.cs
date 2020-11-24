using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using InventorySystem;


namespace TrainSystem
{
    public class ItemSlot : MonoBehaviour
    {
        public Item thisItem;
        public Image itemImage;
        public Text itemName;
        public Text itemQuantity;
        public Text itemFunc;
        
        // Start is called before the first frame update
        void Start()
        {
            itemImage.sprite = thisItem.ItemSprite(thisItem);
            itemQuantity.text = thisItem.quantity.ToString();
            itemName.text = thisItem.itemName.ToString();
            itemFunc.text = "null";
        }
    }
}