using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMediator
{
    //------------------------------------------------------------------------
	// Singleton模版
	private static GameMediator _instance;
	public static GameMediator Instance
	{
		get
		{
			if (_instance == null)			
				_instance = new GameMediator();
			return _instance;
		}
	}

	// 遊戲系統
	TalkObjectSystem m_talkObjectSystem = null;

	private GameMediator(){}

	public void Initinal(){
		m_talkObjectSystem = new TalkObjectSystem(this);
	}

	public void Update(){
		InputProcess(); // 輸入按鍵
		
	}

    public void Release(){
        
    }

	public void InputProcess(){
		if(Input.GetKeyDown(KeyCode.Z)){ 
			PlayStoryWhenKeyPress(); // 播放對話
		}
	}


	// ========= 場景切換相關

	public SceneState m_sceneState; // 目前的場景狀態
	public void SetSceneState(SceneState sceneState){// 切換場景狀態
		m_sceneState = sceneState;
	}

	public SceneState GetSceneState(){
		return m_sceneState;
	}

	// =================talkObjectSystem===============

	public bool WhetherIsTalking(){
		return m_talkObjectSystem.GetIstalking();
	}

		// 將場景的對話物件放入talkObjectSystem
	public void AddTalkObject(GameObject talkObject){
        m_talkObjectSystem.AddTalkObject(talkObject);
    }

	// ========= 狀態機相關 =============

    // 玩家點擊按鈕時，判斷並播放故事
    public void PlayStoryWhenKeyPress(){
        m_talkObjectSystem.PlayStoryWhenKeyPress();
    }

    // 更新對話物件的行為
    public void UpdateObjectBehavior(){
        m_talkObjectSystem.UpdateObjectBehavior();
    }

    // 初始化所有的TalkState
    public void InitializeTalkState(){
        m_talkObjectSystem.InitializeTalkState();
    }

    // 更新場景物件的狀態
    public void UpdateTalkState(){
        m_talkObjectSystem.UpdateTalkState();
    }


}