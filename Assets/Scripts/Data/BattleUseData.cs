using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using InventorySystem;

public class BattleUseData : MonoBehaviour
{
    private PratitiData m_PratitiData;
    public Pratiti EnemyPratiti;
    public Pratiti PlayerPratiti;
    public Item GetItem;

    // Start is called before the first frame update
    void Start()
    {
        m_PratitiData = GameObject.Find("GameData").GetComponent<PratitiData>();
    }

    public void SetPlayerData()
    {
       // Debug.Log("ppd_count: " + m_PratitiData.pratitiData.Count);
        PlayerPratiti = m_PratitiData.pratitiData[0];
    }

    //Annfol Add 5/24
    public Pratiti GetPlayerPratiti()
    {
        return m_PratitiData.pratitiData[0];
    }

}
