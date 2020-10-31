
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PratitiUI: MonoBehaviour
{
    private static PratitiUI instance;
    // UI
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
    public List<BagPratiti> _bagPratitis;
    public List<PratitiBar> _pratitiBars;
    public List<GameObject> _barGameObjects;
    public BagPratiti _selectedPratiti;
    public IAssetFactory factory;
    [SerializeField] GameObject pratitiBarPrefab;
    [SerializeField] Transform transform_PratitiBar;

    /// <summary>
    /// Awake is called when the script instance is being loaded.
    /// </summary>
    void Start()
    {
        instance = this;
        Initialize();
    }

    public void Initialize(){
        GetPratitiInfo();
        
        factory = MainFactory.GetAssetFactory();
        CreateAllPratitiBars();
        RefreshInfo();
    }   

    public static void Refresh(){
        if(instance != null)
        {
            instance.RefreshInfo();
        }
        
        
    }

    public void RefreshInfo(){
        GetPratitiInfo();
        // CreateAllPratitiBars(); // 翻新Bar
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
        // img_icon.sprite = factory.LoadPratitiSprite(_selectedPratiti._pratitiType);
    }

    public void GetPratitiInfo(){
        _pratitiSystem = GameMediator.Instance.GetPratitiSystem();
        _bagPratitis = GameMediator.Instance.GetBagPratitis();
        _selectedPratiti = GameMediator.Instance.GetSelectedPratiti();
    }

    public void CreateAllPratitiBars(){
        RemoveAllPratitiBarObject();
        foreach(BagPratiti pratiti in _bagPratitis){
            CreatePratitiBar(pratiti);
        }

    }

    /// <summary>
    /// 製作一個PratitiBar
    /// </summary>
    public void CreatePratitiBar(BagPratiti pratiti)
    {
        var g = Instantiate(pratitiBarPrefab, transform_PratitiBar);
        _barGameObjects.Add(g);

        var l = g.GetComponent<PratitiBar>();
        l.Initialize(pratiti); 
        _pratitiBars.Add(l);
    }

    public void RemoveAllPratitiBarObject(){       
        foreach(PratitiBar bar in _pratitiBars){
            Destroy(bar.gameObject);
        }
    }

    // 開啟介面方法
    public void OnStickerSelected(int index){
        if(_selectedPratiti._stickers[index] == null)
        {
            StickerSelectedUI.Show(); // 不太好的UI管理方法
            _pratitiSystem._SelectedStickerID = index;
        }
        else{
            Debug.Log("帕拉提提已經裝了貼紙");
        }
    }

    
}