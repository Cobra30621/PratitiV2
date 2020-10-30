
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Fungus;

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
 
    }

    public void SetSticker(Sticker sticker, int id){
        if(id < _stickers.Length)
        {
            _stickers[id] = sticker;
            sticker.SetPratiti(this);
        }
        else{
            Debug.LogError($"StickerID超出範圍。ID = {id}");
        }
        
    }


}