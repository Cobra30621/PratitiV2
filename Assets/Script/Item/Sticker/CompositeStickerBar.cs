using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CompositeStickerBar : MonoBehaviour
{
    [SerializeField] private Text lab_title;
    [SerializeField] private Text lab_chipNum;
    [SerializeField] private Text lab_stickerNum;

    [SerializeField] private Image img_chip;
    [SerializeField] private Image img_sticker;
    [SerializeField] private Button butt_composite;

    private BagUI _bagUI;
    private StickerData _stickerData;
    private StickerType _type;

    private Sticker _sticker;
    private StickerChip _stickerChip;

    public void Initialize(Sticker sticker, StickerChip chip){
        _sticker = sticker; 
        _stickerChip = chip;
        _type = _sticker._stickerType;
        
        _stickerData = _sticker._stickerData;
        RefreshInfo();
    }   

    public void SetBagUI(BagUI bagUI){
        _bagUI = bagUI;
    }

    public void RefreshInfo(){
        img_sticker.sprite = _stickerData._icon;
        img_chip.sprite = _stickerData._chipIcon;
        
        lab_chipNum.text = $"({_stickerChip.count}/5)";
        lab_stickerNum.text = $"{_sticker.count}";
        lab_title.text = $"{_stickerData._name}";

        UpdateButtonState();
    }

    public void OnSelected(){
        _bagUI.CompositeStickerChip(_type);
        RefreshInfo();
    }

    // 道具數量>0才能點選
    public void UpdateButtonState(){
        if(_stickerChip.count >= 5){
            butt_composite.interactable = true;   
        }
        else{
            butt_composite.interactable = false;
        }  
    }

    public string GetStickerInfo(){
        return _stickerData._descript;
    }

    public string GetStickerChipInfo(){
        return _stickerData._chipDes;
    }
}
