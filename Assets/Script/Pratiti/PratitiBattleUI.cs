
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PratitiBattleUI: MonoBehaviour
{
    private static PratitiBattleUI instance;

    // Data
    public PratitiBattleSystem pratitiBattleSystem;
    public IAssetFactory factory;
    public List<BagPratiti> _bagPratitis;
    public List<PratitiBattleBar> _pratitiBars;
    public List<GameObject> _barGameObjects;
    [SerializeField] GameObject pratitiBarPrefab;
    

    // 共同UI
    [SerializeField] private GameObject _panel;

    // 1P
    [Header("1P")]
    public BagPratiti selectedPratiti_1P;

    [SerializeField] private Text lab_name_1P;
    [SerializeField] private Text lab_Attr_1P;
    [SerializeField] private Text lab_Def_1P;
    [SerializeField] private Text lab_Hp_1P;
    [SerializeField] private Text lab_Speed_1P;
    [SerializeField] private Image img_icon_1P;
    private Sprite sprite_icon_1P;
    [SerializeField] private Button[] StickerButtons_1P;
    [SerializeField] private Image[] img_stickers_1P;
    [SerializeField] Transform transform_PratitiBar_1P;

    // 2P
    [Header("2P")]
    public BagPratiti selectedPratiti_2P;

    [SerializeField] private Text lab_name_2P;
    [SerializeField] private Text lab_Attr_2P;
    [SerializeField] private Text lab_Def_2P;
    [SerializeField] private Text lab_Hp_2P;
    [SerializeField] private Text lab_Speed_2P;
    [SerializeField] private Image img_icon_2P;
    private Sprite sprite_icon_2P;
    [SerializeField] private Button[] StickerButtons_2P;
    [SerializeField] private Image[] img_stickers_2P;
    [SerializeField] Transform transform_PratitiBar_2P;

    

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
        CreateAllPratitiBarsForAllPlayer();
        RefreshInfo();
    }   

    public void Open(){
        if (_panel.activeSelf)
        {
            GameMediator.Instance.SetUsingUI(false);
            _panel.SetActive(false);
        }
        else if (!_panel.activeSelf)
        {
            GameMediator.Instance.SetUsingUI(true);
            _panel.SetActive(true);
            RefreshInfo();
        }
    }

    public void Close(){
        GameMediator.Instance.SetUsingUI(false);
        _panel.SetActive(false);
    }

    public static void Refresh(){
        if(instance != null)
        {
            instance.RefreshInfo();
        }
    }

    public void RefreshInfo(){
        GetPratitiInfo();
        RefreshInfo_1P();
        RefreshInfo_2P();

        CreateAllPratitiBarsForAllPlayer();

    }

    

    private void RefreshInfo_1P(){
        PratitiAttr pratitiAttr = selectedPratiti_1P._pratitiAttr;
        Debug.Log("翻新屆面1P");
        lab_name_1P.text = selectedPratiti_1P._name;
        lab_Attr_1P.text = $"攻擊力: {pratitiAttr.rawAttr} <color=#8E00F3>+ {pratitiAttr.plusAttr}</color>" ;
        lab_Hp_1P.text = $"血量: {pratitiAttr.rawHp} <color=#8E00F3>+ {pratitiAttr.plusHp}</color>" ;
        lab_Def_1P.text =  $"防禦: {pratitiAttr.rawDef} <color=#8E00F3>+ {pratitiAttr.plusDef}</color>"  ;
        lab_Speed_1P.text = $"速度: {pratitiAttr.rawSpeed} <color=#8E00F3>+ {pratitiAttr.plusSpeed}</color>"  ;

        img_icon_1P.sprite = selectedPratiti_1P._pratitiData._icon;

        for(int i=0; i < 3; i++){
            BagPratiti pratiti = pratitiBattleSystem.selectedPratiti_1P;
            if(pratiti._stickers[i] != null)
            {
                StickerType type = pratiti._stickers[i]._stickerType;
                StickerData stickerData = factory.LoadStickerData(type);
                img_stickers_1P[i].sprite = stickerData._icon;
            }
            else{
                img_stickers_1P[i].sprite = null;
            }
        }
    }

    private void RefreshInfo_2P(){
        PratitiAttr pratitiAttr = selectedPratiti_2P._pratitiAttr;
        Debug.Log("翻新屆面2P");
        lab_name_2P.text = selectedPratiti_2P._name;
        lab_Attr_2P.text = $"攻擊力: {pratitiAttr.rawAttr} <color=#8E00F3>+ {pratitiAttr.plusAttr}</color>" ;
        lab_Hp_2P.text = $"血量: {pratitiAttr.rawHp} <color=#8E00F3>+ {pratitiAttr.plusHp}</color>" ;
        lab_Def_2P.text =  $"防禦: {pratitiAttr.rawDef} <color=#8E00F3>+ {pratitiAttr.plusDef}</color>"  ;
        lab_Speed_2P.text = $"速度: {pratitiAttr.rawSpeed} <color=#8E00F3>+ {pratitiAttr.plusSpeed}</color>"  ;

        img_icon_2P.sprite = selectedPratiti_2P._pratitiData._icon;

        for(int i=0; i < 3; i++){
            BagPratiti pratiti = pratitiBattleSystem.selectedPratiti_2P;
            if(pratiti._stickers[i] != null)
            {
                StickerType type = pratiti._stickers[i]._stickerType;
                StickerData stickerData = factory.LoadStickerData(type);
                img_stickers_2P[i].sprite = stickerData._icon;
            }
            else{
                img_stickers_2P[i].sprite = null;
            }
        }
    }

    public void GetPratitiInfo(){
        pratitiBattleSystem = GameMediator.Instance.GetPratitiBattleSystem();
        _bagPratitis = GameMediator.Instance.GetBagPratitis();
        selectedPratiti_1P = pratitiBattleSystem.selectedPratiti_1P;
        selectedPratiti_2P = pratitiBattleSystem.selectedPratiti_2P;
        // startPratiti_1P = pratitiBattleSystem.startPratiti_1P;
        // startPratiti_2P = pratitiBattleSystem.startPratiti_2P;
    }

    public void CreateAllPratitiBarsForAllPlayer(){
        RemoveAllPratitiBarObject();
        CreateAllPratitiBars(transform_PratitiBar_1P, 1);
        CreateAllPratitiBars(transform_PratitiBar_2P, 2);
    }

    public void CreateAllPratitiBars(Transform trans , int playerID){
        foreach(BagPratiti pratiti in _bagPratitis){
            CreatePratitiBar(pratiti, trans , playerID);
        }

    }

    /// <summary>
    /// 製作一個PratitiBar
    /// </summary>
    public void CreatePratitiBar(BagPratiti pratiti, Transform trans, int playerID)
    {
        var g = Instantiate(pratitiBarPrefab, trans);
        _barGameObjects.Add(g);

        var l = g.GetComponent<PratitiBattleBar>();
        l.Initialize(pratiti, playerID); 
        _pratitiBars.Add(l);
        Debug.Log($"產生{pratiti._pratitiType}類，編號{pratiti._ID}的帕拉提提Bar");
    }

    public void RemoveAllPratitiBarObject(){       
        foreach(PratitiBar bar in _pratitiBars){
            if(bar != null)
                Destroy(bar.gameObject);
        }
    }

}