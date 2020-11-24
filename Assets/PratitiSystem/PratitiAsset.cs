using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PratitiAsset : MonoBehaviour
{
    public Sprite feather;
    public Sprite pig;
    public Sprite goat;
    public Sprite infant;
    public Sprite snail;
    public Sprite fly;

    public Sprite GetSprite(Pratiti pratiti)
    {
        switch (pratiti.pratitiBrand.pratitiName)
        {
            case PratitiBrand.PratitiName.羽毛:
                return feather;

            case PratitiBrand.PratitiName.山豬:
                return pig;

            case PratitiBrand.PratitiName.山羊:
                return goat;

            case PratitiBrand.PratitiName.幼仔:
                return infant;

            case PratitiBrand.PratitiName.蝸牛:
                return snail;

            case PratitiBrand.PratitiName.雲朵:
                return fly;

            default:
                Debug.Log("sprite not found");
                return null;
        }

    }
}
