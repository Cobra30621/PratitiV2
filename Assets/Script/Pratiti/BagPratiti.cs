
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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
            _stickers[id] = sticker;
            sticker.SetPratiti(this);
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