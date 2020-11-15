
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PratitiBar : MonoBehaviour
{
    [SerializeField] private Text lab_name;
    
    private bool whetherSelect; // 是否被選擇

    [SerializeField] private Image img_icon;
    private Sprite sprite_icon;

    // 被選擇
    [SerializeField] private GameObject img_selected;
    [SerializeField] private GameObject img_light;

    // 首發與否
    [SerializeField] private Image img_start;
    [SerializeField] private Text lab_start;

    [SerializeField] private Button butt_select;
    [SerializeField] private Button butt_start;

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
        WhetherSelected();
        WhetherStart();
    }

    public void WhetherSelected(){
        int id = GameMediator.Instance.GetSelectedPratitiID();
        if (id == _bagPratiti._ID)
        {
            whetherSelect = true;
            img_selected.SetActive(true);
            img_light.SetActive(true);
        }
        else
        {
            whetherSelect = false;
            img_selected.SetActive(false);
            img_light.SetActive(false);
        }
    }

    public void WhetherStart(){
        int id = GameMediator.Instance.GetStartPratitiID();
        if (id == _bagPratiti._ID)
        {
            lab_start.text = "首發";
            img_start.color = new Color32(113, 199, 76, 255); // 綠色
        }
        else
        {
            lab_start.text = "待命";
            img_start.color = new Color32(167, 65, 46, 255);  //紅色
        }
    }


    public void OnBarClick(){
        GameMediator.Instance.SetSelectedPratiti(_bagPratiti._ID);
    }

    public void OnStartClick(){
        GameMediator.Instance.SetStartPratiti(_bagPratiti._ID);
    }


}