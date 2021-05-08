

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[System.Serializable]
public class StickerSaveData{
    public int count;
    public StickerType stickerType;

    public StickerSaveData(Sticker sticker){
        count = sticker.count;
        stickerType = sticker._stickerType;
    }

    public StickerSaveData(StickerChip stickerChip){
        count = stickerChip.count;
        stickerType = stickerChip._stickerType;
    }
}


[System.Serializable]
public class Sticker : IItem{
    public StickerData _stickerData;
    public StickerType _stickerType;
    // public BagPratiti _stickerOwner;
    // public bool _isWear;
    public int count;

    public Sticker(StickerType type, StickerData data){ 
        _stickerType = type;
        _stickerData = data;
        // _isWear = false;

    }


    public Sticker(StickerType type){
        _stickerType = type;
        IAssetFactory _factory =  MainFactory.GetAssetFactory();

        _stickerData = _factory.LoadStickerData(type);
        // _isWear = false;
    }

    // public void SetPratiti(BagPratiti pratiti){
    //     _stickerOwner = pratiti;
    //     _isWear = true;
    // }

}

// + data : StickerData
// + stickerType : StickerType
// + StickerOwner： BagPratiti
// + IsWear: bool