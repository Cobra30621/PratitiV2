using UnityEngine;

[CreateAssetMenu(fileName = "pratitiBattleData", menuName = "CreatePratitiBattleData")]
public class PratitiBattleData : ScriptableObject
{

    [Header("帕拉緹緹資料")]
    // 將所有Trigger顯示管理器中
    [Header("帕拉緹緹類型")]
    public string type = "";

    [Header("帕拉緹緹資料")]
    public float attackStartHitRecoveryTime;
    public float attackCompleteHitRecoveryTime;
    public float attackMissHitRecoveryTime;
    public float abilityStartHitRecoveryTime;
    public float abilityEndHitRecoveryTime;
    public GameObject GameObject;

}