
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

// 貼紙被裝備的事件
public class StickerEquipedEventArgs : System.EventArgs
{
   public Sticker _sticker;
   public StickerEquipedEventArgs(Sticker sticker){
       _sticker = sticker;
   }
}

public class StickerSelectedUI: MonoBehaviour
{
    private static StickerSelectedUI instance;
    // public delegate void OnPratitiEquipStickerEvent(Sticker sticker);
    // public  event OnPratitiEquipStickerEvent _onPratitiEquipStickerEvent;   //定義事件    
    public event System.EventHandler<StickerEquipedEventArgs> _stickerEquiped;

    public ItemSystem _itemSystem;
    public List<StickerSelectedBar> _selectedBars;
    public List<Sticker> _stickers;
    public List<GameObject> _barGameObjects;
  
    [SerializeField] GameObject stickerSelectedBarPrefab;
    [SerializeField] Transform transform_SelectedBar;

    /// <summary>
    /// Awake is called when the script instance is being loaded.
    /// </summary>
    void Start()
    {
        instance = this;
        Initialize();
    }

    public void Initialize(){
        _itemSystem = GameMediator.Instance.GetItemSystem();
        Open();
        CreateAllPratitiBars();
    }   

    // 開啟UI方式

    public static void Show(){
        instance.Open();
    }
    public void Open(){
        gameObject.SetActive(true);
        _stickers = _itemSystem._stickers;
        // CreateAllPratitiBars();
    }

    public void Hide(){
        gameObject.SetActive(false);
        RemoveAllPratitiBars();
    }



    public void RemoveAllPratitiBars(){
        Debug.Log("刪除所有PratitiBars");
    }

    public void CreateAllPratitiBars(){
        // RemoveAllPratitiBarObject();
        foreach(Sticker sticker in _stickers){
            CreatePratitiBar(sticker);
        }

    }

    /// <summary>
    /// 製作一個PratitiBar
    /// </summary>
    public void CreatePratitiBar(Sticker sticker)
    {
        var g = Instantiate(stickerSelectedBarPrefab, transform_SelectedBar);
        _barGameObjects.Add(g);

        var l = g.GetComponent<StickerSelectedBar>();
        l.Initialize(sticker, this); 
        _selectedBars.Add(l);
    }

    // 當貼紙被裝上
    public void SelectedSticker(Sticker sticker){
        // if(_onPratitiEquipStickerEvent != null)
        //     _onPratitiEquipStickerEvent(sticker);
        if(_stickerEquiped != null)
        {
            _stickerEquiped(this, new StickerEquipedEventArgs(sticker));
            Hide();
        }
            
    }
}