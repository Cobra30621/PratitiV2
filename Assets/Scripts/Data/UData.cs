using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 戰鬥結果
public enum BattleResult
{
    win,
    lose,
    catches
}

// 用在戰鬥系統上，玩家與敵人的帕拉提提數值
public static class UData 
{

    // 地圖轉場用資料
    [Header("玩家所在地圖")]
    // 將所有Trigger顯示管理器中
    public static string Map;
    public static string smallMap;
    [Header("玩家位置")]
    public static Vector3 PlayerPos;

    // 戰鬥結束變數
    public static bool BattleEnd;
    // 戰鬥結果
    public static BattleResult battleResult;

    // 正在對話
    public static bool IsTalking;

}
