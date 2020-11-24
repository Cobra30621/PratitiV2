using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using InventorySystem;

namespace TrainSystem {
	public class TrainManager : MonoBehaviour
	{
		public InventoryData inventoryData;

		//精靈列表    public List<Pratiti> pratitiData; 
		private List<Pratiti> pratitiData;
		public GameObject pratitiSlotGrid;
		public GameObject pratitiSlot;
		public Button[] toPage;//toPage[0]=>statPage;toPage[1]=>infoPage;

		//在trainmenu上顯示能力值
		public Text pratitiNameText, sexText, characteristicText;
		public Text hpText, atkText, defText, mAtkText, mDefText, agiText;
		public Text gpText, gpUseText;
		public int gpUse = 0;
		public Slider gpSlider;

		public Image pressureMaskImage;
		public Image nowWatch;    //頭貼顯示處
		private Pratiti pratiti;    //當前選中的Pratiti

		float trainUp = 1.0f;   //訓練後提升的數值
		int maxPressure = 15;   //最大壓力值

		private Button[] setTrainUp; //設定培養所提升的數值大小;現版本不會用到

		private void Awake()
		{
			inventoryData = GameObject.Find("GameData").GetComponent<InventoryData>();
			pratitiData = GameObject.Find("GameData").GetComponent<PratitiData>().pratitiData;
		}

		public void Initialize()
		{
			this.gameObject.transform.parent.gameObject.SetActive(true);
			RefreshPrattiScrollPanel();
			pratitiSlotGrid.transform.GetChild(0).gameObject.GetComponent<Button>().onClick.Invoke();
		}

		public void Escape()
		{
			this.gameObject.transform.parent.gameObject.SetActive(false);
		}


		public void SelectPratiti(int i)
		{
			//Debug.Log(i);
			pratiti = pratitiData[i];
		}

		void Update()
		{
			if (this.gameObject.activeSelf && Input.GetKeyDown(KeyCode.Escape))
			{
				Escape();
			}
		}
		public void RefreshPrattiScrollPanel()
		{
			for (int i = 0; i < 10; i++)
			{
				if (i < pratitiData.Count)
				{
					pratitiSlotGrid.transform.GetChild(i).transform.GetChild(0).gameObject.GetComponent<Image>().sprite = pratitiData[i].GetPratitiSprite();

					//顯示帕拉緹緹的名字與性別
					if (pratitiData[i].sex)
						pratitiSlotGrid.transform.GetChild(i).transform.GetChild(1).gameObject.GetComponent<Text>().text = pratitiData[i].pratitiBrand.pratitiName + "♂";
					else
						pratitiSlotGrid.transform.GetChild(i).transform.GetChild(1).gameObject.GetComponent<Text>().text = pratitiData[i].pratitiBrand.pratitiName + "♀";
				}
				else
				{
					pratitiSlotGrid.transform.GetChild(i).GetComponent<Button>().interactable = false;
					pratitiSlotGrid.transform.GetChild(i).transform.GetChild(0).gameObject.SetActive(false);
					pratitiSlotGrid.transform.GetChild(i).transform.GetChild(1).gameObject.SetActive(false);
				}
			}
		}

		//改變界面上顯示的帕拉提提資訊
		public void RefreshStatPanel()
		{
			toPage[0].onClick.Invoke();
			Debug.Log(pratiti);
			hpText.text = ((int)pratiti.hp).ToString();
			atkText.text = ((int)pratiti.atk).ToString();
			defText.text = ((int)pratiti.def).ToString();
			mAtkText.text = ((int)pratiti.mAtk).ToString();
			mDefText.text = ((int)pratiti.mDef).ToString();
			agiText.text = ((int)pratiti.agi).ToString();
			pratitiNameText.text = pratiti.pratitiName;
			sexText.text = (pratiti.sex) ? "♂" : "♀";
			pressureMaskImage.rectTransform.sizeDelta = new Vector2(240.0f * pratiti.pressure / maxPressure, 86.64999f);//改變Mask物件的寬度達成pressure槽減少的效果
			characteristicText.text = pratiti.characteristic.ToString();
			nowWatch.GetComponent<Image>().sprite = pratiti.GetPratitiSprite();

			gpText.text = pratiti.generalPoints.ToString();
			ValueChangeSlider(gpUse);
		}

		public void SliderChangeValue()
		{
			gpSlider.maxValue = pratiti.generalPoints;
			gpUse = (int)gpSlider.value;
			gpUseText.text = gpUse.ToString();
		}

		public void ValueChangeSlider(int value)
		{
			gpSlider.maxValue = pratiti.generalPoints;
			gpUse = value;
			gpUseText.text = gpUse.ToString();
			gpSlider.value = gpUse;
		}

		public void RefreshInfoPanel(Pratiti sD)
		{

		}

		//設定增加能力值的大小，現版本不會用到，故先註解起來
		// public void SetTrainUp(int setTrainUp)
		// {
		// 	trainUp=setTrainUp;	//按下按鈕傳入trainUp值；
		// }

		public void StatUp(int stat) 
		{
			/*if ((pratiti.pressure + gpUse) <= maxPressure)
			{
				switch (stat)   //根據傳入值提升某項能力
				{
					case 1:
						pratiti.hp += gpUse;
						break;
					case 2:
						pratiti.atk += gpUse;
						break;
					case 3:
						pratiti.def += gpUse;
						break;
					case 4:
						pratiti.mAtk += gpUse;
						break;
					case 5:
						pratiti.mDef += gpUse;
						break;
					case 6:
						pratiti.agi += gpUse;
						break;
				}
				pratiti.pressure = pratiti.pressure + (int)gpUse;
				pratiti.generalPoints -= gpUse;
				gpUse = 0;
				RefreshStatPanel();
				PratitiData.SavePratitiData(pratitiData);
			}
			else
			{
				Debug.Log("too much pressure");
			}*/
			switch (stat)   //根據傳入值提升某項能力
			{
				case 1:
					pratiti.hp += gpUse;
					break;
				case 2:
					pratiti.atk += gpUse;
					break;
				case 3:
					pratiti.def += gpUse;
					break;
				case 4:
					pratiti.mAtk += gpUse;
					break;
				case 5:
					pratiti.mDef += gpUse;
					break;
				case 6:
					pratiti.agi += gpUse;
					break;
			}
			pratiti.generalPoints -= gpUse;
			gpUse = 0;
			RefreshStatPanel();
			PratitiData.SavePratitiData(pratitiData);
			toPage[0].GetComponent<Button>().interactable = true;
			toPage[1].GetComponent<Button>().interactable = true;
		}

		public void StatUpDesseert(Item item)
		{
			int[] func = item.GetItemFunction(item);

			bool isConsume = false;

			for (int i = 0; i < 8; i++)
			{
				switch (i)
				{
					case 0:
						if (pratiti.hp + func[i] > pratiti.maxHp)
						{
							if (isConsume) 
							{
								break;
							}
							isConsume = false;
						}
						else
						{
							pratiti.hp += func[i];
							if(pratiti.hp > pratiti.maxHp)
							{
								pratiti.hp = pratiti.maxHp;
							}
							isConsume = true;
						}
						break;

					case 1:
						if (pratiti.atk + func[i] > pratiti.maxAtk)
						{
							if (isConsume)
							{
								break;
							}
							isConsume = false;
						}
						else
						{
							pratiti.atk += func[i];
							if (pratiti.atk > pratiti.maxAtk)
							{
								pratiti.atk = pratiti.maxAtk;
							}
							isConsume = true;
						}
						break;

					case 2:
						if (pratiti.def + func[i] > pratiti.maxDef)
						{
							if (isConsume)
							{
								break;
							}
							isConsume = false;
						}
						else
						{
							pratiti.def += func[i];
							if (pratiti.def > pratiti.maxDef)
							{
								pratiti.def = pratiti.maxDef;
							}
							isConsume = true;
						}
						break;

					case 3:
						if (pratiti.mAtk + func[i] > pratiti.maxMAtk)
						{
							if (isConsume)
							{
								break;
							}
							isConsume = false;
						}
						else
						{
							pratiti.mAtk += func[i];
							if (pratiti.mAtk > pratiti.maxMAtk)
							{
								pratiti.mAtk = pratiti.maxMAtk;
							}
							isConsume = true;
						}
						break;

					case 4:
						if (pratiti.mDef + func[i] > pratiti.maxMDef)
						{
							if (isConsume)
							{
								break;
							}
							isConsume = false;
						}
						else
						{
							pratiti.mDef += func[i];
							if (pratiti.mDef > pratiti.maxMDef)
							{
								pratiti.mDef = pratiti.maxMDef;
							}
							isConsume = true;
						}
						break;

					case 5:
						if (pratiti.agi + func[i] > pratiti.maxAgi)
						{
							if (isConsume)
							{
								break;
							}
							isConsume = false;
						}
						else
						{
							pratiti.agi += func[i];
							if (pratiti.agi > pratiti.maxAgi)
							{
								pratiti.agi = pratiti.maxAgi;
							}
							isConsume = true;
						}
						break;

					case 6:
						if (pratiti.pressure < 0)
						{
							if (isConsume)
							{
								break;
							}
							isConsume = false;
						}
						else
						{
							pratiti.pressure -= func[i];
							if (pratiti.pressure < 0)
							{
								pratiti.pressure = 0;
							}
							isConsume = true;
						}
						break;

					case 7:
						if (func[i] > 0)
						{
							pratiti.generalPoints += func[i];
							isConsume = true;
						}
						break;

					default:
						break;
				} 
			}

			if (isConsume)
			{
				inventoryData.RemoveItem(item);
			}
		}
	}
}