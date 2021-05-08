using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fungus;


public class TalkObjectSystem : IGameSystem
{
    private List<GameObject> talkObjects; // 所有可對話物件
    private List<GameObject> mapPratitis; // 所有場景中的帕拉緹緹

    private Flowchart mainFlowchart; // 遊戲共同事件
    public bool IsTalking;  // 是否正在對話

    //------------------對話時，其他東西執行------------------------

    public delegate void StoryEvent(Fungus.Block storyblock);

    public static StoryEvent onStoryStart;
    public static StoryEvent onStoryEnd;

    public TalkObjectSystem(GameMediator mediator):base(mediator)
	{
		Initialize();
    }

    public override void Initialize(){
        talkObjects = new List<GameObject>();
        mapPratitis = new List<GameObject>();
    
        mainFlowchart = GameObject.Find("MainFlowchart").GetComponent<Flowchart>();

        // 註冊故事事件
        Fungus.BlockSignals.OnBlockStart += onFungusPlay;
        Fungus.BlockSignals.OnBlockEnd += onFungusEnd;
    }

    //---------------------------------------------------------
    // 劇情播放相關

    private void onFungusPlay(Fungus.Block b)
    {
        // Debug.Log("<color=purple>Fungus Play: "+b.BlockName+"</color>");
        // IsTalking = true;
        onStoryStart?.Invoke(b);
    }

    private void onFungusEnd(Fungus.Block b)
    {
        // Debug.Log("<color=purple>Fungus End: "+b.BlockName+"</color>");
        // IsTalking = false;
        onStoryEnd?.Invoke(b);
        // UpdateObjectBehavior(); // 更新對話物件狀態
    }

    // =========== 增加對話物件
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
    public void UpdateAllObjectBehavior(){
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
                // talk.GetComponent<TalkObject>().TalkState = 0;
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

    public bool GetIstalking(){
        return IsTalking;
    }

    public void SetIsTalking(bool bo){
        IsTalking = bo;
    }

    public string EndBattleStoryName; // 回到戰鬥時，撥放的劇情名稱
    public bool playEndBattleStory;

    // 回戰鬥時撥放劇情
    public void SetEndBattlerStory(string name){
        EndBattleStoryName = name;
        SetWhetherPlayEndBattleStory(true);
        
    }

    public void SetWhetherPlayEndBattleStory(bool bo){
         playEndBattleStory = bo;
    }

    public void PlayEndBattleStory(){
        if( !playEndBattleStory){return;} // 戰鬥結束後，是否要撥劇情

        string blockName = EndBattleStoryName;
        BattleOutcome battleOutcome = GameMediator.Instance. GetBattleOutcome();
        if(battleOutcome == BattleOutcome.Win)
            blockName += "_Win";
        if(battleOutcome == BattleOutcome.Lose)
            blockName += "_Lose";

        if(mainFlowchart == null)
            mainFlowchart = GameObject.Find("MainFlowchart").GetComponent<Flowchart>();

        UseFungus.PlayBlock(mainFlowchart.gameObject, blockName);
        SetWhetherPlayEndBattleStory(false);
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

    

}