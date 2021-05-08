using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;// Required when using Event data.

public class ShowCompositeStickerDesInfo : IShowDesInfo
{
    [SerializeField] private CompositeStickerBar compositeStickerBar;

    public override void OnPointerEnter(PointerEventData eventData)
    {
        lab_des.text = compositeStickerBar.GetStickerInfo();
        base.OnPointerEnter(eventData);
        Debug.Log("The cursor entered the selectable UI element.");
    }

}