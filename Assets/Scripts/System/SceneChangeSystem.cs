
using UnityEngine;
using System.Collections.Generic;
using UnityEngine.SceneManagement;


// 遊戲事件系統
public class SceneChangeSystem : IGameSystem
{
    public SceneChangeSystem(SystemController SControl) : base(SControl)
    {
        Debug.Log("初始化SceneChangeSystem");
    }

    // 釋放
    public override void Release()
    {
    
    }

    // 初始化場景
    #region InitialMap
    // 初始化場景
    GameObject[] Maps;
    // 讀入場景時的map
    GameObject Map;
    public void InitialMap()
    {
        // 讀取玩家地圖數據
        Debug.Log("現在的地圖" + UData.Map);
        Debug.Log("現在的小地圖" + UData.smallMap);

        // 尋找小關卡物件
        Map = GameObject.Find(UData.smallMap);

        // 搜尋所有小關卡
        Maps = GameObject.FindGameObjectsWithTag("Map");

        // 將所有小關卡關閉
        foreach (GameObject map in Maps)
        {
            // Debug.Log("關掉場景:" + map );
            map.SetActive(false);
        }

        // 將現在的小關卡打開
        Debug.Log("打開場景:" + Map );
        if (Map != null)
        {
            Map.SetActive(true);
        }

        // 將Player回到原位
        GameObject player = GameObject.FindWithTag("Player");
        player.transform.position = UData.PlayerPos;
        Debug.Log("玩家位置" + player.transform.position);
        //Debug.LogWarning("之後修正bug:");
    }
    #endregion


    // 儲存小地圖的資料
    public void SetSmallMap(string smallMapName)
    {
        UData.smallMap = smallMapName;
    }

    // 轉場時儲存GameData, smallMap, PlayerPosition
    public void SaveMapData()
    {
        UData.Map = SceneManager.GetActiveScene().name;
        UData.PlayerPos = GameObject.FindWithTag("Player").transform.position;
    }

}