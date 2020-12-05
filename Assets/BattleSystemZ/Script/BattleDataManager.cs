using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BattleOutcome{
    Win, Lose
};

public class BattleDataManager : MonoBehaviour
{
    public bool isEditMode;
    public BattleOutcome _outCome;
    void Awake()
    {
        if(isEditMode)
            return;
        Init();
    }

    public void Init(){
        PratitiAttr _playerAttr = GameMediator.Instance.GetPlayerAttr();
        PratitiAttr _enemyAttr = GameMediator.Instance.GetEnemyAttr();

        Debug.Log("Player" + _playerAttr);
        Debug.Log("Enemy" + _enemyAttr);
        BattlePratitiData.SetPlayerData(_playerAttr);
        BattlePratitiData.SetEnemyData(_enemyAttr);
    }

    public void EndBattle(){
        if(isEditMode)
            return;

        GameMediator.Instance.EndBattle(_outCome);
    }

    

}
