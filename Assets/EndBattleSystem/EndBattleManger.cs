using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using InventorySystem;

public class EndBattleManger : MonoBehaviour
{
    private PratitiBattleGrowth pratitiBattleGrowth;
    private BattleUseData m_battleUseData;
    private InventoryData inventoryData;

    public GameObject endBattlePanel;
    public GameObject losePanel;

    public bool isWin;

    public Text pratitiNameText;
    public Text hpText, atkText, defText, mAtkText, mDefText, agiText;
    public Text gpText, gpUseText;
    public int gpUse;
    public Slider gpSlider;

    private Pratiti pratiti;
    public GameObject pratitiImage;
    public GameObject itemSlotGrid;
    public GameObject itemSlot;
    private GameObject cloneSlot;

    public GameObject statUps;
    public Button[] statUp; 

    //勝利老虎機
    //public GameObject winRoller;
    public bool isRoll = false;
    public Button winRollerButton;
    private int rollRange = 6;
    private int rollResult = 1;
    private int timeCount;

    private void Awake() 
    {
        pratitiBattleGrowth = new PratitiBattleGrowth();
        m_battleUseData = GameObject.Find("GameData").GetComponent<BattleUseData>();
        inventoryData = GameObject.Find("GameData").GetComponent<InventoryData>();

        pratiti = m_battleUseData.GetPlayerPratiti();
    }

    void Start()
    {
        RefreshEndBattlePanel_Win();

        winRollerButton.onClick.AddListener(delegate () { 
            isRoll = !isRoll;
            pratiti.winTimer = 0;
        });

        for (int i = 0; i<=5; i++)
        {
            switch (i)
            {
                case 0:
                    statUp[i].onClick.AddListener(delegate() {
                        Debug.Log("press");
                        if (pratiti.hp + (float)gpUse <= pratiti.maxHp)
                        {
                            pratiti.hp += gpUse;
                            pratiti.generalPoints -= gpUse;
                            gpUse = 0;
                        }
                        else
                        {
                            gpUse = (int)(gpUse - (pratiti.maxHp - pratiti.hp));//把gpUse的值變成沒用到的
                            pratiti.hp = pratiti.maxHp;//pratitiStat有必要是float嗎；
                        }
                        Debug.Log("plus");
                        ValueChangeSlider(gpUse);
                        });
                        Debug.Log(pratiti.hp);
                    break;
                case 1:
                    statUp[i].onClick.AddListener(delegate () {
                        if (pratiti.atk + (float)gpUse <= pratiti.maxAtk)
                        {
                            pratiti.atk += gpUse;
                            pratiti.generalPoints -= gpUse;
                            gpUse = 0;
                        }
                        else
                        {
                            gpUse = (int)(gpUse - (pratiti.maxAtk - pratiti.atk));
                            pratiti.atk = pratiti.maxAtk;
                        }
                        ValueChangeSlider(gpUse);
                    });
                    Debug.Log(pratiti.atk);
                    break;
                case 2:
                    statUp[i].onClick.AddListener(delegate () {
                        if (pratiti.def + (float)gpUse <= pratiti.maxDef)
                        {
                            pratiti.def += gpUse;
                            pratiti.generalPoints -= gpUse;
                            gpUse = 0;
                        }
                        else
                        {
                            gpUse = (int)(gpUse - (pratiti.maxDef - pratiti.def));
                            pratiti.def = pratiti.maxDef;
                        }
                        ValueChangeSlider(gpUse);
                    });
                    break;
                case 3:
                    statUp[i].onClick.AddListener(delegate () {
                        if (pratiti.mAtk + (float)gpUse <= pratiti.maxMAtk)
                        {
                            pratiti.mAtk += gpUse;
                            pratiti.generalPoints -= gpUse;
                            gpUse = 0;
                        }
                        else
                        {
                            gpUse = (int)(gpUse - (pratiti.maxMAtk - pratiti.mAtk));
                            pratiti.mAtk = pratiti.maxMAtk;
                        }
                        ValueChangeSlider(gpUse);
                    });
                    break;
                case 4:
                    statUp[i].onClick.AddListener(delegate () {
                        if (pratiti.mDef + (float)gpUse <= pratiti.maxMDef)
                        {
                            pratiti.mDef += gpUse;
                            pratiti.generalPoints -= gpUse;
                            gpUse = 0;
                        }
                        else
                        {
                            gpUse = (int)(gpUse - (pratiti.maxMDef - pratiti.mDef));
                            pratiti.mDef = pratiti.maxMDef;
                        }
                        ValueChangeSlider(gpUse);
                    });
                    break;
                case 5:
                    statUp[i].onClick.AddListener(delegate () {
                        if (pratiti.agi + (float)gpUse <= pratiti.maxAgi)
                        {
                            pratiti.agi += gpUse;
                            pratiti.generalPoints -= gpUse;
                            gpUse = 0;
                        }
                        else
                        {
                            gpUse = (int)(gpUse - (pratiti.maxAgi - pratiti.agi));
                            pratiti.agi = pratiti.maxAgi;
                        }
                        ValueChangeSlider(gpUse);
                    });
                    break;
                default:
                    break;
            }
        }

        ValueChangeSlider(0);
    }

    public void RefreshEndBattlePanel_Win()
    {
        pratitiImage.GetComponent<Image>().sprite = pratiti.GetPratitiSprite();
        cloneSlot = Instantiate(itemSlot, itemSlotGrid.transform);
        cloneSlot.GetComponent<ItemSlot>().thisItem = m_battleUseData.GetItem;

        Debug.Log("RefreshEndBattlePanel_Win");
        inventoryData.AddItem(m_battleUseData.GetItem);

        pratiti.generalPoints += 30;
        pratiti.winTimer++;

        int gate = 20;
        if (UnityEngine.Random.Range(1, 101) < gate)
        {
            pratiti.generalPoints += 50;
        }

        ShowPratitiStat();
    }

    public void ShowPratitiStat()
    {
        hpText.text = ((int)pratiti.hp).ToString();
        atkText.text = ((int)pratiti.atk).ToString();
        defText.text = ((int)pratiti.def).ToString();
        mAtkText.text = ((int)pratiti.mAtk).ToString();
        mDefText.text = ((int)pratiti.mDef).ToString();
        agiText.text = ((int)pratiti.agi).ToString();
        pratitiNameText.text = pratiti.pratitiName;
        gpText.text = pratiti.generalPoints.ToString();
        gpSlider.maxValue = pratiti.generalPoints;
    }

    void Update()
    {
        ShowPratitiStat();
    }

    public void SliderChangeValue()
    {
        gpUse = (int)gpSlider.value;
        gpUseText.text = gpUse.ToString();
        ShowPratitiStat();

        if (gpSlider.value == 0)
        {
            foreach (Button bt in statUp)
                bt.interactable = false;
        }
        else
        {
            foreach (Button bt in statUp)
                bt.interactable = true;
        }
    }
    public void ValueChangeSlider(int value)
    {
        if (value > pratiti.generalPoints)
        {
            return;
        }

        gpUse = value;
        gpUseText.text = gpUse.ToString();
        gpSlider.maxValue = pratiti.generalPoints;
        gpSlider.value = gpUse;
        if (pratiti.generalPoints == 0)
            gpSlider.value = 1;
        ShowPratitiStat();
        Debug.Log(gpUse);
        Debug.Log(pratiti.generalPoints);

        if (value == 0)
        {
            foreach (Button bt in statUp)
                bt.interactable = true;
        }
        else
        {
            foreach (Button bt in statUp)
                bt.interactable = true;
        }
    }

    public void ShowStatUps()
    {
        if(gpSlider.value != 0)
        {
            statUps.SetActive(true);
        }
    }

    public void WinRoller()
    {
        if (pratiti.winTimer < 3)
            return;
        winRollerButton.onClick.AddListener(delegate () { winRollerButton.interactable = false; });

        if ((timeCount % 30 == 0) & (isRoll))
        {
            rollResult = rollResult + 1;
            if (rollResult > rollRange)
                rollResult = 1;
            Debug.Log(rollResult);
            Debug.Log(timeCount);
            winRollerButton.gameObject.transform.GetChild(0).GetComponent<Text>().text = rollResult.ToString();
        }
        timeCount++;
    }
}
