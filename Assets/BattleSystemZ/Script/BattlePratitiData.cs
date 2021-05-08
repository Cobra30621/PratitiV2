using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattlePratitiData : MonoBehaviour
{
    static public float player_hp;
    static public float player_def;
    static public float player_energy;
    static public float player_atk = 1f;
    static public float player_maxHp = 500;
    static public float player_maxDef = 100;
    static public float player_maxEnergy = 100;
    static public float enemy_hp;
    static public float enemy_def;
    static public float enemy_energy;
    static public float enemy_atk = 1f;
    static public float enemy_maxHp = 500;
    static public float enemy_maxDef = 200;
    static public float enemy_maxEnergy = 100;
    static public bool isDataLoadComplete = false;

    void Awake()
    {
        
    }

    public static void InitData(){
        player_hp = player_maxHp;
        player_def = player_maxDef;
        player_energy = 0;

        enemy_hp = enemy_maxHp;
        enemy_def = enemy_maxDef;
        enemy_energy = 0;
        isDataLoadComplete = true;
    }

    public static void SetPlayerData(PratitiAttr attr){
        player_maxHp = attr.Hp;
        player_maxDef = attr.Def;
        player_atk = (float)attr.Atk / 100f;
    }

    public static void SetEnemyData(PratitiAttr attr){
        enemy_maxHp = attr.Hp;
        enemy_maxDef = attr.Def;
        enemy_atk = (float)attr.Atk / 100f;
    }

    private void FixedUpdate()
    {
        CheckOverMaxValue(ref player_hp, player_maxHp);
        CheckOverMaxValue(ref player_energy, player_maxEnergy);
        CheckOverMaxValue(ref enemy_hp, enemy_maxHp);
        CheckOverMaxValue(ref enemy_energy, enemy_maxEnergy);
        

    }
    void CheckOverMaxValue(ref float current_value, float max_value)
    {
        if (current_value > max_value)
        {
            current_value -= (current_value - max_value);
        }
    }
}
