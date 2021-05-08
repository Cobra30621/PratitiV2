using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarControl : MonoBehaviour
{
    [SerializeField] private GameObject playerHpBar;
    [SerializeField] private Text lab_playerHp;
    [SerializeField] private GameObject playerDefBar;
    [SerializeField] private GameObject playerEnergyBar;
    [SerializeField] private GameObject enemyHpBar;
    [SerializeField] private Test lab_EnemyHP;
    [SerializeField] private GameObject enemyDefBar;
    [SerializeField] private GameObject enemyEnergyBar;
    [SerializeField] GameObject Tutorial;
    void Start()
    {
        
    }

    void Update()
    {
        if(BattlePratitiData.isDataLoadComplete)
        {
            UpdateBarUI();
        }
        if (Input.GetKeyDown(KeyCode.T))
        {
            if (Tutorial.gameObject.activeSelf)
            {
                Tutorial.SetActive(false);
            }
            else if (!Tutorial.gameObject.activeSelf)
            {
                Tutorial.SetActive(true);
            }
        }
    }
    public void UpdateBarUI()
    {
        playerHpBar.transform.localScale = new Vector3(8.5f * (BattlePratitiData.player_hp / BattlePratitiData.player_maxHp), this.transform.localScale.y);
        playerDefBar.transform.localScale = new Vector3(this.transform.localScale.x, 3.15f * (BattlePratitiData.player_def / BattlePratitiData.player_maxDef));
        playerEnergyBar.transform.localScale = new Vector3(6.676f * (BattlePratitiData .player_energy/ 100), this.transform.localScale.y);
        lab_playerHp.text = $"{BattlePratitiData.player_hp} / {BattlePratitiData.player_maxHp}";

        enemyHpBar.transform.localScale = new Vector3(8.5f * (BattlePratitiData.enemy_hp / BattlePratitiData.enemy_maxHp), this.transform.localScale.y);
        enemyDefBar.transform.localScale = new Vector3(this.transform.localScale.x, 3.15f * (BattlePratitiData.enemy_def / BattlePratitiData.enemy_maxDef));
        enemyEnergyBar.transform.localScale = new Vector3(6.676f * (BattlePratitiData.enemy_energy / 100), this.transform.localScale.y);

    }
    /*
    IEnumerator NumberChange(float Number, string DataType)
    {
        while (Number >= 1)
        {
            if (Time.timeScale != 0)
            {
                switch (DataType)
                {
                    case "hp":
                        if (Number < 10)
                        {
                            Pratiti.hp -= (Number * 0.5f);
                            Number -= (Number * 0.5f);
                        }
                        else
                        {
                            Pratiti.hp -= (Number * 0.1f);
                            Number -= (Number * 0.1f);
                        }
                        break;
                    case "def":
                        Pratiti.def -= (Number * 0.1f);
                        Number *= 0.9f;
                        break;
                    case "mDef":
                        Pratiti.mDef -= (Number * 0.1f);
                        Number *= 0.9f;
                        break;
                    case "gainEnergy":
                        Pratiti.energy += (Number * 0.12f);
                        Number *= 0.9f;
                        break;
                    case "costEnergy":
                        Pratiti.energy -= (Number * 0.11f);
                        Number *= 0.9f;
                        if (Pratiti.energy < 0)
                        {
                            Pratiti.energy = 0;
                        }
                        break;
                }
            }
            yield return null;
        }
        switch (DataType)
        {
            case "hp":
                Pratiti.hp = (int)Pratiti.hp;
                break;
            case "def":
                Pratiti.def = (int)Pratiti.def;
                break;
            case "mDef":
                Pratiti.mDef = (int)Pratiti.mDef;
                break;
            case "energy":
                Pratiti.energy = (int)Pratiti.energy;
                if (Pratiti.energy < 0)
                {
                    BattlePratitiData.player_energy = 0;
                }
                break;
        }
    }
    */
}
