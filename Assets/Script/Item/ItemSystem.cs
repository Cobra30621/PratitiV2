
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;

public class ItemSystem : IGameSystem
{
    public List<Sticker> _stickers = new List<Sticker>();
    public Sticker _selectedSticker;
    private IAssetFactory _factory;
    // 事件相關
    public StickerSelectedUI _stickerSelectedUI;

    public ItemSystem(GameMediator mediator):base(mediator)
	{
		Initialize();
    }

    public override void Initialize(){
        _factory =  MainFactory.GetAssetFactory();
        CreateAllSticker();
        Debug.Log("初始化itemSystem");
        AddSticker(StickerType.Attr , 3);
        AddSticker(StickerType.Def , 3);
        AddSticker(StickerType.Hp , 3);
        // CreateSticker(StickerType.Attr);

        // 事件相關
        // 超級爛的方法
        _stickerSelectedUI = GameMediator.Instance.GetStickerSelectedUI();
        _stickerSelectedUI._stickerEquiped += OnStickerEquiped;
        // _startPratiti = _bagPratitis[0];
    }

    public void CreateAllSticker(){
        foreach (StickerType type in Enum.GetValues( typeof( StickerType ) )){
            CreateSticker(type);
        }
        
    }

    public void CreateSticker(StickerType type){
        StickerData data = _factory.LoadStickerData(type);
        Sticker sticker = new Sticker(type, data);
        _stickers.Add(sticker); 
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


    

    // 當貼紙被裝備在帕拉提提上面
    public void OnStickerEquiped( object sender, StickerEquipedEventArgs e){
        AddSticker(e._sticker._stickerType, -1);
        Debug.Log("貼紙"+ e._sticker._stickerType +"少1");
    }
}