using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DessertSystem;
using QuestSystem;


namespace InventorySystem
{
    public class InventoryManager : MonoBehaviour
    {
        static InventoryManager instance;

        private GameObject inventoryPanel;
        private InventoryData inventoryData; 
        
        [SerializeField]
        private GameObject slotGrid;   //for normal_bag
        [SerializeField]
        private GameObject cookDessertGrid;
        [SerializeField]
        private GameObject cookIngredientGrid;

        public GameObject TitleWord;

        private GameObject itemSlot;   //for normal_bag
        private GameObject dessertSlot;
        private GameObject ingredientSlot;
        private GameObject cloneSlot;

        private GameObject cloneSlotDessert;
        private GameObject cloneSlotIngredient;

        [SerializeField]
        private GameObject cookPanel;
        [SerializeField]
        private GameObject cookButton;
        //private Text itemInfo;
        private TextMesh itemName;

        private Item selectItem;
        private List<Item> ingredientList;

        private GameObject dessertBigImage;
        private GameObject lightImage;

        Color Transparent;
        void Awake()
        {
            if (instance != null)
                Destroy(this);
            instance = this;

            inventoryData = GameObject.Find("GameData").GetComponent<InventoryData>();
            Debug.Log(inventoryData);
            itemSlot = Resources.Load<GameObject>("ItemSlot");
            dessertSlot = Resources.Load<GameObject>("DessertSlot");
            ingredientSlot = Resources.Load<GameObject>("ingredientSlot");

            ingredientList = new List<Item>();
        }

        public void Initialize()
        {
            this.gameObject.transform.parent.gameObject.SetActive(true);
            if (cookPanel.activeSelf)
            {
                cookPanel.SetActive(false);
            }

            RefreshIngredientInventory();
        }

        public void Escape()
        {
            this.gameObject.transform.parent.gameObject.SetActive(false);
        }

        void Update()
        {
            if (this.gameObject.activeSelf && Input.GetKeyDown(KeyCode.Escape))
            {
                Escape();
            }
        }

        public void DestorySlotGrids()
        {
            if (slotGrid.activeInHierarchy)
            {
                foreach (Transform slot in slotGrid.transform)
                {
                    Destroy(slot.gameObject);
                }
            }

            if (cookDessertGrid.activeInHierarchy) 
            {
                foreach (Transform slot in cookDessertGrid.transform)
                {
                    Destroy(slot.gameObject);
                }
            }

            if (cookIngredientGrid.activeInHierarchy)
            {
                foreach (Transform slot in cookIngredientGrid.transform)
                {
                    Destroy(slot.gameObject);
                }
            }
        }

        public void RefreshIngredientInventory() 
        {
            TitleWord.GetComponent<TextMeshProUGUI>().text = "Ingredients";

            DestorySlotGrids();

            for (int i = 0;i < inventoryData.GetIngredientInventory().Count;i++)
            {
                cloneSlot = Instantiate(itemSlot,slotGrid.transform);
                cloneSlot.GetComponent<ItemSlot>().thisItem = inventoryData.GetIngredientInventory()[i];
            }
        }

        public void RefreshDessertInventory()
        {
            TitleWord.GetComponent<TextMeshProUGUI>().text = "Desserts" ;

            DestorySlotGrids();

            foreach (KeyValuePair<Item.ItemName, Recipe> cB in inventoryData.GetCookBook())
            {
                cloneSlot = Instantiate(itemSlot, slotGrid.transform); 
                cloneSlot.GetComponent<ItemSlot>().thisItem = new Item { itemName =  cB.Key };
                bool isblack = true;
                foreach (Item item in inventoryData.GetDessertInventory())   
                {
                    if (item.itemName == cB.Key)
                    {
                        isblack = false;
                        //cloneSlot.GetComponent<Button>().onClick.AddListener(delegate () { RefreshItemInfo(cloneSlot); });
                        
                        cloneSlot.GetComponent<ItemSlot>().thisItem = item;
                    }
                }
                if (isblack)
                {
                    cloneSlot.transform.GetChild(0).GetComponent<Image>().color = new Color(0, 0, 0);
                    cloneSlot.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = "";
                }
            }
             
        }

        public void RefreshOtherInventory()
        {
            TitleWord.GetComponent<TextMeshProUGUI>().text = "Others" ;

            DestorySlotGrids();

            for (int g = 0; g < slotGrid.transform.childCount; g++)
            {
                Destroy(slotGrid.transform.GetChild(g).gameObject);
            }

            for (int i = 0; i < inventoryData.GetOtherInventory().Count; i++)
            {
                cloneSlot = Instantiate(itemSlot, slotGrid.transform);
                cloneSlot.GetComponent<ItemSlot>().thisItem = inventoryData.GetOtherInventory()[i];
            }
        }

        public void RefreshCookPanel()
        {
            print("refresh cookpanel");
            cookPanel.SetActive(true);
            TitleWord.GetComponent<TextMeshProUGUI>().text = "Cook";

            DestorySlotGrids();

            foreach (Transform slot in cookDessertGrid.transform)
                Destroy(slot.gameObject);

            foreach (KeyValuePair<Item.ItemName, Recipe> cB in inventoryData.GetCookBook())
            {
                cloneSlotDessert = Instantiate(dessertSlot, cookDessertGrid.transform);
                cloneSlotDessert.GetComponent<ItemSlot>().thisItem = new Item { itemName = cB.Key };
                cloneSlotDessert.GetComponent<Button>().onClick.AddListener(delegate () { RefreshCookInfo(cloneSlotDessert.GetComponent<ItemSlot>().thisItem); });
                foreach (Item item in inventoryData.GetDessertInventory())
                {
                    if (item.itemName == cB.Key)
                    {
                        cloneSlotDessert.GetComponent<ItemSlot>().thisItem = item;
                    }
                }
                cloneSlotDessert.transform.GetChild(2).transform.GetComponent<TextMeshProUGUI>().text = cB.Key.ToString();
            }
        }

        public void RefreshCookInfo(Item slotItem)
        {
            print("refresh cookinfo");

            ingredientList.Clear();

            foreach (Transform slot in cookIngredientGrid.transform)                
                Destroy(slot.gameObject);
                

            //infoPanel.SetActive(true);
            selectItem = slotItem;
            bool enough = true;
            
            //檢測inventory中是否有足夠的食材
            foreach (Item ingredient in inventoryData.GetCookBook()[selectItem.itemName].recipe) 
            {
                cloneSlotIngredient = Instantiate(ingredientSlot, cookIngredientGrid.transform);

                if (inventoryData.FindItem(ingredient) != null)
                {
                    cloneSlotIngredient.GetComponent<ItemSlot>().thisItem = new Item { itemName = inventoryData.FindItem(ingredient).itemName, quantity = inventoryData.FindItem(ingredient).quantity };
                    cloneSlotIngredient.transform.GetChild(3).GetComponent<TextMeshProUGUI>().text = inventoryData.FindItem(ingredient).quantity.ToString();
                    if (inventoryData.FindItem(ingredient).quantity >= ingredient.quantity)
                    {
                        ingredientList.Add(ingredient);
                    }
                    else
                    {
                        enough = false;
                    }
                }
                else
                {
                    enough = false;
                    cloneSlotIngredient.GetComponent<ItemSlot>().thisItem = new Item { itemName = ingredient.itemName, quantity = ingredient.quantity };
                    cloneSlotIngredient.transform.GetChild(3).GetComponent<TextMeshProUGUI>().text = "0";
                }
            }

            cookButton.GetComponent<Button>().interactable = enough;
        }

        public void Cook()
        {
            print("Cook");
            //移出inventory內的ingredient
            foreach (Item neededIngredient in ingredientList)
            {
                foreach(Item ingredient in inventoryData.GetIngredientInventory()) {
                    if(neededIngredient.itemName == ingredient.itemName) {
                        ingredient.quantity -= neededIngredient.quantity;
                        if(ingredient.quantity == 0)
                        {
                            inventoryData.RemoveItem(ingredient);
                            break;
                        }
                    }
                }
            }

            inventoryData.AddItem(new Item { itemName = selectItem.itemName, quantity = 1 });
            RefreshCookPanel();
            RefreshCookInfo(selectItem);
        }

        public IEnumerator FadeIn()
        {
            lightImage.GetComponent<Image>().CrossFadeAlpha(1, 0.125f, false);
            dessertBigImage.GetComponent<Image>().CrossFadeAlpha(1, 0.125f, false);
            yield return null;
        }
    }
}
