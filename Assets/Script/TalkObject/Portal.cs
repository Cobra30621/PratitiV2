using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fungus;
public class Portal : TalkObject
{
    // public Vector3 ChangePos;
    public GameObject ChangePos;
    public MapName myMap;
    public bool WhetherChangeScene;
    void Start(){
        Initialize();
        talkIconType = TalkIconType.door; // 對話時顯示的Icon類型
        InputZToPlayBlock = true; // 要玩家點擊Z見觸發
    }

    // 玩家點擊按鈕時，判斷並播放故事
    public override void PlayStoryWhenKeyPress(){
        if(WhetherChangeScene)
        {
            if (InputZToPlayBlock && inPlayerView && !IsTalking)
            {
                ChangeScene();
            }
        }
        else
        {
            PlayStoryEvent();
        }
        
    }

    public void ChangeScene(){
        Vector3 vector= ChangePos.transform.position;
        MapName changeMap = ChangePos.GetComponent<Portal>().GetProtalMapName();
        GameMediator.Instance.ChangeScene(vector, changeMap);
    }

    public MapName GetProtalMapName(){
        return myMap;
    }

    public void SetWhetherChangeScene(bool bo){
        WhetherChangeScene = bo;
    }

}