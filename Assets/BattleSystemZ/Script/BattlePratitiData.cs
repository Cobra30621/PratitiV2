using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattlePratitiData : MonoBehaviour
{
    static public float player_hp;
    static public float player_def;
    static public float player_energy;
    static public float player_atk;
    static public float player_maxHp;
    static public float player_maxDef;
    static public float player_maxEnergy;
    static public float enemy_hp;
    static public float enemy_def;
    static public float enemy_energy;
    static public float enemy_atk;
    static public float enemy_maxHp;
    static public float enemy_maxDef;
    static public float enemy_maxEnergy;
    static public bool isDataLoadComplete = false;

    void Start()
    {
        player_atk = 500;
        player_maxHp = 2500;
        player_maxDef = 500;
        player_maxEnergy = 100;
        player_hp = player_maxHp;
        player_def = player_maxDef;
        player_energy = 0;

        enemy_atk = 500;
        enemy_maxHp = 2500;
        enemy_maxDef = 500;
        enemy_maxEnergy = 100;
        enemy_hp = enemy_maxHp;
        enemy_def = enemy_maxDef;
        enemy_energy = 0;
        isDataLoadComplete = true;
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
