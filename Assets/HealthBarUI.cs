using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarUI : MonoBehaviour
{
    [SerializeField] private GameObject playerHpBar;
    [SerializeField] private Text lab_playerHp;
    [SerializeField] private GameObject enemyHpBar;
    [SerializeField] private Text lab_enemyHp;

    // Update is called once per frame
    void Update()
    {
        RefreshInfo();
    }

    private void RefreshInfo(){
        float playerHpPre = BattlePratitiData.player_hp / BattlePratitiData.player_maxHp;
        playerHpBar.transform.localScale = new Vector3 (playerHpPre, 1, 0);
        lab_playerHp.text = $"{BattlePratitiData.player_hp} / {BattlePratitiData.player_maxHp}";

        float enemyHpPre = BattlePratitiData.enemy_hp / BattlePratitiData.enemy_maxHp;
        enemyHpBar.transform.localScale = new Vector3 (enemyHpPre, 1, 0);
        lab_enemyHp.text = $"{BattlePratitiData.enemy_hp} / {BattlePratitiData.enemy_maxHp}";
    }
}
