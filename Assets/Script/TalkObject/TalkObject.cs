using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fungus;

public class TalkObject : MonoBehaviour
{
    public bool InputZToPlayBlock ; // 啟動對話是否為玩家按Z鍵
    private bool inPlayerView; // 是否在視野內

    public TalkIconType talkIconType = TalkIconType.talk; // 對話時顯示的Icon類型
    
    private TalkIcon talkIcon; // 對話時顯示的Icon
    private Flowchart m_flowchart;
    
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
        m_flowchart = transform.GetComponentInChildren<Flowchart>(); // 取得自己的對話物件
    }

    public virtual void Initialize(){
        talkIcon = new TalkIcon(talkIconType, this.gameObject);  // 初始化按鈕
        GameMediator.Instance.AddTalkObject(this.gameObject);  // 加入TalkObjectSystem
    }

    void OnTriggerEnter (Collider other){
		if(other.tag == "Player"){
            inPlayerView = true;

            if ( InputZToPlayBlock ){
                talkIcon.ShowIcon();
            }
            else{
                PlayStoryEvent();
            }
		}
	}

    void OnTriggerExit (Collider other){
        if(other.tag == "Player"){
            inPlayerView = false;

            if ( InputZToPlayBlock ){
                talkIcon.CloseIcon();
            }
		}
    }

    // 玩家點擊按鈕時，判斷並播放故事
    public void PlayStoryWhenKeyPress(){
        if (InputZToPlayBlock && inPlayerView && !IsTalking)
        {
            PlayStoryEvent();
        }
    }

    // 更新對話物件的事件
    public void PlayStoryEvent(){
        UpdateTalkState(); // 判斷並更新對話物件的狀態
        UseFungus.PlayBlock( m_flowchart.gameObject , "JudgeTalkEvent" );
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
}
