using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;// Required when using Event data.

public class ShowHappyGrassInfo : IShowDesInfo
{
    public override void OnPointerEnter(PointerEventData eventData)
    {
        lab_des.text = "快樂草\n一種貨幣";
        base.OnPointerEnter(eventData);
    }

}