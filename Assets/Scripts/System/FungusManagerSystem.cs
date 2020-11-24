
using UnityEngine;
using Fungus;
using System;
using InventorySystem;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class FungusManagerSystem : IGameSystem
{
    public static Flowchart FungusPrice;
    public static Flowchart FungusMap;

    public FungusManagerSystem(SystemController SControl) : base(SControl)
	{
        Debug.Log("產生FungusManagerSystem" );
	}

    #region PlayBlock

    // 三種PlayBlock
    void PlayBlock(string targetBlockName)
    {
        // 尋找Block
        Block targetBlock = talkFlowChart.FindBlock(targetBlockName);
        // 當targetBlock有物件時執行Block
        if (targetBlock != null)
        {
            talkFlowChart.ExecuteBlock(targetBlock);
        }
        else
        {
            Debug.LogError("找不到在" + talkFlowChart.name + "裡的" + targetBlockName + "Block");
        }
    }


    // 給Flowchart與targetBlockName
    public void PlayBlock(Flowchart talkFlowChart, string targetBlockName)
    {
        // 尋找Block
        Block targetBlock = talkFlowChart.FindBlock(targetBlockName);
        // 當targetBlock有物件時執行Block
        if (targetBlock != null)
        {
            talkFlowChart.ExecuteBlock(targetBlock);
        }
        else
        {
            Debug.LogError("找不到在" + talkFlowChart.name + "裡的" + targetBlockName + "Block");
        }
    }

    // 給Flowchart與targetBlockName
    public void PlayBlock(string FlowChartName, string targetBlockName)
    {

        Flowchart flowchart = GetFlowchart(FlowChartName);
        Block targetBlock = flowchart.FindBlock(targetBlockName);
        // 當targetBlock有物件時執行Block
        if (targetBlock != null)
        {
            flowchart.ExecuteBlock(targetBlock);
        }
        else
        {
            Debug.LogError("找不到在" + flowchart.name + "裡的" + targetBlockName + "Block");
        }
    }


    public Flowchart GetFlowchart(string name)
    { 
        
        switch (name)
        {
            
            case "P":
                return GameObject.Find("FungusPrice").GetComponent<Flowchart>();
            case "M":
                return GameObject.Find("FungusMap").GetComponent<Flowchart>();
            case "S":
                return GameObject.Find("StoryFlow").GetComponent<Flowchart>();
            case "B":
                return GameObject.Find("FungusBattle").GetComponent<Flowchart>();
            default:
                Debug.Log("找不到flowchart");
                return GameObject.Find("FungusMap").GetComponent<Flowchart>();
        }
    }

    #endregion


    public void GetFungusPrice()
    {
        FungusPrice = GameObject.Find("FungusPrice").GetComponent<Flowchart>();
        if (FungusPrice == null)
        {
            Debug.Log("FungusManagerSystem找不到FungusPrice");
        }
    }

    public void GetFungusMap()
    {
        FungusMap = GameObject.Find("FungusMap").GetComponent<Flowchart>();
        if (FungusMap == null)
        {
            Debug.Log("FungusManagerSystem找不到FungusMap");
        }
    }


    #region 戰鬥相關方法
    // 結束戰鬥
    // 戰鬥勝利數入1。敗北輸入0
    public void EndBattle()
    { 
        // 獎勵場景判斷
        GetFungusPrice();
        talkFlowChart = FungusPrice;
        if(FungusPrice == null)
        {
            Debug.Log("找不到 FungusPrice");
            return;
        }
        UData.BattleEnd = true;
        //UData.BattleWin = (winOrLose == 1) ? true : false;
        switch (UData.battleResult)
        {
            case BattleResult.win:
                Win();
                break;
            case BattleResult.lose:
                Lose();
                break;
            case BattleResult.catches:
                Catch();
                break;
        }
 
        
    }

    // 贏了戰鬥
    private void Win()
    {
        //BattleUseData m_battleUseData = GameObject.Find("GameData").GetComponent<BattleUseData>();
        //m_SControl.AddItem(m_battleUseData.GetItem); // 增加甜點數量
        // 增加技能點數
        PlayBlock("WinBattle");
    }
    // 輸了戰鬥
    private void Lose()
    {
        // 增加壓力值

        PlayBlock("LoseBattle");
    }

    // 捕捉
    private void Catch()
    {

    }



    /*
     *
         private InventoryData m_inventoryData;
    public void checkPudin()
    {
        m_inventoryData = GameObject.Find("GameData").GetComponent<InventoryData>();
        if (m_inventoryData.ingredientInventory.)
    }

    // 結束戰鬥回到地圖
    public void EndBattlePrice()
    {
        GetFungusMap();
        talkFlowChart = FungusMap;
        if (FungusMap == null)
        {
            Debug.Log("找不到 FungusMap");
            return;
        }
        if (BattleEnd)
        {
            if (BattleWin)
            {
                PlayBlock("WinBattle");
            }
            else
            {
                PlayBlock("LoseBattle");
            }
            BattleEnd = false;
        }
    }

    */
    #endregion

    #region Inventory相關
    // 取得 Flowchart
    private Flowchart talkFlowChart;
    
    // 取的甜點數量，供Fungus使用
    public void SetFungusDessert(Item.ItemName dessertName)
	{
		// 要輸進去的甜點
		talkFlowChart = GameObject.Find("FungusData").GetComponent<Flowchart>();
		// talkFlowChart.SetIntegerVariable(dessertName, SystemController.Instance.GetDesert(dessertName);
		//Debug.Log(dessertName + talkFlowChart.GetIntegerVariable(dessertName));
	}


    
    #endregion
}
