
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;

public class PratitiBattleSystem : IGameSystem
{
    private PratitiSystem pratitiSystem;
    public List<BagPratiti> _bagPratitis ;
    public BagPratiti selectedPratiti_1P; // 正在被選擇中的帕拉提提
    public BagPratiti startPratiti_1P; // 出戰的帕拉提提

    public BagPratiti selectedPratiti_2P; // 正在被選擇中的帕拉提提
    public BagPratiti startPratiti_2P; // 出戰的帕拉提提

    public bool whether2P; // 是否為2P戰鬥



    public PratitiBattleSystem(GameMediator mediator):base(mediator)
	{
		Initialize();
    }

    public override void Initialize(){

        pratitiSystem = GameMediator.Instance.GetPratitiSystem();
        GetPratitiData();

        selectedPratiti_1P = _bagPratitis[0];
        selectedPratiti_2P = _bagPratitis[0];

        startPratiti_1P = _bagPratitis[0];
        startPratiti_2P = _bagPratitis[0];
    }

    public void SetWhether2P(bool bo){
        whether2P = bo;
    }

    public void SetStartPratiti_1P(int ID){
        GetPratitiData();

        if(ID > _bagPratitis.Count){
            Debug.LogError("設定初始帕拉提提超過包包上限");
            return;
        }
        startPratiti_1P = _bagPratitis[ID];
        Debug.Log($"設定1P首發帕拉提提編號{ID}");

        // 刷新UI介面
        PratitiBattleUI.Refresh();
    }

    public void SetStartPratiti_2P(int ID){
        GetPratitiData();

        if(ID > _bagPratitis.Count){
            Debug.LogError("設定初始帕拉提提超過包包上限");
            return;
        }
        startPratiti_2P = _bagPratitis[ID];
        Debug.Log($"設定2P首發帕拉提提編號{ID}");

        // 刷新UI介面
        PratitiBattleUI.Refresh();
    }

    public void SetSelectedPratiti_1P(int ID){
        GetPratitiData();

        if(ID > _bagPratitis.Count){
            Debug.LogError("設定初始帕拉提提超過包包上限");
            return;
        }
        selectedPratiti_1P = _bagPratitis[ID];

        // 刷新UI介面
        PratitiBattleUI.Refresh();
    }

    public void SetSelectedPratiti_2P(int ID){
        GetPratitiData();

        if(ID > _bagPratitis.Count){
            Debug.LogError("設定初始帕拉提提超過包包上限");
            return;
        }
        selectedPratiti_2P = _bagPratitis[ID];

        // 刷新UI介面
        PratitiBattleUI.Refresh();
    }

    private void GetPratitiData(){
        _bagPratitis = pratitiSystem._bagPratitis;
    }


}
