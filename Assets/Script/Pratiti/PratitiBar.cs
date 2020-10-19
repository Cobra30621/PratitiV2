
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PratitiBar : MonoBehaviour
{
    [SerializeField] private Text lab_name;
    [SerializeField] private Text lab_first;
    private bool whetherSelect; // 是否被選擇

    [SerializeField] private Image img_icon;
    private Sprite sprite_icon;

    [SerializeField] private Button butt_select;
    [SerializeField] private Button butt_first;

    public BagPratiti _bagPratiti;
    public IAssetFactory factory;

    public void Initialize(BagPratiti pratiti){
        _bagPratiti = pratiti;
        
        factory = MainFactory.GetAssetFactory();
        SetIcon();
        RefreshInfo();
    }   

    public void RefreshInfo(){
        img_icon.sprite = sprite_icon;
        lab_name.text = _bagPratiti._name;
        lab_first.text = "選擇";
    }

    private void SetIcon(){
        PratitiType type = _bagPratiti._pratitiType;
        sprite_icon = factory.LoadPratitiSprite(type);
    }

    public void OnBarClick(){
        GameMediator.Instance.SetStartPratiti(_bagPratiti._ID);
    }

    public void OnSelectClick(){

    }


}