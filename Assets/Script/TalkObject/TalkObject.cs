using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fungus;

public class TalkObject : MonoBehaviour
{
    public bool InputZToPlayBlock ; // 啟動對話是否為玩家按Z鍵
    protected bool inPlayerView; // 是否在視野內

    public TalkIconType talkIconType = TalkIconType.talk; // 對話時顯示的Icon類型
    
    private TalkIcon talkIcon; // 對話時顯示的Icon
    public Flowchart m_flowchart;
    
    
    // 物件的狀態機
    public int TalkState  
    {
        get{return m_flowchart.GetIntegerVariable("TalkState");}
        set{m_flowchart.SetIntegerVariable("TalkState", value);}
    }
    // 是否在對話

    public bool IsTalking
    {
        get{return GameMediator.Instance.WhetherIsTalking();}
    }

    void Start(){
        Initialize();
        // m_flowchart = transform.GetComponentInChildren<Flowchart>(); // 取得自己的對話物件
    }

    public virtual void Initialize(){
        talkIcon = new TalkIcon(talkIconType, this.gameObject);  // 初始化按鈕
        GameMediator.Instance.AddTalkObject(this.gameObject);  // 加入TalkObjectSystem
    }

    void OnTriggerEnter2D (Collider2D other){
        Debug.Log("SomeThingEnter");
		if(other.tag == "Player"){
            Debug.Log("PlayerEnter");
            inPlayerView = true;

            if ( InputZToPlayBlock ){
                talkIcon.ShowIcon();
            }
            else{
                PlayStoryEvent();
            }
		}
	}

    void OnTriggerExit2D (Collider2D other){
        if(other.tag == "Player"){
            inPlayerView = false;

            if ( InputZToPlayBlock ){
                talkIcon.CloseIcon();
            }
		}
    }

    // 玩家點擊按鈕時，判斷並播放故事
    public virtual void PlayStoryWhenKeyPress(){
        if (InputZToPlayBlock && inPlayerView && !IsTalking)
        {
            PlayStoryEvent();
        }
    }

    // 更新對話物件的事件
    public void PlayStoryEvent(){
        UpdateTalkState(); // 判斷並更新對話物件的狀態
        UseFungus.PlayBlock( m_flowchart.gameObject , "JudgeTalkEvent" );

        // 播完對話後更新狀態
        TalkObjectSystem.StoryEvent action = null;
        action = (b)=>
        { 
            TalkObjectSystem.onStoryEnd-=action; 
            GameMediator.Instance.UpdateAllObjectBehavior();   
        };
        TalkObjectSystem.onStoryEnd += action;  
    }

    // 更新對話物件的行為
    public void UpdateObjectBehavior(){
        UpdateTalkState(); // 判斷並更新對話物件的狀態
        UseFungus.PlayBlock( m_flowchart.gameObject , "JudgeObjectBehavior" );
    }

    // 判斷並更新對話物件的狀態
    public void UpdateTalkState(){
        UseFungus.PlayBlock( m_flowchart.gameObject , "JudgeState" );
    }

    // 提供給Fungus用的方法
    public void SetInputZToPlayBlock(bool bo){
        InputZToPlayBlock = bo;
    }

}
