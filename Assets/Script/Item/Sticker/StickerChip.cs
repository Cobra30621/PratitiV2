

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StickerChip : IItem{
    public StickerType _stickerType;
    public int count;
    public Sprite _icon;

    public StickerChip(StickerType type, Sprite icon){
        _stickerType = type;
        _icon = icon;
    }

    

}