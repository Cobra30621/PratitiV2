
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[System.Serializable]
public class PratitiSaveData{
    public PratitiType pratitiType;
    public StickerType[] stickers = new StickerType[3];
    public PratitiSaveData(BagPratiti pratiti){
        pratitiType = pratiti._pratitiType;
        SetStickers(pratiti._stickers);
    }

    private void SetStickers(Sticker[] stickers){
        for (int i = 0; i < stickers.Length ; i++)
        {
            this.stickers[i] = stickers[i]._stickerType;
        }
    }

}


[System.Serializable]
public class BagPratiti
{
    public int _ID;
    public PratitiType _pratitiType;
    public PratitiAttr _pratitiAttr;
    public PratitiData _pratitiData;
    public string _name;

    public Sticker[] _stickers = new Sticker[3];
    
    // stickers
    // magicSkill

    public BagPratiti(PratitiType type , PratitiData data){
        _pratitiType = type;
        _pratitiData = data;
        _pratitiAttr = new PratitiAttr(type, data);

        _name  = data._name;
        InitialStickers();
    }

    // 初始化所有的貼紙
    public void InitialStickers(){
        for (int i = 0; i < 3 ; i++)
        {
            _stickers[i] = new Sticker(StickerType.Null);
        }
    }

    public void SetSticker(Sticker sticker, int id){
        if(id < _stickers.Length)
        {
            Debug.Log("sticker" + sticker);
            Debug.Log("stickerData" + sticker._stickerData);
            Debug.Log("stickerData" + sticker._stickerData._stickerType);
            _stickers[id] = sticker;
            // sticker.SetPratiti(this);
            SetStickerPlusData();
        }
        else{
            Debug.LogError($"StickerID超出範圍。ID = {id}");
        }
    }

    public void SetStickerPlusData(){
        _pratitiAttr.SetPlusData(_stickers);
    }


}