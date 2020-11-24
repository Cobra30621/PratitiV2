using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using Fungus;

public class StoryTrigger : MonoBehaviour
{
    // 取得 Flowchart
    public Flowchart talkForchart;
    // Block的名稱
    public string CallTalkBlock;
    
    internal bool inView; // 是否在視野內
    // 是否要顯示任務中
    public bool haveMission;

    // 顯示任務icon
    public GameObject missionIcon;
    internal GameObject micon;
    private bool hasmicon;

    

    private void Awake()
    {
        inView = false;

    }

    private void Update()
    {

        // 按下Z鍵且可以開始對話啟動（inView = true）
        if (Input.GetKeyDown(KeyCode.Z) && inView)
        {
            // 當onCollosionEnter不是空白、不在對話中才能觸發
            if (!string.IsNullOrEmpty(CallTalkBlock) && !UData.IsTalking)
            {
                CloseMissionIcon();
                PlayBlock(CallTalkBlock);
            }
        }

    }

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



    private void OnTriggerEnter2D(Collider2D other)
    {
        
        // 當玩家靠夠近時，可以開始對話（inView = true;）
        if (other.gameObject.CompareTag("Player"))
        {
            if (haveMission)
            {
                ShowMissionIcon();
            }
            else
            {
                CloseMissionIcon();
            }
            inView = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        // 當玩家靠太遠時，無法開始對話（inView = true;）
        if (other.gameObject.CompareTag("Player"))
        {

            CloseMissionIcon();
            inView = false;
        }
    }

    /*
    public void ShowMissionIcon()
    {
        missionIcon.SetActive(true);
    }

    public void CloseMissionIcon()
    {
        missionIcon.SetActive(false);
    }

    */
    //=======
    // 開關任務icon
    //=======

    public void ShowMissionIcon()
    {
        if (!hasmicon)
        {
            micon = Instantiate(missionIcon);
            micon.transform.position = transform.position + new Vector3(0, 3f, 0);
                     
            hasmicon = true;
        }
    }

    public void CloseMissionIcon()
    {
        Destroy(micon);
        hasmicon = false;
    }
}
