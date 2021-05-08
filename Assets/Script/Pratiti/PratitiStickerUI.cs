
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

[System.Serializable]
public enum UIPanel{Pratiti=0, Sticker}; // 現在開啟的UI頁面
public enum TumbType{StickerUI, PratitiUI, CompostieSticker, StickerBox, EquipSticker, CloseUI, OpenUI}; // 教學箭頭顯示的UI

public class PratitiStickerUI : MonoBehaviour
{
    public static PratitiStickerUI instance;

    // UI
    [SerializeField] private GameObject _mainPanel;
    private Dictionary<UIPanel , GameObject> _mainPanels = new Dictionary<UIPanel , GameObject>();

    [Header("帕拉緹緹UI")]
    [SerializeField] private GameObject pratitiPanel;

    [SerializeField] private Text lab_name;
    [SerializeField] private Text lab_Attr;
    [SerializeField] private Text lab_Def;
    [SerializeField] private Text lab_Hp;
    [SerializeField] private Text lab_Speed;

    [SerializeField] private Image img_icon;
    private Sprite sprite_icon;

    [SerializeField] private Button[] StickerButtons;
    [SerializeField] private Image[] img_stickers;

    // Data
    public PratitiSystem _pratitiSystem;
    public BagPratiti _selectedPratiti;
    public IAssetFactory factory;

    
    
    [Header("StickerUI")]
    [SerializeField] GameObject stickerPanel;

    private ItemSystem _itemSystem;
    public Dictionary<StickerType, Sticker> _dicStickers ;
    public Dictionary<StickerType, StickerChip> _dicStickerChips;

    public List<CompositeStickerBar> _compostierStickerBars;

    [SerializeField] GameObject compositeStickerBarPrefab;
    [SerializeField] Transform transform_book;

    [Header("教學用")]
    [SerializeField] List<TeachingTumb> teachingTumbs;

    [Header("快樂草")]
    public Text lab_happyGrassNum;

    
    
    void Awake(){
        instance = this;
    }

    /// <summary>
    /// Awake is called when the script instance is being loaded.
    /// </summary>
    void Start()
    {
        Initialize();
    }

    public void Initialize(){
        GetPratitiInfo();
        
        factory = MainFactory.GetAssetFactory();
        RefreshInfo();
        InitSticker();
        RefreshHappyGrassInfo();
    }   

    public void Open(){
        if (_mainPanel.activeSelf)
        {
            GameMediator.Instance.SetUsingUI(false);
            _mainPanel.SetActive(false);
            RefreshInfo();
        }
        else if (!_mainPanel.activeSelf)
        {
            GameMediator.Instance.SetUsingUI(true);
            SelectPanel ( UIPanel.Sticker);
            _mainPanel.SetActive(true);
            RefreshInfo();
        }
    }

    public void Close(){
        GameMediator.Instance.SetUsingUI(false);
        _mainPanel.SetActive(false);

        EventManager.instance.onStateChange("關閉包包"); // 播放教學劇情
    }

    public static void Refresh(){
        if(instance != null)
        {
            instance.RefreshInfo();
        }
    }

    public void RefreshInfo(){
        GetPratitiInfo();
        PratitiAttr pratitiAttr = _selectedPratiti._pratitiAttr;

        Debug.Log("翻新屆面");
        lab_name.text = _selectedPratiti._name;
        lab_Attr.text = $"攻擊力: {pratitiAttr.rawAttr} <color=#8E00F3>+ {pratitiAttr.plusAttr}</color>" ;
        lab_Hp.text = $"血量: {pratitiAttr.rawHp} <color=#8E00F3>+ {pratitiAttr.plusHp}</color>" ;
        lab_Def.text =  $"防禦: {pratitiAttr.rawDef} <color=#8E00F3>+ {pratitiAttr.plusDef}</color>"  ;
        lab_Speed.text = $"速度: {pratitiAttr.rawSpeed} <color=#8E00F3>+ {pratitiAttr.plusSpeed}</color>"  ;

        img_icon.sprite = _selectedPratiti._pratitiData._icon;

        for(int i=0; i < 3; i++){
            BagPratiti pratiti = _pratitiSystem._selectedPratiti;
            if(pratiti._stickers[i] != null)
            {
                StickerType type = pratiti._stickers[i]._stickerType;
                StickerData stickerData = factory.LoadStickerData(type);
                img_stickers[i].sprite = stickerData._icon;
            }
            else{
                img_stickers[i].sprite = null;
            }
        }

        
        RefreshStickerInfo();
        
    }


    public void RefreshHappyGrassInfo(){
        if(_itemSystem != null)
            lab_happyGrassNum.text = _itemSystem.GetHappyGrassCount() + "";

    }

    public void GetPratitiInfo(){
        Debug.Log("GetPratitiInfo()");
        _pratitiSystem = GameMediator.Instance.GetPratitiSystem();
        Debug.Log("GetPratitiInfo()2");
        _selectedPratiti = GameMediator.Instance.GetSelectedPratiti();
        Debug.Log("GetPratitiInfo()3");

    }


    // 開啟介面方法
    public void OnStickerSelected(int index){
        StickerType type = _selectedPratiti._stickers[index]._stickerType;
        _itemSystem.SetSelectedStieckerType(type);
        // if(type == StickerType.Null)
        // {
        //     StickerSelectedUI.Show(); // 不太好的UI管理方法
        //     _pratitiSystem._SelectedStickerID = index;
        // }
        // else{
        //     Debug.Log($"帕拉提提的第{index}格已經裝了{type}貼紙");
        // }

        StickerSelectedUI.Show(); // 不太好的UI管理方法
        _pratitiSystem._SelectedStickerID = index;
        

        EventManager.instance.onStateChange("點三貼紙按鈕"); // 播放教學劇情
        
    }

    // ===============================================
    // Sticker
    // ===============================================

    public void InitSticker(){
        _itemSystem = GameMediator.Instance.GetItemSystem();
        // 將所有分頁的panel加入管禮器
        _mainPanels.Add(UIPanel.Pratiti, pratitiPanel);
        _mainPanels.Add(UIPanel.Sticker, stickerPanel);

        // stickerpanel 
        _dicStickers = _itemSystem._dicStickers;
        _dicStickerChips = _itemSystem._dicStickerChips;

        CreateAllCompostieStickerBars();
        SetCompositeTeachingTumb(); // 在攻擊貼紙上設置教學的箭頭
        RefreshStickerInfo();
    }  

  

    //===================================================
    //=================開啟分頁Panel方法===================
    //===================================================

    public void SelectPanel (int index){
        SelectPanel( (UIPanel) index);
    }

    public void SelectPanel (UIPanel key){
        foreach (GameObject panel in _mainPanels.Values)
        {
            panel.SetActive(false);
        }
        _mainPanels[key].SetActive(true);
        RefreshInfo();

        if(key == UIPanel.Pratiti)
            EventManager.instance.onStateChange("你給我裝備貼紙"); // 播放教學劇情
    }


    public void RefreshStickerInfo(){
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

    // 在攻擊貼紙上設置教學的箭頭
    private void SetCompositeTeachingTumb(){
        foreach (CompositeStickerBar bar in _compostierStickerBars)
        {
            if(bar.GetStickerType() == StickerType.Attr){
                TeachingTumb tumb = bar.gameObject.GetComponent<TeachingTumb>();

                if(tumb != null)
                    AddTeachingTumb(tumb);
                else
                    Debug.LogError("攻擊貼紙Bar上找不到TeachingTumb");
            }
        }
    }

    //===================================================
    //================= 教學手指 ============
    //===================================================

    public void ShowTumb(TumbType tumbType){
        foreach (TeachingTumb tumb in teachingTumbs)
        {
            if(tumb.GetTumbType() == tumbType)
                tumb.ShowTumb();
        }
    }

    public void CloseTumb(TumbType tumbType){
        foreach (TeachingTumb tumb in teachingTumbs)
        {
            if(tumb.GetTumbType() == tumbType)
                tumb.CloseTumb();
        }
    }

    public void AddTeachingTumb(TeachingTumb tumb){
        teachingTumbs.Add(tumb);
    }

}
