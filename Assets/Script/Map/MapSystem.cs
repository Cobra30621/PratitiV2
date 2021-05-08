
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Fungus;
using Cinemachine;

[System.Serializable]
public enum MapName{
    Room, Gallery, DevilCity, InfiniteCorridor, DevilCity2, Forest, ForestFrontier, Village, TargetHouse, End 
}

[System.Serializable]
public class MapSaveData{
    public Vector3 playerPos;
    public MapName nowMap;
    public MapSaveData(MapSystem mapSystem){
        playerPos = mapSystem._playerPos;
        nowMap = mapSystem._nowMap;
    }
}

public class MapSystem : IGameSystem
{
    private Flowchart mainFlowchart; // 遊戲共同事件
    private GameObject player;

    private MapState mapState; // 地圖場景狀態
    private BattleState battleState; // 戰鬥場景狀態

    public GameObject[] Cameras;
    private GameObject mainCamera;

    private Dictionary<string, PratitiMove> _dicEnemyPratitis; // 所有敵方帕拉提提
    

    // 玩家位置相關
    public Vector3 _playerPos;
    public Vector3 _startPos;
    public MapName _nowMap;

    public BattleOutcome _battleOutcome;
    public string nowEnemyId;

    public bool whetherLoadPlayerPos;

    public MapSystem(GameMediator mediator):base(mediator)
	{
		Initialize();
    }

     public override void Initialize(){
        LoadSaveData(); // 讀取檔案
        SetMapObject();
        SetCamera();
    }

    public void Enter2PBattle(){
        GameMediator.Instance.SetWhether2P(true); //是2P對戰

        SaveVariable(); // 儲存故事資料
        SavePlayerPos(); // 儲存玩家位置
        GameMediator.Instance.Save(); // 存檔
        mapState = (MapState) GameMediator.Instance.GetSceneState(SceneState.Map); // 取得Map狀態
        mapState.EnterBattle(); // 進入戰鬥
    }


    public void EnterBattle(){
        SaveVariable(); // 儲存故事資料
        SavePlayerPos(); // 儲存玩家位置
        GameMediator.Instance.Save(); // 存檔
        mapState = (MapState) GameMediator.Instance.GetSceneState(SceneState.Map); // 取得Map狀態
        GameMediator.Instance.SetWhether2P(false); //不是2P對戰
        mapState.EnterBattle(); // 進入戰鬥
    }

    public void EndBattle(BattleOutcome outcome){
        _battleOutcome = outcome;
        switch(outcome){
            case BattleOutcome.Win:
                Debug.Log("贏的戰鬥");
                SceneManager.LoadScene( "EndBattleScene" );
                break;
            case BattleOutcome.Lose:
                Debug.Log("輸了戰鬥");
                SceneManager.LoadScene( "EndBattleScene" );
                break;
        }
    }

    public BattleOutcome GetBattleOutcome(){
        return _battleOutcome;
    }

    public void BackToMap(){
        SceneManager.sceneLoaded += OnSceneLoaded; // 新增讀取場景後的動作
        battleState = (BattleState) GameMediator.Instance.GetSceneState(SceneState.Battle);
        battleState.BackToMap(); // 回到地圖
        
    }

    // 等讀取完場景，再執行
    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        SetMapObjectAsLoadData();
        GameMediator.Instance.OnSceneLoad(); // 所有系統重回場景要重新訂閱的

        if(_battleOutcome == BattleOutcome.Win){
            DefeatPratiti();
            // 戰鬥結束劇情
        }
        if(_battleOutcome == BattleOutcome.Lose){
            SetPlayerPos(_startPos);
        }

        SetCamera(_nowMap); // 設定攝影機
        GameMediator.Instance.PlayEndBattleStory(); // 回地圖後，播放劇情
        
        // 淡入淡出
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    // 讀檔時設定場景物件的位置
    private void SetMapObjectAsLoadData(){
        SetEnemyPratitiInDic(); // 抓取地圖中的帕拉提提;
        GameMediator.Instance.Load(); // 讀檔
        GameMediator.Instance.InitializeTalkState(); // 更新所有物件狀態
        LoadVariable(); // 讀取劇情變數

        Cameras = GameObject.FindGameObjectsWithTag("VSCamera"); // 找到所有的Camera
        SetPlayerPos(_playerPos); // 更改玩家位置
        Debug.Log($"玩家位置{_playerPos}");
        SetCamera(_nowMap); // 設定攝影機
    }

    // 設置攝影機
    public void SetCamera(MapName map){
        _nowMap = map;

        foreach(GameObject camera in Cameras){
            camera.SetActive(false);
            if(camera.GetComponent<ICamera>()._mapName == map)
            {
                camera.SetActive(true);
                Debug.Log($"打開攝影機{map}");
            }
        }
    }

    // ===================玩家位置方法===================
    public void SavePlayerPos(){
        if(player == null)  
            SetMapObject();
        
        _playerPos = player.transform.position;
    }

    public void SetPlayerPos(Vector3 pos){
        if(player == null)  
            SetMapObject();
        
        player.transform.position = pos;
    }

    public GameObject GetPlayerObject(){
        if(player == null)  
            SetMapObject();

        return player;
    }

    public void SetStartPos(Vector3 vector){
        _startPos = vector;
    }


    // ===================場景中帕拉提提處理===================
    public void SetEnemyPratitiInDic(){
        _dicEnemyPratitis = new Dictionary<string, PratitiMove>();
        foreach (var pratitiMove in GameObject.FindObjectsOfType<PratitiMove>())
        {
            _dicEnemyPratitis[pratitiMove.GetID()] = pratitiMove;
        } 
    }

    public void SetEnemyPratiti(MapPratiti pratiti) {
        nowEnemyId = pratiti.GetID();
    }

    public void DefeatPratiti(){
        if(_dicEnemyPratitis == null)
            SetEnemyPratitiInDic();
        
        if(_dicEnemyPratitis.ContainsKey(nowEnemyId)){
            PratitiMove pratiti = _dicEnemyPratitis[nowEnemyId];
            pratiti.IsDefeat();
        }
        
    }

    // 重置場景帕拉提提
    public void ResetMapPratiti(MapName mapName){
        if(_dicEnemyPratitis == null)
            SetEnemyPratitiInDic();

        foreach (PratitiMove pratiti in _dicEnemyPratitis.Values )
        {
            if (pratiti.mapName == mapName)
                pratiti.Reset();
        }
    }



    // ===================轉場方法===================
    public float duration = 1f;
	float targetAlpha = 1f;
	Color fadeColor = Color.black;
    FadeScreenMethon fadeMethon = new FadeScreenMethon(); // 執行轉場的Fungus方法

    public void ChangeScene(Vector3 vec, MapName map){
        var cameraManager = FungusManager.Instance.CameraManager;
        

        cameraManager.ScreenFadeTexture = CameraManager.CreateColorTexture(fadeColor, 32, 32);
        cameraManager.FadeOut(duration, delegate { // 淡出
                fadeMethon.Continue(); // 繼續對話
                SetCamera(map); // 設置攝影機
                SetPlayerPos(vec); // 轉移玩家位置
                _startPos = vec;
                ResetMapPratiti(map); // 重置場景帕拉提提
                fadeMethon.FadeIn(); // 淡入
        });
    }

    public void FadeIn(){
        fadeMethon.FadeIn();
    }
    
    public void FadeOut(){
        fadeMethon.FadeOut();
    }


    // ====================== 存擋相關方法 ==========================

    public MapSaveData CreateSaveData(){
        SavePlayerPos();
        MapSaveData saveData = new MapSaveData(this);
        SaveVariable();
        return saveData;
    }

    public void LoadSaveData(){
        MapSaveData mapSaveData = SaveFile.LoadMapData();
        if(mapSaveData == null){
            Debug.Log("MapSystem沒讀取到資料");
            return;
        }

        if(!whetherLoadPlayerPos) // 是否讀取玩家位置
            return;
        
        _playerPos = mapSaveData.playerPos;
        _nowMap = mapSaveData.nowMap;

        SetMapObjectAsLoadData();
    }

    public void SetWhetherLoadPlayerPos(bool bo){
        whetherLoadPlayerPos = bo;
    }


    public void SaveVariable(){
        SetMapObject(); // 重新找mainFlowchart（切場景會消失）
        UseFungus.PlayBlock(mainFlowchart.gameObject, "SaveVariable");
    }

    public void LoadVariable(){
        SetMapObject(); // 重新找mainFlowchart（切場景會消失）
        UseFungus.PlayBlock(mainFlowchart.gameObject, "LoadVariable");
    }

    // 每次回到地圖，會重新抓這些東西
    public void SetMapObject(){
        mainFlowchart = GameObject.Find("MainFlowchart").GetComponent<Flowchart>();
        player = GameObject.Find("Player");
        // _startPos = player.transform.position;

        if(mainFlowchart == null)
            Debug.LogError("找不到Map.sceme的mainFlowchart");

        if(player == null)
            Debug.LogError("找不到Map.sceme的player"); 
    }

    public void SetCamera(){
        Cameras = GameObject.FindGameObjectsWithTag("VSCamera"); // 找到所有的Camera
        mainCamera = GameObject.FindGameObjectWithTag("MainCamera");
    }
}


