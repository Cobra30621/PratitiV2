
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;
using System.Linq;

public class PratitiSystem : IGameSystem
{
    public List<BagPratiti> _bagPratitis = new List<BagPratiti>();
    public BagPratiti _startPratiti; // 出戰的帕拉提提
    public BagPratiti _selectedPratiti; // 被選中的帕拉提提
    public MapPratiti _enemyPratiti; // 敵方的帕拉提提
    
    // 裝備相關
    public int _SelectedStickerID;

    private IAssetFactory _factory;

    // 事件相關
    public StickerSelectedUI _stickerSelectedUI;

    public PratitiSystem(GameMediator mediator):base(mediator)
	{
		Initialize();
    }

    public override void Initialize(){
        SetAssetFactory();
        // CreateBagPratiti(PratitiType.Pig);
        // CreateBagPratiti(PratitiType.Feather);
        CreateAllPratiti();
        // 超級爛的方法
        _stickerSelectedUI = GameMediator.Instance.GetStickerSelectedUI();
        _stickerSelectedUI._stickerEquiped += OnStickerEquiped;

        _startPratiti = _bagPratitis[0];
        _selectedPratiti = _bagPratitis[0];
    }

    public void CreateAllPratiti(){
        foreach (PratitiType type in Enum.GetValues( typeof( PratitiType ) )){
            CreateBagPratiti(type);
        }
    }

    public void CreateBagPratiti(PratitiType type){
        SetAssetFactory();
        PratitiData data = _factory.LoadPratitiData(type);
        BagPratiti pratiti = new BagPratiti(type, data);
        pratiti._ID = _bagPratitis.Count; // 設定帕拉緹緹ID
        _bagPratitis.Add(pratiti);
        Debug.Log("獲得編號" + pratiti._ID + $"{type}類帕拉提提");

        PratitiUI.Refresh(); // 翻新帕拉提提介面

    }

    public void SetAssetFactory(){
        if(_factory == null)
            _factory = MainFactory.GetAssetFactory();
    }

    public void SetStartPratiti(int ID){
        if(ID > _bagPratitis.Count){
            Debug.LogError("設定初始帕拉提提超過包包上限");
            return;
        }
        _startPratiti = _bagPratitis[ID];
        Debug.Log($"首發帕拉提提編號{ID}");

        // 刷新UI介面
        PratitiUI.Refresh();
    }

    public int GetStartPratitiID(){
        return _startPratiti._ID;
    }

    public void SetSelectedPratiti(int ID){
        if(ID > _bagPratitis.Count){
            Debug.LogError("設定初始帕拉提提超過包包上限");
            return;
        }
        _selectedPratiti = _bagPratitis[ID];

        // 刷新UI介面
        PratitiUI.Refresh();
    }

    public int GetSelectedPratitiID(){
        return _selectedPratiti._ID;
    }

    // public void OnStickerEquiped( Sticker sticker){
    //     _selectedPratiti.SetSticker(sticker, _SelectedStickerID);
    // }
    // 當貼紙被裝備在帕拉提提上面
    public void OnStickerEquiped( object sender, StickerEquipedEventArgs e){
        _selectedPratiti.SetSticker(e._sticker, _SelectedStickerID);
    }
}