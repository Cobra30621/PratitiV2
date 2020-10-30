
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
        RefreshInfo();
    }   

    public void RefreshInfo(){
        // img_icon.sprite = sprite_icon;
        img_icon.sprite = _bagPratiti._pratitiData._icon;
        lab_name.text = _bagPratiti._name;
        lab_first.text = "選擇";
    }


    public void OnBarClick(){
        GameMediator.Instance.SetSelectedPratiti(_bagPratiti._ID);
    }

    public void OnStartClick(){
        GameMediator.Instance.SetStartPratiti(_bagPratiti._ID);
    }


}