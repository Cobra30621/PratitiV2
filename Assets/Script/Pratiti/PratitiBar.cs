
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PratitiBar : MonoBehaviour
{
    [SerializeField] protected Text lab_name;
    
    protected bool whetherSelect; // 是否被選擇

    [SerializeField] protected Image img_icon;
    protected Sprite sprite_icon;

    // 被選擇
    [SerializeField] protected GameObject img_selected;
    [SerializeField] protected GameObject img_light;

    // 首發與否
    [SerializeField] protected Image img_start;
    [SerializeField] protected Text lab_start;

    [SerializeField] protected Button butt_select;
    [SerializeField] protected Button butt_start;

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

    public virtual void WhetherSelected(){
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

    public virtual void WhetherStart(){
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