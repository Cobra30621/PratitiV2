using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BattleOutcome{
    Win, Lose
};

public class BattleDataManager : MonoBehaviour
{
    public bool isEditMode;
    public bool enemyUseAI;
    public PlayerController enemy;
    public BattleOutcome _outCome;

    public GameObject TeachingUI;
    
    void Awake()
    {
        if(isEditMode)
            return;
        Init();
    }

    /// <summary>
    /// Update is called every frame, if the MonoBehaviour is enabled.
    /// </summary>
    void Update()
    {
        WhetherEnd();
    }

    public void Init(){
        SetPratitiData();
        
    }

    private void SetPratitiData(){
        bool whether2P = GameMediator.Instance.GetWhether2P();
        
        PratitiAttr _playerAttr,_enemyAttr;

        if(whether2P){ // 2P對戰
            enemy.AI = false;

            _playerAttr = GameMediator.Instance.GetPlayerAttr_1P();
            _enemyAttr = GameMediator.Instance.GetPlayerAttr_2P();
            Debug.Log("Player1" + _playerAttr);
            Debug.Log("Player2" + _enemyAttr);
        }
        else{ // 劇情戰鬥
            enemy.AI = GameMediator.Instance.WhetherUseAI();
            
            _playerAttr = GameMediator.Instance.GetPlayerAttr();
            _enemyAttr = GameMediator.Instance.GetEnemyAttr();
            Debug.Log("Player" + _playerAttr);
            Debug.Log("Enemy" + _enemyAttr);
        }
        
        BattlePratitiData.SetPlayerData(_playerAttr);
        BattlePratitiData.SetEnemyData(_enemyAttr);
        BattlePratitiData.InitData();
        
        // 是否顯示教學UI
        bool whetherShowTeachingUI = GameMediator.Instance.WhetherShowTeachingUI();
        if(whetherShowTeachingUI)
            TeachingUI.SetActive(true);
        else
            TeachingUI.SetActive(false);

    }

    public void WhetherEnd(){
        if(isEditMode)
            return;
            
        if(BattlePratitiData.player_hp <=0)
        {
            _outCome = BattleOutcome.Lose;
            Debug.Log("贏了");
            EndBattle();
        }

        if(BattlePratitiData.enemy_hp <=0)
        {
            _outCome = BattleOutcome.Win;
            Debug.Log("輸了");
            EndBattle();
        }
        
    }

    public void EndBattle(){
        if(isEditMode)
            return;

        GameMediator.Instance.EndBattle(_outCome);
    }


}
