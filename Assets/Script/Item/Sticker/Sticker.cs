

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Sticker : IItem{
    public StickerData _stickerData;
    public StickerType _stickerType;
    public BagPratiti _stickerOwner;
    public bool _isWear;
    public int count;

    public Sticker(StickerType type, StickerData data){ 
        _stickerType = type;
        _stickerData = data;
        _isWear = false;

    }

    public void SetPratiti(BagPratiti pratiti){
        _stickerOwner = pratiti;
        _isWear = true;
    }

}

// + data : StickerData
// + stickerType : StickerType
// + StickerOwner： BagPratiti
// + IsWear: bool