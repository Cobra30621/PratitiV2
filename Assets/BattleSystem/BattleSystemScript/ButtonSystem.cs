using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using NameBattleSystem;

public class ButtonSystem : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public UnityEvent Select;
    Navigation isNone = new Navigation();
    Navigation isntNone = new Navigation();
    void Start()
    {
        isNone.mode = Navigation.Mode.None;
        isntNone = this.GetComponent<Button>().navigation;
    }
    void Update()
    {
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        this.Select.Invoke();
        control.isSelect = true;
        this.GetComponent<Button>().navigation = isNone;
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        GameObject.Find("EventSystem").GetComponent<UnityEngine.EventSystems.EventSystem>().SetSelectedGameObject(null);
        control.isSelect = false;
        this.GetComponent<Button>().navigation = isntNone;
    }
}
