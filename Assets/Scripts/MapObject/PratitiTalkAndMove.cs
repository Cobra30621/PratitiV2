using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using Fungus;
using InventorySystem;
public class PratitiTalkAndMove : MonoBehaviour
{
    // 取得 Flowchart
    private Flowchart talkForchart;
    // Block的名稱
    internal bool inView; // 是否在視野內
    public Vector3 pos1;
    public Vector3 pos2;
    public Vector2 waitTime;
    // 元件
    internal Navigation2D nav;

    [Header("Pratiti Setting")]
    public Pratiti thisPratiti = new Pratiti();
    public bool isRandom;
    public int baseStatistic;
    public PratitiBrand.PratitiName pratitiName;
    [Range(0, 100)]
    public int sexDetermination;
    [Range(0, 100)]
    public int characteristic;
    [Range(0, 100)]
    public int IV;
    [Range(0, 100)]
    public int level;
       
    [Header("掉落甜點類型")]
    public Item getItem;

    // 是否要顯示任務中
    public bool haveMission;

    // 顯示任務icon
    public GameObject missionIcon;
    internal GameObject micon;
    private bool hasmicon;

    private void Awake()
    {
        nav = GetComponent<Navigation2D>();
        pos1 += transform.position;
        pos2 += transform.position;
        inView = false;
    }

    private void PratitiIntialize()
    {
        PratitiController pC = new PratitiController();
        Debug.Log(PratitiBrand.GetPratitiBrand(pratitiName));
        if (isRandom)
            pC.PratitiInitialize(baseStatistic, PratitiBrand.GetPratitiBrand(pratitiName), sexDetermination, characteristic, IV, level);
        else
            pC.RandomData(PratitiBrand.GetPratitiBrand(pratitiName), level);
        thisPratiti = pC.toControl;
        pC.toControl.winItem = getItem;
    }

    void Start()
    {
        PratitiIntialize();
        StartCoroutine(Wander());
        talkForchart = GameObject.Find("FungusMap").GetComponent<Flowchart>();
    }


    private void Update()
    {
        EnterBattle();
    }

    // 進入戰鬥
    public void EnterBattle()
    {
        // 按下Z鍵且可以開始對話啟動（inView = true）
        if (Input.GetKeyDown(KeyCode.Z) && inView)
        {
            // 當onCollosionEnter不是空白、不在對話中才能觸發
            if (!UData.IsTalking)
            {
                print(13241);
                SystemController.Instance.SetBattleData(thisPratiti, getItem);
                PlayBlock("StartBattle");
            }
        }
    }


    // 遊蕩
    IEnumerator Wander()
    {
        // 隨機決定往左往右走
        int direction = Random.Range(1, 3);
        bool point1 = true;
        switch (direction)
        {
            case 1:
                point1 = true;
                nav.MoveTo(pos1);
                break;
            case 2:
                point1 = false;
                nav.MoveTo(pos2);
                break;
        }


        while (true)
        {
            yield return new WaitForSeconds(Time.deltaTime);
            // 移動到點1，改移到點2
            if (!nav.moving)
            {
                yield return new WaitForSeconds(Random.Range(waitTime.x, waitTime.y));
                point1 = !point1;
                nav.MoveTo(point1 ? pos1 : pos2);
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
    //=======
    // 觸發對話
    //=======

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

    //=======
    // Debug
    //=======
    // 用來在畫面畫出pos1,pos2
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position + pos1, 0.2f);
        Gizmos.DrawWireSphere(transform.position + pos2, 0.2f);
    }

}
