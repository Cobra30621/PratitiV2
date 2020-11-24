using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fungus;

public class Wall : MonoBehaviour
{
    public GameObject wall;
    // 取得 Flowchart
    public Flowchart talkForchart;
    // Block的名稱
    public string CallTalkBlock;

    // 玩家觸發對話
    #region OnTriggerEnter2D
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // 當玩家靠夠近時，可以開始對話
        if (collision.gameObject.CompareTag("Player"))
        {
            if (!string.IsNullOrEmpty(CallTalkBlock) && !UData.IsTalking)
            {
                PlayBlock(CallTalkBlock);
            }
        }
    }

    #endregion
    // 執行Block
    #region PlayBlock
    void PlayBlock(string targetBlockName)
    {
        // 尋找Block
        Block targetBlock = talkForchart.FindBlock(targetBlockName);
        // 當targetBlock有物件時執行Block
        if (targetBlock != null)
        {
            talkForchart.ExecuteBlock(targetBlock);
        }
        else
        {
            Debug.LogError("找不到在" + talkForchart.name + "裡的" + targetBlockName + "Block");
        }
    }
    #endregion

}