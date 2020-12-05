
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum PratitiType{
    Feather, Pig, Cloud
}

public class PratitiAttr
{
    public PratitiType pratitiType;
    // public MagicSkill magicSkill;

    public int rawHp;
    public int rawAttr;
    public int rawDef;
    public int rawSpeed;

    public int plusHp;
    public int plusAttr;
    public int plusDef;
    public int plusSpeed;

    // 戰鬥時拿的資料
    public int Hp{
        get{return rawHp + plusHp;}
    }

    public int Atk{
        get{return rawAttr + plusAttr;}
    }

    public int Def{
        get{return rawDef + plusDef;}
    }

    public int Speed{
        get{return rawSpeed + plusSpeed;}
    }

    public PratitiAttr(PratitiType type, PratitiData data){
        pratitiType = type;

        rawHp = data._rawHp;
        rawAttr = data._rawAttr;
        rawDef = data._rawDef;
        rawSpeed = data._rawSpeed;

        plusAttr = 0;
        plusHp = 0;
        plusDef = 0;
        plusSpeed = 0;
    }

    public void SetPlusData(StickerType[] types){
        InitPlusAttr();
        IAssetFactory _factory = MainFactory.GetAssetFactory();
        foreach(StickerType type in types){
            StickerData data = _factory.LoadStickerData(type);
            AddPlusData(data);
        }
    }

    public void SetPlusData(Sticker[] stickers){
        InitPlusAttr();
        foreach(Sticker sticker in stickers){
            if(sticker != null)
            {
                AddPlusData(sticker._stickerData);
            } 
        }
    }

    public void InitPlusAttr(){
        plusAttr = 0;
        plusHp = 0;
        plusDef = 0;
        plusSpeed = 0;
    }

    public void AddPlusData(StickerData data){
        StickerType type = data._stickerType;
        float plus = data._addAttr;
        switch(type){
            case StickerType.Attr:
                plusAttr += Mathf.FloorToInt( rawAttr * plus);
                break;
			case StickerType.Hp:
                plusHp += Mathf.FloorToInt( rawHp * plus);
                break;
			case StickerType.Def:
                plusDef += Mathf.FloorToInt( rawDef * plus);
                break;
            case StickerType.Null:
                break;
            default:
                Debug.LogError("裝備時找不到StickerType");
                break;
        }
    }

    public override string ToString(){
        return $"{pratitiType}類Pratiti的Akt:{Atk},Hp:{Hp}, Def{Def}";
    }

}

