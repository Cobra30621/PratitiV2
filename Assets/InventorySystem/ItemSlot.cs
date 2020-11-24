using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Sprites;
using TMPro;

namespace InventorySystem
{
    public class ItemSlot : MonoBehaviour
    {
        public Item thisItem;
        private Image itemImage;
        private Text itemName;
        private Text itemQuantity;
        
        // Start is called before the first frame update
        void Start()
        {
            itemImage = this.transform.GetChild(0).GetComponent<Image>();
            itemQuantity = this.transform.GetChild(1).GetComponent<Text>();
            itemName = this.transform.GetChild(2).GetComponent<Text>();
            itemImage.sprite = thisItem.ItemSprite(thisItem);
            itemQuantity.text = thisItem.quantity.ToString();
            itemName.text = thisItem.itemName.ToString();
        }
    }
}