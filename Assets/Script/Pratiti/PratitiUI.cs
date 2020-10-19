
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PratitiUI: MonoBehaviour
{
    private static PratitiUI instance;
    [SerializeField] private Text lab_name;
    [SerializeField] private Text lab_Attr;
    [SerializeField] private Text lab_Def;
    [SerializeField] private Text lab_Hp;
    [SerializeField] private Text lab_Speed;

    [SerializeField] private Image img_icon;
    private Sprite sprite_icon;

    public List<BagPratiti> _bagPratitis;
    public BagPratiti _startPratiti;
    public IAssetFactory factory;
    [SerializeField] GameObject pratitiBarPrefab;
    [SerializeField] Transform transform_PratitiBar;

    /// <summary>
    /// Awake is called when the script instance is being loaded.
    /// </summary>
    void Awake()
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
        instance.GetPratitiInfo();
        instance.RefreshInfo();
    }


    public void GetPratitiInfo(){
        _bagPratitis = GameMediator.Instance.GetBagPratitis();
        _startPratiti = GameMediator.Instance.GetStartPratiti();
    }

    
    public void RefreshInfo(){
        lab_name.text = _startPratiti._name;
        lab_Attr.text = "攻擊力:" + _startPratiti._pratitiAttr.rawAttr ;
        lab_Hp.text = "血量:" + _startPratiti._pratitiAttr.rawHp ;
        lab_Def.text =  "防禦:" + _startPratiti._pratitiAttr.rawDef ;
        lab_Speed.text = "速度:" + _startPratiti._pratitiAttr.rawSpeed ;

        img_icon.sprite = factory.LoadPratitiSprite(_startPratiti._pratitiType);
    }

    public void CreateAllPratitiBars(){
        foreach(BagPratiti pratiti in _bagPratitis){
            CreatePratitiBar(pratiti);
        }

    }

        /// <summary>
    /// 製作一個CreateStageDataCard
    /// </summary>
    public void CreatePratitiBar(BagPratiti pratiti)
    {
        var g = Instantiate(pratitiBarPrefab, transform_PratitiBar);
        // g.transform.SetParent(transform);

        var l = g.GetComponent<PratitiBar>();
        l.Initialize(pratiti); 
    }



    
}