using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;// Required when using Event data.

public class ShowStickerDesInfo : IShowDesInfo
{
    [SerializeField] private LootUnitBar LootUnitBar;

    public override void OnPointerEnter(PointerEventData eventData)
    {
        lab_des.text = LootUnitBar.GetInfo();
        base.OnPointerEnter(eventData);
    }

}