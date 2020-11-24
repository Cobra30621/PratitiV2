using UnityEngine;
using InventorySystem;

[System.Serializable]
public class Pratiti
{   //Pratiti基本資料設定
    public static int counter = 0;
    public int index;
    public string pratitiName;
    public PratitiBrand pratitiBrand;
    public int sexDetermination , characteristic , IV;
    public int baseStatistic;
    public int pressure;
    public float maxHp,maxAtk,maxDef,maxMAtk,maxMDef,maxAgi;
    public bool sex;
    public float hp;
    public float atk;
    public float def;
    public float mAtk;
    public float mDef;
    public float agi;

    public int generalPoints = 0;
    public int winTimer = 0;

    public Item winItem;

    public Sprite GetPratitiSprite()
    {
        switch (pratitiBrand.pratitiName)
        {
            case PratitiBrand.PratitiName.山羊: return Resources.Load<Sprite>("PratitiAssets/山羊");
            case PratitiBrand.PratitiName.山豬: return Resources.Load<Sprite>("PratitiAssets/山豬");
            case PratitiBrand.PratitiName.幼仔: return Resources.Load<Sprite>("PratitiAssets/幼仔");
            case PratitiBrand.PratitiName.羽毛: return Resources.Load<Sprite>("PratitiAssets/羽毛");
            case PratitiBrand.PratitiName.蝸牛: return Resources.Load<Sprite>("PratitiAssets/蝸牛");
            case PratitiBrand.PratitiName.雲朵: return Resources.Load<Sprite>("PratitiAssets/雲朵");
            default:
                Debug.Log("error");
                return null;
        }
    }

    public int GetLevel()
    {
        int level = 0;
        level = (int)((hp + atk + def + mAtk + mDef + agi) / (maxHp + maxAtk + maxDef + maxMAtk + maxMDef + maxAgi) * 100);
        return level;
    }

    public static Sprite GetPratitiSprite(Pratiti pratiti)
    {
        switch (pratiti.pratitiBrand.pratitiName)
        {
            case PratitiBrand.PratitiName.山羊: return Resources.Load<Sprite>("PratitiAssets/山羊");
            case PratitiBrand.PratitiName.山豬: return Resources.Load<Sprite>("PratitiAssets/山豬");
            case PratitiBrand.PratitiName.幼仔: return Resources.Load<Sprite>("PratitiAssets/幼仔");
            case PratitiBrand.PratitiName.羽毛: return Resources.Load<Sprite>("PratitiAssets/羽毛");
            case PratitiBrand.PratitiName.蝸牛: return Resources.Load<Sprite>("PratitiAssets/蝸牛");
            case PratitiBrand.PratitiName.雲朵: return Resources.Load<Sprite>("PratitiAssets/雲朵");
            default:
                Debug.Log("error");
                return null;
        }
    }
}