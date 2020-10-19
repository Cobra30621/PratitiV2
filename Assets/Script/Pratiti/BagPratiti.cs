
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
    public string _name;
    
    // stickers
    // magicSkill

    public BagPratiti(PratitiType type){
        _pratitiType = type;
        _pratitiAttr = new PratitiAttr(type);

        switch(type){
            case PratitiType.Feather:
                _name = "羽毛";
                break;
            case PratitiType.Pig:
                _name = "山豬";
                break;
            default:
                Debug.LogError("找不到名字");
                break;
        }
        
    }


}