
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;// Required when using Event data.

public class StickerSelectedBar : MonoBehaviour , IPointerEnterHandler , IPointerExitHandler
{
    
    [SerializeField] private Text lab_title;
    [SerializeField] private Text lab_count;

    [SerializeField] private Image img_icon;
    [SerializeField] private Button butt_select;

    [SerializeField] private GameObject panel_des;
    [SerializeField] private Text lab_des;

    public IAssetFactory factory;
    public Sticker _sticker;
    private StickerSelectedUI _selectedUI;

    public void Initialize(Sticker sticker, StickerSelectedUI UI){
        _sticker = sticker;
        _selectedUI = UI;
        
        factory = MainFactory.GetAssetFactory();
        RefreshInfo();
    }   

    public void RefreshInfo(){
        img_icon.sprite = _sticker._stickerData._icon;
        lab_count.text = "" + _sticker.count;
        Debug.Log($"{_sticker._stickerData._name}有{_sticker.count}個");
        UpdateButtonState();
        
    }

    public void OnSelected(){
        _selectedUI.SelectedSticker(_sticker); 
    }

    // 道具數量>0才能點選
    public void UpdateButtonState(){
        if(_sticker.count > 0)
            butt_select.interactable = true;
        else
            butt_select.interactable = false;
    }
    
    public void OnPointerEnter(PointerEventData eventData)
    {
        lab_des.text = _sticker._stickerData._descript;
        panel_des.SetActive(true);
        Debug.Log("The cursor entered the selectable UI element.");
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        panel_des.SetActive(false);
        Debug.Log("The cursor exited the selectable UI element.");
    }
}