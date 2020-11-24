using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowGameData : MonoBehaviour
{

    [Header("玩家所在地圖")]
    // 將所有Trigger顯示管理器中
    public string Map;
    public string smallMap;
    [Header("玩家位置")]
    public Vector3 PlayerPos;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        Map = UData.Map;
        smallMap = UData.smallMap;
        PlayerPos = UData.PlayerPos;
    }
}
