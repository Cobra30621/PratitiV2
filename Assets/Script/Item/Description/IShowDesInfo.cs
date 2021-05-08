
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;// Required when using Event data.

public class IShowDesInfo : MonoBehaviour , IPointerEnterHandler , IPointerExitHandler
{
    [SerializeField] protected GameObject panel_des;
    [SerializeField] protected Text lab_des;


    public virtual void OnPointerEnter(PointerEventData eventData)
    {
        panel_des.SetActive(true);
        Debug.Log("The cursor entered the selectable UI element.");
    }

    public virtual void OnPointerExit(PointerEventData eventData)
    {
        panel_des.SetActive(false);
        Debug.Log("The cursor exited the selectable UI element.");
    }
}