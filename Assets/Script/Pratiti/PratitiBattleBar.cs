
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PratitiBattleBar : PratitiBar
{
    public int playerID;
    private PratitiBattleSystem pratitiBattleSystem;

    public void Initialize(BagPratiti pratiti, int playerID){
        this.playerID = playerID;
        pratitiBattleSystem = GameMediator.Instance.GetPratitiBattleSystem();
        Initialize(pratiti);

    }   

    public override void WhetherSelected(){
        int id;
        if(playerID == 1)
            id = pratitiBattleSystem.selectedPratiti_1P._ID;
        else
            id = pratitiBattleSystem.selectedPratiti_2P._ID;

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

    public override void WhetherStart(){
        int id;
        if(playerID == 1)
            id = pratitiBattleSystem.startPratiti_1P._ID;
        else
            id = pratitiBattleSystem.startPratiti_2P._ID;

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
        if(playerID == 1)
            pratitiBattleSystem.SetSelectedPratiti_1P(_bagPratiti._ID);
        else
            pratitiBattleSystem.SetSelectedPratiti_2P(_bagPratiti._ID);
        
    }

    public void OnStartClick(){
        if(playerID == 1)
            pratitiBattleSystem.SetStartPratiti_1P(_bagPratiti._ID);
        else
            pratitiBattleSystem.SetStartPratiti_2P(_bagPratiti._ID);
        
    }


}