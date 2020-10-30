
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

    public PratitiAttr(PratitiType type){
        pratitiType = type;

        rawHp = 200;
        rawAttr = 120;
        rawDef = 3000;
        rawSpeed = 100;
    }

    public PratitiAttr(PratitiType type, PratitiData data){
        pratitiType = type;

        rawHp = data._rawHp;
        rawAttr = data._rawAttr;
        rawDef = data._rawDef;
        rawSpeed = data._rawSpeed;
    }
}

