

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LootUnitBar : MonoBehaviour
{
    [SerializeField] private Text lab_count;
    [SerializeField] private Image img_icon;
    [SerializeField] private Image img_bg;

    public Loot _loot;
    private IAssetFactory factory;

    public void Initialize(Loot loot){
        _loot = loot;
        factory = MainFactory.GetAssetFactory();  
        RefreshInfo();
        
    }

    public void RefreshInfo(){
        lab_count.text = $"{_loot._dropCount}";
        SetIcon();
    }

    private void SetIcon(){
        ItemType itemType = _loot._itemType;
        switch (itemType){
            case ItemType.Sticker:
                StickerType stickerType = _loot._stickerType;
                img_icon.sprite = factory.LoadStickerData(stickerType)._icon;
                img_bg.color = new Color32(255, 174, 0, 144);
                break;
            case ItemType.StickerChip:
                StickerType chipType = _loot._stickerType;
                img_icon.sprite = factory.LoadStickerData(chipType)._chipIcon;
                img_bg.color = new Color32(91, 0, 255, 144); // 紫色
                break;
            default:
                Debug.Log($"找不到item類型{itemType}");
                break;
        }
    }
    public string GetInfo(){
        ItemType itemType = _loot._itemType;
        switch (itemType){
            case ItemType.Sticker:
                StickerType stickerType = _loot._stickerType;
                StickerData data = factory.LoadStickerData(stickerType);
                return $"{data._name}\n{data._descript}";
            case ItemType.StickerChip:
                StickerType chipType = _loot._stickerType;
                StickerData data2 = factory.LoadStickerData(chipType);
                return $"{data2._name}碎片\n{data2._chipDes}";
            default:
                Debug.Log($"找不到item類型{itemType}");
                return "";
        }
    }

    public void AddItem(){
        ItemType itemType = _loot._itemType;
        switch (itemType){
            case ItemType.Sticker:
                StickerType stickerType = _loot._stickerType;
                GameMediator.Instance.AddSticker(stickerType, _loot._dropCount);
                Debug.Log($"掉落{_loot._dropCount}個{_loot._stickerType}{_loot._itemType} ");
                break;
            case ItemType.StickerChip:
                StickerType chipType = _loot._stickerType;
                GameMediator.Instance.AddStickerChip(chipType, _loot._dropCount);
                Debug.Log($"掉落{_loot._dropCount}個{_loot._stickerType}{_loot._itemType} ");
                break;
            default:
                Debug.Log($"找不到item類型{itemType}");
                break;
        }

        
    }

}