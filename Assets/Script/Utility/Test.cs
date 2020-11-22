using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    public ItemType itemType;

    public StickerType stickerType;

    [Range(0, 100)]
    public int _probability;

    [Header("道具掉落機率")]
    public float f_probability;

}
