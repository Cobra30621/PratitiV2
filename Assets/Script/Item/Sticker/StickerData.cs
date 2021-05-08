
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
[System.Serializable]
public enum StickerType{
    Attr, Def, Hp,  Null
} // 超垃圾寫法，把道具寫在這邊

[System.Serializable]
[CreateAssetMenu(fileName= "StickerData", menuName= "Pratiti/Craete StickerData")]
public class StickerData : ScriptableObject{
    public StickerType _stickerType;
    public string _name;
    public string _descript;
    public string _chipDes;
    public Sprite _icon;
    public Sprite _chipIcon;
    public float _addAttr;


}

// + stickerType :StickerType 
// + name : string
// + descript: name
// + icon : sprite
// + addAttr: int