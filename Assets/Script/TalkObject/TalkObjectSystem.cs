using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fungus;

public class TalkObjectSystem : IGameSystem
{
    private List<GameObject> talkObjects; // 所有可對話物件
    private List<GameObject> mapPratitis; // 所有場景中的帕拉緹緹

//    private TalkPratiti enemyPratiti; // 正在戰鬥的帕拉緹緹

    private Flowchart mainFlowchart; // 遊戲共同事件
    public bool IsTalking; 

    public TalkObjectSystem(GameMediator mediator):base(mediator)
	{
		Initialize();
    }

    public override void Initialize(){
        talkObjects = new List<GameObject>();
        mapPratitis = new List<GameObject>();
    
        mainFlowchart = GameObject.Find("MainFlowchart").GetComponent<Flowchart>();
    }

    public void AddTalkObject(GameObject talkObject){
        talkObjects.Add(talkObject);
    }

    public void AddMapPratiti(GameObject pratiti){
        mapPratitis.Add(pratiti);
    }

    // ========= 狀態機相關 =============

    // 玩家點擊按鈕時，判斷並播放故事
    public void PlayStoryWhenKeyPress(){
        foreach(GameObject talk in talkObjects)  {
            if(talk != null){
                talk.GetComponent<TalkObject>().PlayStoryWhenKeyPress();
            }
		}
    }

    // 更新對話物件的行為
    public void UpdateObjectBehavior(){
        foreach(GameObject talk in talkObjects)  {
            if(talk != null){
                talk.GetComponent<TalkObject>().UpdateObjectBehavior();
            }
		}
    }

    // 初始化所有的TalkState
    public void InitializeTalkState(){
        foreach(GameObject talk in talkObjects)  {
            if(talk != null){
                talk.GetComponent<TalkObject>().TalkState = 0;
            }
		}
    }

    // 更新場景物件的狀態
    public void UpdateTalkState(){
        foreach(GameObject talk in talkObjects)  {
            if(talk != null){
                talk.GetComponent<TalkObject>().UpdateTalkState();
            }
		}
    }

    // ========= MainFlowchart相關 =============

    public Flowchart GetMainFlowchart(){
        return mainFlowchart;
    }

    // 存檔相關


    // 與帕拉緹緹戰鬥相關
    /*
    public void SetEnemyPratiti(TalkPratiti talkPratit){
        this.enemyPratiti = talkPratit;
    }*/

    /*
    public void EndBattleChangeMapPratitiState(){
        enemyPratiti.SetTalkState(TalkState.A);
        UpdateTalkState();
    }


    // 重生帕拉緹緹
    public void ReBrithMapPratiti(){
        foreach (GameObject pratiti in mapPratitis)  {
            if(pratiti != null){
                pratiti.GetComponent<TalkPratiti>().SetTalkState(TalkState.Null);
            }
		}
        UpdateTalkState();
    }*/

    public bool GetIstalking(){
        return IsTalking;
    }

}