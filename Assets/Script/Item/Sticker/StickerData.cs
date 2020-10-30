
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum StickerType{
    Attr, Def, Hp
}


[CreateAssetMenu(fileName= "StickerData", menuName= "Pratiti/Craete StickerData")]
public class StickerData : ScriptableObject{
    public StickerType _stickerType;
    public string _name;
    public string _descript;
    public Sprite _icon;
    public float _addAttr;


}

// + stickerType :StickerType 
// + name : string
// + descript: name
// + icon : sprite
// + addAttr: int