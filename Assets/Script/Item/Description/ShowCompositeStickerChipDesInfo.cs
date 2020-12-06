using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;// Required when using Event data.

public class ShowCompositeStickerChipDesInfo : IShowDesInfo
{
    [SerializeField] private CompositeStickerBar compositeStickerBar;

    public override void OnPointerEnter(PointerEventData eventData)
    {
        lab_des.text = compositeStickerBar.GetStickerChipInfo();
        base.OnPointerEnter(eventData);
    }

}