using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using InventorySystem;
using Fungus;

public class SystemController
{
    private FungusManagerSystem m_FungusManager;
    private SceneChangeSystem m_sceneChangeSystem = null;
    private InventoryData m_inventoryData;
    public PratitiData m_PratitiData ;
    public BattleUseData m_battleUseData ;

    //------------------------------------------------------------------------
    #region Singleton模版
    private static SystemController _instance;
    public static SystemController Instance
    {
        get
        {
            if (_instance == null)
            {
                Debug.Log("產生SystemController");
                _instance = new SystemController();
            }
            return _instance;
        }
    }
    private SystemController()
    { }

    #endregion
    
    #region 初始化、更新、釋出、玩家輸入（如果有的話）
    public void Initinal()
    {
        //遊戲系統
        m_FungusManager = new FungusManagerSystem(this);
        m_sceneChangeSystem = new SceneChangeSystem(this);
        m_PratitiData = GameObject.Find("GameData").GetComponent<PratitiData>();
        m_battleUseData = GameObject.Find("GameData").GetComponent<BattleUseData>();
        m_inventoryData = GameObject.Find("GameData").GetComponent<InventoryData>();
    }

    // 釋放遊戲系統
    public void Release()
    {

    }

    // 更新
    public void Update()
    {

    }

    // 玩家輸入
    public void InputProcess()
    {
    }

    #endregion

    #region Pratiti方法


    // 新增帕拉緹緹到包包
    public void AddPratiti(Pratiti pratiti){
        m_PratitiData.pratitiData.Add(pratiti);
        PratitiData.SavePratitiData(m_PratitiData.pratitiData);
    }

    // 設置戰鬥時帕拉提提資料
    public void SetBattleData(Pratiti pratiti, Item getItem)
    {
        m_battleUseData.EnemyPratiti = pratiti;
        m_battleUseData.GetItem = getItem;
        m_battleUseData.SetPlayerData();
    }

    // 清除帕拉提提資料
    public void ClearPratitiData()
    {
        m_PratitiData.pratitiData.Clear();
        PratitiData.SavePratitiData(m_PratitiData.pratitiData);
        m_PratitiData.LoadPratitiData();
    }
    #endregion

    #region Inventory方法


    // 設定甜點數量
    public void RemoveItem(Item item)
    {
        m_inventoryData.RemoveItem(item);
    }

    // 增加甜點數量
    public void AddItem(Item item)
    {
        m_inventoryData = GameObject.Find("GameData").GetComponent<InventoryData>();
        m_inventoryData.AddItem(item);
    }

    // 取得甜點數量
    public int GetDesert(Item desert)
    {
        Debug.Log("desertname" + desert.itemName);
        return m_inventoryData.FindItem(desert).quantity;
    }
    #endregion


    #region SceneChangeSystem方法
    // 初始化地圖
    public void InitialMap()
    {
        m_sceneChangeSystem.InitialMap();
    }

    // 儲存小地圖的資料
    public void SetSmallMap(string smallMapName)
    {
        m_sceneChangeSystem.SetSmallMap(smallMapName);
    }

    // 轉場時儲存GameData, smallMap, PlayerPosition
    public void SaveMapData()
    {
        m_sceneChangeSystem.SaveMapData();
    }
    #endregion

    #region FungusManager方法
    // 取得 Flowchart

    public void EndBattle()
    {
        m_FungusManager.EndBattle();
    }

    public void PlayBlock(Flowchart talkFlowChart, string targetBlockName)
    {
        m_FungusManager.PlayBlock(talkFlowChart, targetBlockName);
    }

    public void PlayBlock(string talkFlowChart, string targetBlockName)
    {
        m_FungusManager.PlayBlock(talkFlowChart, targetBlockName);
    }


    /*
    public void EndBattle(int winOrLose)
    {
        m_FungusManager.EndBattle(winOrLose);
    }

    
    // 結束戰鬥回到地圖
    public void EndBattlePrice()
    {
        m_FungusManager.EndBattlePrice();
    }
    */
    #endregion
}
