using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[System.Serializable]
public class Loot {
    [Tooltip("道具的種類")]
    public ItemType _itemType;
    public StickerType _stickerType;

    [Tooltip("道具掉落相對機率")]
    [Range(0, 100)]
    public int _probability;

    // [Header("道具掉落機率")]
    public float f_probability;

    [HideInInspector]
    public int _dropCount;

}