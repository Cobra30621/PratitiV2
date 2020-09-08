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
	MapSystem m_mapSystem = null;

	private GameMediator(){}

	public void Initinal(){
		Debug.Log("初始化系統");
		m_talkObjectSystem = new TalkObjectSystem(this);
		m_mapSystem = new MapSystem(this);
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

	public ISceneState _sceneState; // 目前的場景物件
	public void SetSceneState(ISceneState sceneState){// 切換場景狀態
		_sceneState = sceneState;
	}

	public ISceneState GetSceneState(SceneState sceneState){
		if(_sceneState._sceneState != sceneState)
			Debug.Log($"你要的sceneState:{sceneState}與現在的{_sceneState._sceneState}不同"); 
		
		return _sceneState;
	}


	// ================================================
	// =================TalkObjectSystem===============
	// ================================================

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
    public void UpdateAllObjectBehavior(){
        m_talkObjectSystem.UpdateAllObjectBehavior();
    }

    // 初始化所有的TalkState
    public void InitializeTalkState(){
        m_talkObjectSystem.InitializeTalkState();
    }

    // 更新場景物件的狀態
    public void UpdateTalkState(){
        m_talkObjectSystem.UpdateTalkState();
    }

	// ================================================
	// =================MapSystem======================
	// ================================================

	// 進入戰鬥
	public void EnterBattle(){
		m_mapSystem.EnterBattle();
	}

	// 戰鬥結束回到場景
	public void BackToMap(){
		m_mapSystem.BackToMap();
	}


}