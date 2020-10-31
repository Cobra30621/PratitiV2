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
	PratitiSystem m_pratitiSystem = null;
	ItemSystem m_itemSystem = null;

	// 遊戲UI
	public StickerSelectedUI _stickerSelectedUI;

	private GameMediator(){}

	public void Initinal(){
		Debug.Log("初始化系統");
		m_talkObjectSystem = new TalkObjectSystem(this);
		m_mapSystem = new MapSystem(this);
		m_pratitiSystem = new PratitiSystem(this);
		m_itemSystem = new ItemSystem(this);

		_stickerSelectedUI = GameObject.Find("StickerSelectedPanel").GetComponent<StickerSelectedUI>();
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

	public void FadeIn(){
		m_mapSystem.FadeIn();
	}

	public void FadeOut(){
		m_mapSystem.FadeOut();
	}

	public void FadeInAndOut(){
		// m_mapSystem.FadeInAndOut();
	}

	public void ChangeScene(Vector3 vec, MapName map){
		m_mapSystem.ChangeScene(vec, map);
	}

	public void SetCamera(MapName map){
        m_mapSystem.SetCamera(map);
    }

	// ================================================
	// =================PratitiSystem======================
	// ================================================

	public List<BagPratiti> GetBagPratitis(){
		return m_pratitiSystem._bagPratitis;
	}

	public void SetSelectedPratiti(int ID){
		m_pratitiSystem.SetSelectedPratiti(ID);
	}

	public BagPratiti GetSelectedPratiti(){
		return m_pratitiSystem._selectedPratiti;
	}

	public PratitiSystem GetPratitiSystem(){
		return m_pratitiSystem;
	}

	public void SetStartPratiti(int ID){
		m_pratitiSystem.SetStartPratiti(ID);
	}

	public BagPratiti GetStartPratiti(){
		return m_pratitiSystem._startPratiti;
	}

	public void SetEnemyPratiti(MapPratiti pratiti){
		m_pratitiSystem._enemyPratiti = pratiti;
	}

	public void CreateBagPratiti(PratitiType type){
		m_pratitiSystem.CreateBagPratiti(type);
	}

	public void CreateAllPratitiTest(){
		m_pratitiSystem.CreateAllPratiti();
	}

	// 戰鬥系統用
	public PratitiAttr GetPlayerAttr(){
		if(m_pratitiSystem == null || m_pratitiSystem._startPratiti == null)
			return m_pratitiSystem._startPratiti._pratitiAttr;
		else{
			Debug.Log("Player的資料是空的");
			return null;
		}
	}

	public PratitiAttr GetEnemyAttr(){
		if(m_pratitiSystem == null || m_pratitiSystem._enemyPratiti == null)
			return m_pratitiSystem._enemyPratiti._pratitiAttr;
		else{
			Debug.Log("Enemy的資料是空的");
			return null;
		}
	}

	// ================================================
	// =================ItemSystem===============
	// ================================================
	public ItemSystem GetItemSystem(){
		return m_itemSystem;
	}

	// ================================================
	// =================StickerSelectedUI======================
	// ================================================

	public StickerSelectedUI GetStickerSelectedUI(){
		if(_stickerSelectedUI == null)
			_stickerSelectedUI = GameObject.Find("StickerSelectedPanel").GetComponent<StickerSelectedUI>();

		return _stickerSelectedUI;
	}

}