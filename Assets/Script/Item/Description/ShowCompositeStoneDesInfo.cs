using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;// Required when using Event data.

public class ShowCompositeStoneDesInfo : IShowDesInfo
{
    [SerializeField] private StoneBar stoneBar;

    public override void OnPointerEnter(PointerEventData eventData)
    {
        lab_des.text = stoneBar.GetStoneDes();
        base.OnPointerEnter(eventData);
    }

}