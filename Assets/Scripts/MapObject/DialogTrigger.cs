using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using Fungus;

public class DialogTrigger : MonoBehaviour
{
    // 取得 Flowchart
    public Flowchart talkForchart;
    // Block的名稱
    public string onCollosionEnter;
    // Start is called before the first frame update

    // 執行Block
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


    private void OnTriggerEnter2D(UnityEngine.Collider2D other)
    {
        // 觸發對話
        if (other.gameObject.CompareTag("Player"))
        {
            // 當onCollosionEnter不是空白、不在對話中才能觸發
            if (!string.IsNullOrEmpty(onCollosionEnter) && !UData.IsTalking)
            {
                PlayBlock(onCollosionEnter);
                this.gameObject.SetActive(false);
                
            }
        }
    }
}
