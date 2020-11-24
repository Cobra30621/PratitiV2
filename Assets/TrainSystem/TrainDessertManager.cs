using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using InventorySystem;
using UnityEngine.UI;

namespace TrainSystem
{
    public class TrainDessertManager : MonoBehaviour
    {
        private InventoryData inventoryData;
        public GameObject itemSlotGrid;
        public GameObject itemSlot;
        public GameObject cloneSlot;
        public GameObject trainManager;

        public void Initialize()
        {
            inventoryData = GameObject.Find("GameData").GetComponent<InventoryData>();
            trainManager = GameObject.Find("TrainManager");
            this.gameObject.SetActive(true);
            DeetoryDessertPanel();
            RefreshDessertPanel();
        }

        public void RefreshDessertPanel()
        {
            foreach(Item item in inventoryData.GetDessertInventory())
            {
                cloneSlot = Instantiate(itemSlot, itemSlotGrid.transform);
                cloneSlot.GetComponent<TrainSystem.ItemSlot>().thisItem = item;
                Debug.Log(item.quantity);
                cloneSlot.GetComponent<Button>().onClick.AddListener(delegate ()
                {
                    trainManager.GetComponent<TrainManager>().StatUpDesseert(item);
                    trainManager.GetComponent<TrainManager>().RefreshStatPanel();
                    DeetoryDessertPanel();
                    RefreshDessertPanel();
                });
            }
            foreach(Item item in inventoryData.GetIngredientInventory())
            {
                cloneSlot = Instantiate(itemSlot, itemSlotGrid.transform);
                cloneSlot.GetComponent<TrainSystem.ItemSlot>().thisItem = item;
                cloneSlot.GetComponent<Button>().onClick.AddListener(delegate()
                {
                    trainManager.GetComponent<TrainManager>().StatUpDesseert(item);
                    trainManager.GetComponent<TrainManager>().RefreshStatPanel();
                    DeetoryDessertPanel();
                    RefreshDessertPanel();
                });
            }
        }

        public void DeetoryDessertPanel()
        {
            foreach (Transform slot in itemSlotGrid.transform)
            {
                Destroy(slot.gameObject);
            }
        }

        void Update()
        {
            if (Input.GetMouseButtonDown(1))
            {
                DeetoryDessertPanel();
                this.gameObject.SetActive(false);
            }
        }
    }
}
