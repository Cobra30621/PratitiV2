using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using Fungus;

public class ProtalChangeScene : MonoBehaviour
{
    // 取得 Flowchart
    private Flowchart FungusMap;
    // Block的名稱
    public string GoMap;

    public GameObject missionIcon;
    internal GameObject micon;
    private bool hasmicon;
    internal bool inView; // 是否在視野內

    private void Awake()
    {
        inView = false;
        FungusMap = GameObject.Find("FungusMap").GetComponent<Flowchart>();
        //Debug.Log("FungusManagerSystem產生FungusBattle:" + FungusBattle);
        if (FungusMap == null)
        {
            Debug.Log("FungusManagerSystem找不到FungusMap");
        }
    }


    private void Update()
    {

        // 按下Z鍵且可以開始對話啟動（inView = true）
        if (Input.GetKeyDown(KeyCode.Z) && inView)
        {
            SystemController.Instance.PlayBlock(FungusMap, GoMap);
            
        }

    }


    // 靠夠近，進行傳送
    private void OnTriggerEnter2D(Collider2D co)
    {
        if(co.tag != "Player") return;
        ShowMissionIcon();
        inView = true;
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




}
