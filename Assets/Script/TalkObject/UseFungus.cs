
using UnityEngine;
using Fungus;

public class UseFungus
{

    // 取得 Flowchart
    private static Flowchart talkForchart;

    public static void PlayBlock(string targetBlockName)
    {

        // 尋找Flowchart
        talkForchart = GameObject.Find("StoryFlowchart").GetComponent<Flowchart>();

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


    /// <summary>
    /// rewritten by JCxYIS
    /// </summary>
    public static void PlayBlock(GameObject GOwithFlowChart, string targetBlock)
    {
        var fc = GOwithFlowChart.GetComponent<Flowchart>();

        // 尋找Block
        Block tb = fc.FindBlock(targetBlock);
        // 當targetBlock有物件時執行Block
        if (tb != null)
        {
            fc.ExecuteBlock(targetBlock);
        }
        else
        {
            Debug.LogError("找不到在" + GOwithFlowChart.name + "裡的 " + targetBlock + " Block");
        }
    }

}
