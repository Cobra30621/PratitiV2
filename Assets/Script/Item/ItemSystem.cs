
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;

public class ItemSystem : IGameSystem
{
    public List<Sticker> _stickers = new List<Sticker>();
    public List<StickerChip> _stickerChips = new List<StickerChip>();

    public Dictionary<StickerType, Sticker> _dicStickers = new Dictionary<StickerType, Sticker>();
    public Dictionary<StickerType, StickerChip> _dicStickerChips = new Dictionary<StickerType, StickerChip>();
    public Dictionary<StoneType, Stone> _dicStones = new Dictionary<StoneType, Stone>();

    public Sticker _selectedSticker;
    
    private IAssetFactory _factory;
    // 事件相關
    public StickerSelectedUI _stickerSelectedUI;

    public int CompositeCount = 5;

    public ItemSystem(GameMediator mediator):base(mediator)
	{
		Initialize();
    }

    public override void Initialize(){
        _factory =  MainFactory.GetAssetFactory();
        CreateAllSticker();
        Debug.Log("初始化itemSystem");
        CreateAllStickerChip();
        CreateAllStone();
        GetAllStone(); // 測試用 

        OnSceneLoad();
    }

    public override void OnSceneLoad(){
        // 事件相關
        // 超級爛的方法
        _stickerSelectedUI = GameMediator.Instance.GetStickerSelectedUI();
        _stickerSelectedUI._stickerEquiped += OnStickerEquiped;
    }

    // CompositeStickerChip

    public void CompositeStickerChip(StickerType type){
        AddStickerChip(type, - CompositeCount );
        AddSticker(type, 1);
    }

    // Item
    // public void AddItem(Itemtype itemtype)


    // Sticker
    public void CreateAllSticker(){
        foreach (StickerType type in Enum.GetValues( typeof( StickerType ) )){
            if (type != StickerType.Null)
                CreateSticker(type);
        }
    }

    public void CreateSticker(StickerType type){
        StickerData data = _factory.LoadStickerData(type);
        Sticker sticker = new Sticker(type, data);
        _stickers.Add(sticker); 
        _dicStickers.Add(type, sticker);
        Debug.Log($"創造一個{data._name}");
    }

    public void AddSticker(StickerType type, int addNum){
        foreach(Sticker sticker in _stickers){
            if(sticker._stickerType == type)
            {
                sticker.count += addNum;
                Debug.Log($"{type}貼紙增加{addNum}");
            }
        }
    }

    // StoneChip
    public void CreateAllStone(){
        foreach (StoneType type in Enum.GetValues( typeof( StoneType ) )){
            CreateStone(type);
        }
    }

    public void CreateStone(StoneType type){
        StoneData data = _factory.LoadStoneData(type);
        Stone stone = new Stone(type, data ); 
        _dicStones.Add(type, stone );
        Debug.Log($"創造一個{data._name}俗頭");
    }

    public void GetAllStone(){
        Debug.Log("測試：取的所有俗頭");
        foreach (Stone stone in _dicStones.Values)
        {
            stone._hadGet = true;
        }
    }

    public void GetStone(StoneType type){
        Stone stone = _dicStones[type];
        stone._hadGet = true;
    }

    

    // StickerChip
    public void CreateAllStickerChip(){
        foreach (StickerType type in Enum.GetValues( typeof( StickerType ) )){
            if (type != StickerType.Null)
                CreateStickerChip(type);
        }
    }

    public void CreateStickerChip(StickerType type){
        StickerData data = _factory.LoadStickerData(type);
        StickerChip stickerChip = new StickerChip(type, data._chipIcon );
        _stickerChips.Add(stickerChip); 
        _dicStickerChips.Add(type, stickerChip );
        Debug.Log($"創造一個{data._name}Chip");
    }

    public void AddStickerChip(StickerType type, int addNum){
       foreach(StickerChip stickerChip in _stickerChips){
            if(stickerChip._stickerType == type)
            {
                stickerChip.count += addNum;
                Debug.Log($"{type}貼紙增加{addNum}");
            }
        }
    }

    // 當貼紙被裝備在帕拉提提上面
    public void OnStickerEquiped( object sender, StickerEquipedEventArgs e){
        AddSticker(e._sticker._stickerType, -1);
        Debug.Log("貼紙"+ e._sticker._stickerType +"少1");
    }
}