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
	SaveSystem m_saveSystem = null;
	PratitiBattleSystem m_pratitiBattleSystem = null;

	// 遊戲UI
	public StickerSelectedUI _stickerSelectedUI;
	public UIManager _UIManager;

	// 重要參數
	private bool usingUI;

	private GameMediator(){}

	public void Initinal(){
		Debug.Log("初始化系統");
		
		m_talkObjectSystem = new TalkObjectSystem(this);
		m_saveSystem = new SaveSystem(this);
		m_mapSystem = new MapSystem(this);
		m_itemSystem = new ItemSystem(this);
		m_pratitiSystem = new PratitiSystem(this);
		m_pratitiBattleSystem = new PratitiBattleSystem(this);

		

		_stickerSelectedUI = GameObject.Find("StickerSelectedPanel").GetComponent<StickerSelectedUI>();
		_UIManager = GameObject.Find("UICanvas").GetComponent<UIManager>();
		usingUI = false;
	}

	public void Update(){
		InputProcess(); // 輸入按鍵
		
	}

    public void Release(){
        
    }

	public void OnSceneLoad(){
		m_itemSystem.OnSceneLoad();
		m_pratitiSystem.OnSceneLoad();
	}

	public void InputProcess(){
		if(Input.GetKeyDown(KeyCode.Z) || Input.GetKeyDown(KeyCode.Return)){ 
			PlayStoryWhenKeyPress(); // 播放對話
		}
	}

	// ========= 特殊參數
	public bool WhetherUsingUI(){
		return usingUI;
	}

	public void SetUsingUI(bool bo){
		usingUI = bo;
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

	public void SetIsTalking(bool bo){
        m_talkObjectSystem.SetIsTalking(bo);
    }

	public void SetBlurPanel(bool bo){
		if(_UIManager == null)
			_UIManager = GameObject.Find("UICanvas").GetComponent<UIManager>();
		
		_UIManager.SetBlurPanel(bo);
	}

	 public void SetEndBattlerStory(string name){
        m_talkObjectSystem.SetEndBattlerStory(name);
        
    }

    public void SetWhetherPlayEndBattleStory(bool bo){
         m_talkObjectSystem.SetWhetherPlayEndBattleStory(bo);
    }

    public void PlayEndBattleStory(){
        m_talkObjectSystem.PlayEndBattleStory();
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
	public MapSystem GetMapSystem(){
		return m_mapSystem;
	}

	// 進入戰鬥
	public void EnterBattle(){
		m_mapSystem.EnterBattle();
	}

	//進入2P對戰
	public void Enter2PBattle(){
        m_mapSystem.Enter2PBattle();
    }

	// 戰鬥結束，判斷誰勝利
	public void EndBattle(BattleOutcome outcome){
		m_mapSystem.EndBattle(outcome);
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

	public GameObject GetPlayerObject(){
        return m_mapSystem.GetPlayerObject();
    }

	public BattleOutcome GetBattleOutcome(){
        return m_mapSystem.GetBattleOutcome();
    }

	public void SetWhetherLoadPlayerPos(bool bo){
        m_mapSystem.SetWhetherLoadPlayerPos(bo);
    }

	public void SetStartPos(Vector3 vector){
        m_mapSystem.SetStartPos(vector);
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
        Debug.Log(" Game mme GetPratitiInfo()");
		Debug.Log("m_pratitiSystem" +m_pratitiSystem);
		Debug.Log("m_pratitiSystem._selectedPratiti" +　m_pratitiSystem._selectedPratiti);

		return m_pratitiSystem._selectedPratiti;
	}

	public int GetSelectedPratitiID(){
        return m_pratitiSystem.GetSelectedPratitiID();
    }

	public PratitiSystem GetPratitiSystem(){
		return m_pratitiSystem;
	}

	public void SetStartPratiti(int ID){
		m_pratitiSystem.SetStartPratiti(ID);
	}

	public int GetStartPratitiID(){
        return m_pratitiSystem.GetStartPratitiID();
	}
	

	public BagPratiti GetStartPratiti(){
		return m_pratitiSystem._startPratiti;
	}

	public void SetEnemyPratiti(MapPratiti pratiti){
		m_pratitiSystem.SetEnemyPratiti(pratiti) ;
		m_mapSystem.SetEnemyPratiti(pratiti) ;
	}

	public void CreateBagPratiti(PratitiType type){
		m_pratitiSystem.CreateBagPratiti(type);
	}

	public void CreateAllPratitiTest(){
		m_pratitiSystem.CreateAllPratiti();
	}

	// 戰鬥系統用
	public PratitiAttr GetPlayerAttr(){
		if(m_pratitiSystem != null || m_pratitiSystem._startPratiti != null)
			return m_pratitiSystem._startPratiti._pratitiAttr;
		else{
			Debug.Log("Player的資料是空的");
			return null;
		}
	}

	public PratitiAttr GetEnemyAttr(){
		if(m_pratitiSystem != null || m_pratitiSystem._enemyPratiti != null)
			return m_pratitiSystem._enemyPratiti._pratitiAttr;
		else{
			Debug.Log("Enemy的資料是空的");
			return null;
		}
	}

	public bool WhetherUseAI(){
        return m_pratitiSystem.WhetherUseAI();
    }

	public bool WhetherShowTeachingUI(){
        return m_pratitiSystem.WhetherShowTeachingUI();
    }

	// 獎勵界面用
	public LootTable GetLootTable(){
		return m_pratitiSystem._lootTable;
	}

	// ================================================
	// =================PratitiBattlerSystem===============
	// ================================================
	public PratitiBattleSystem GetPratitiBattleSystem(){
		return m_pratitiBattleSystem;
	}

	public void SetWhether2P(bool bo){
        m_pratitiBattleSystem.SetWhether2P(bo);
    }

	

	public bool GetWhether2P(){
        return m_pratitiBattleSystem.whether2P;
    }


	// 戰鬥系統用
	public PratitiAttr GetPlayerAttr_1P(){
		if(m_pratitiBattleSystem != null || m_pratitiBattleSystem.startPratiti_1P != null)
			return m_pratitiBattleSystem.startPratiti_1P._pratitiAttr;
		else{
			Debug.Log("Player1P的資料是空的");
			return null;
		}
	}

	public PratitiAttr GetPlayerAttr_2P(){
		if(m_pratitiBattleSystem != null || m_pratitiBattleSystem.startPratiti_2P != null)
			return m_pratitiBattleSystem.startPratiti_2P._pratitiAttr;
		else{
			Debug.Log("Player2P的資料是空的");
			return null;
		}
	}

	// ================================================
	// =================ItemSystem===============
	// ================================================
	public ItemSystem GetItemSystem(){
		return m_itemSystem;
	}

	public void AddSticker(StickerType type, int addNum){
		m_itemSystem.AddSticker(type, addNum);
	}

	public Sticker CreateStickerToPratiti(StickerType type){
        return m_itemSystem.CreateStickerToPratiti(type) ;
    }

	public void AddStickerChip(StickerType type, int addNum){
		m_itemSystem.AddStickerChip(type, addNum);
	}

	public void GetStone(StoneType type){
		m_itemSystem.GetStone(type);
	}

	public void AddHappyGrass(int num){
		m_itemSystem.AddHappyGrass(num);
	}

	public int GetHappyGrassCount(){
		return m_itemSystem.GetHappyGrassCount();
	}

	// ================================================
	// =================SaveSystem===============
	// ================================================
	public void Save(){
		m_saveSystem.Save();
	}

	public void Load(){
		m_saveSystem.Load();
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