using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[System.Serializable]
public enum BagUIPanel{Material=0, Sticker, Stone};

public class BagUI : MonoBehaviour
{
    private ItemSystem _itemSystem;
    public Dictionary<StickerType, Sticker> _dicStickers ;
    public Dictionary<StickerType, StickerChip> _dicStickerChips;

    public List<CompositeStickerBar> _compostierStickerBars;

    [SerializeField] GameObject compositeStickerBarPrefab;
    [SerializeField] Transform transform_book;

    
    [SerializeField] GameObject _mainPanel;
    // 分頁
    private Dictionary<BagUIPanel , GameObject> _panels = new Dictionary<BagUIPanel , GameObject>();
    [SerializeField] GameObject materialPanel;
    [SerializeField] GameObject stickerPanel;
    [SerializeField] GameObject stonePanel;


    // Start is called before the first frame update
    void Start()
    {
        Initialize();
    }

    public void Initialize(){
        _itemSystem = GameMediator.Instance.GetItemSystem();
        // 將所有分頁的panel加入管禮器
        _panels.Add(BagUIPanel.Material, materialPanel);
        _panels.Add(BagUIPanel.Sticker, stickerPanel);
        _panels.Add(BagUIPanel.Stone, stonePanel);

        // stickerpanel 
        _dicStickers = _itemSystem._dicStickers;
        _dicStickerChips = _itemSystem._dicStickerChips;

        CreateAllCompostieStickerBars();
        RefreshInfo();
    }  

    //===================================================
    //=================開啟包包介面========================
    //===================================================
    public void Open(){
        _mainPanel.SetActive(true);
        SelectPanel ( BagUIPanel.Sticker);
        RefreshInfo();
    }

    public void Close(){
        _mainPanel.SetActive(false);
    }

    //===================================================
    //=================開啟分頁Panel方法===================
    //===================================================

    public void SelectPanel (int index){
        SelectPanel( (BagUIPanel) index);
    }

    public void SelectPanel (BagUIPanel key){
        foreach (GameObject panel in _panels.Values)
        {
            panel.SetActive(false);
        }
        _panels[key].SetActive(true);
        RefreshInfo();
    }


    public void RefreshInfo(){
        // StickerPanel的資訊
        foreach(CompositeStickerBar bar in _compostierStickerBars){
            bar.RefreshInfo();
        }
    }

    //===================================================
    //=================開啟分頁StickerPanel方法============
    //===================================================


    public void CompositeStickerChip(StickerType type){
        _itemSystem.CompositeStickerChip(type);
    }

    public void CreateAllCompostieStickerBars(){
        foreach (StickerType type in Enum.GetValues( typeof( StickerType ) )){
            if (type != StickerType.Null)
            {
                Sticker sticker = _dicStickers[type];
                StickerChip chip = _dicStickerChips[type];
                CreateCompostieStickerBar(sticker, chip);
            }
        }
    }

    /// <summary>
    /// 製作一個CompostieStickerBar
    /// </summary>
    public void CreateCompostieStickerBar(Sticker sticker, StickerChip chip)
    {
        var g = Instantiate(compositeStickerBarPrefab, transform_book);
        // _barGameObjects.Add(g);

        var l = g.GetComponent<CompositeStickerBar>();
        l.Initialize(sticker, chip); 
        l.SetBagUI(this);
        _compostierStickerBars.Add(l);
        // Debug.Log($"產生{pratiti._pratitiType}類，編號{pratiti._ID}的帕拉提提Bar");
    }
}
