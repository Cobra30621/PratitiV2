using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class EndBattleUI : MonoBehaviour
{
    [SerializeField] GameObject _mainPanel;

    [SerializeField] GameObject lootUnitBarPrefab;
    [SerializeField] Transform transform_lootUnitBarPanel;

    public LootTable _lootTable;
    private Loot[] loots;
    public List<LootUnitBar> _lootUnitBars = new List<LootUnitBar>();
    
    // Start is called before the first frame update
    void Start()
    {
        Initialize();
    }

    public void Initialize(){
        SetDropItemCount();
        CreateAllLootUnitBars();
        AddDropItem();
        RefreshInfo();
    }  

    private void GetLootTable(){
        if(GameMediator.Instance.GetLootTable() != null)
            _lootTable = GameMediator.Instance.GetLootTable();
        else
            Debug.LogError("找不到掉落物品"+ GameMediator.Instance.GetLootTable());
    }

    public void SetDropItemCount(){
        GetLootTable();
        loots = _lootTable.DropItem();
        RefreshInfo();
    }

    public void AddDropItem(){
        foreach ( LootUnitBar bar in _lootUnitBars){
            bar.AddItem();
            Debug.Log($"<color=#8E00F3>{bar}掉落物品</color>");
        }
    }

    public void BackToMap(){
        GameMediator.Instance.BackToMap();
    }

    //===================================================
    //=================開啟包包介面========================
    //===================================================
    public void Open(){
        _mainPanel.SetActive(true);
        RefreshInfo();
    }

    public void Close(){
        _mainPanel.SetActive(false);
    }

    public void RefreshInfo(){
        foreach ( LootUnitBar bar in _lootUnitBars){
            bar.RefreshInfo();
        }
    }


    //===================================================
    //=================產生Bar介面========================
    //===================================================

    public void CreateAllLootUnitBars(){
        foreach ( Loot loot in loots ){
            if(loot._dropCount > 0)
                CreateLootUnitBar(loot);
        }
    }

    /// <summary>
    /// 製作一個CompostieStickerBar
    /// </summary>
    public void CreateLootUnitBar(Loot loot)
    {
        var g = Instantiate(lootUnitBarPrefab, transform_lootUnitBarPanel);

        var l = g.GetComponent<LootUnitBar>();
        l.Initialize(loot); 
        _lootUnitBars.Add(l);
    }
}



