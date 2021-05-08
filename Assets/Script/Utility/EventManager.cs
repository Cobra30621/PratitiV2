using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager : MonoBehaviour
{
    public static EventManager instance;


    public delegate void StoryEvent(Fungus.Block storyblock);

    public StoryEvent onStoryStart;
    public StoryEvent onStoryEnd;
    public StoryEvent act;

    [SerializeField]  private bool IsTeaching; //  正在教學中
    public GameObject storyFlowchartGO; // 教學的flowchart
    public PratitiStickerUI pratitiStickerUI; // UI介面


    //--------------------------------------------------

    // Start is called before the first frame update
    void Awake() 
    {
        instance = this;
    }

    void Start()
    {
        // 註冊故事事件
        Fungus.BlockSignals.OnBlockStart += onFungusPlay;
        Fungus.BlockSignals.OnBlockEnd += onFungusEnd;
        
    }

    public void SetIsTeaching(bool bo){
        IsTeaching = bo;
    }

    //---------------------------------------------------------

    /// <summary>
    /// 當事件被觸發時
    /// </summary>
    public void onStateChange(string stateName)
    {
        if(!IsTeaching){return;} // 不是在教學的話退出

        Debug.Log("<color=purple>進入狀態："+stateName+"</color>");
        StoryEvent action = null;
        switch(stateName)
        {
            default:
                Debug.LogWarning("沒有此STATE！ "+stateName);
                return;
            case "打開UI":
                CloseTumb(TumbType.OpenUI); 
                PlayStory("合成攻擊貼紙");
                action = (b)=>
                {
                    onStoryEnd-=action;
                    ShowTumb(TumbType.CompostieSticker); 
                };
                onStoryEnd += action;  // 當教學文本播完後，執行action的功能
                break;
            case "合成攻擊貼紙":
                CloseTumb(TumbType.CompostieSticker); 

                 PlayStory("去裝備貼紙");
                action = (b)=>
                {
                    onStoryEnd-=action;
                    ShowTumb(TumbType.PratitiUI); 
                };
                onStoryEnd += action;  // 當教學文本播完後，執行action的功能
                break;
            case "你給我裝備貼紙":
                CloseTumb(TumbType.PratitiUI); 

                PlayStory("你給我裝備貼紙");
                action = (b)=>
                {
                    onStoryEnd-=action;
                    ShowTumb(TumbType.StickerBox); 
                };
                onStoryEnd += action;  // 當教學文本播完後，執行action的功能
                break;
            case "點三貼紙按鈕":
                CloseTumb(TumbType.StickerBox); 
                ShowTumb(TumbType.EquipSticker); 

                break;
            case "裝備攻擊貼紙":
                CloseTumb(TumbType.EquipSticker); 
                PlayStory("裝備完貼紙拉");
                action = (b)=>
                {
                    onStoryEnd-=action;
                    ShowTumb(TumbType.CloseUI); 
                };
                onStoryEnd += action;  // 當教學文本播完後，執行action的功能

                break;
            case "關閉包包":
                CloseTumb(TumbType.CloseUI); 
                PlayStory("結束包包教學");
                
                break;
        }
    }

    // 產生手指
    private void ShowTumb(TumbType type){
        pratitiStickerUI.ShowTumb(type);
        Debug.Log($"開啟教學箭頭{type}");
    }

    // 關閉手指
    private void CloseTumb(TumbType type){
        pratitiStickerUI.CloseTumb(type);
        Debug.Log($"關閉教學箭頭{type}");
    }

    
    //---------------------------------------------------------
    // 劇情播放相關

    protected void onFungusPlay(Fungus.Block b)
    {
        onStoryStart?.Invoke(b);
    }

    protected void onFungusEnd(Fungus.Block b)
    {
        onStoryEnd?.Invoke(b);
    }


    /// <summary>
    /// 撥放故事
    /// </summary>
    public void PlayStory(string storyID)
    {
        // chose use block
        GameObject useBlock = storyFlowchartGO;

        // play
        Debug.Log($"<color=purple>播放劇情{storyID}</color>");
        UseFungus.PlayBlock(useBlock, storyID);
    }


}
