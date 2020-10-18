
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Fungus;

public enum MapName{
    Village, DessertHouse1, DessertHouse2
}

public class MapSystem : IGameSystem
{
    private Flowchart mainFlowchart; // 遊戲共同事件
    private GameObject player;

    private MapState mapState; // 地圖場景狀態
    private BattleState battleState; // 戰鬥場景狀態

    public GameObject[] Cameras;
    

    // 玩家位置相關
    public Vector3 _playPos;
    public MapName _nowMap;

    public MapSystem(GameMediator mediator):base(mediator)
	{
		Initialize();
    }

     public override void Initialize(){
        SetMapObject();
        Cameras = GameObject.FindGameObjectsWithTag("VSCamera"); // 找到所有的Camera
        SetCamera(MapName.Village); // 暫時直接設為村子
    }

    // 每次回到地圖，會重新抓這些東西
    public void SetMapObject(){
        mainFlowchart = GameObject.Find("MainFlowchart").GetComponent<Flowchart>();
        player = GameObject.Find("Player");

        if(mainFlowchart == null)
            Debug.LogError("找不到Map.sceme的mainFlowchart");

        if(player == null)
            Debug.LogError("找不到Map.sceme的player"); 
    }

    public void EnterBattle(){
        SaveVariable(); // 儲存故事資料
        SavePlayerPos(); // 儲存玩家位置
        // loadPratitiData
        mapState = (MapState) GameMediator.Instance.GetSceneState(SceneState.Map); // 取得Map狀態
        mapState.EnterBattle(); // 進入戰鬥
    }

    public void BackToMap(){
        SceneManager.sceneLoaded += OnSceneLoaded; // 新增讀取場景後的動作
        battleState = (BattleState) GameMediator.Instance.GetSceneState(SceneState.Battle);
        battleState.BackToMap(); // 回到地圖
        
    }

    // 等讀取完場景，再執行
    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        LoadVariable(); // 讀取劇情變數
        GameMediator.Instance.InitializeTalkState(); // 更新所有物件狀態
        SetPlayerPos(_playPos); // 更改玩家位置
        SetCamera(_nowMap); // 設定攝影機
        // 淡入淡出
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    // 設置攝影機
    public void SetCamera(MapName map){
        _nowMap = map;
        if(Cameras == null)
            Cameras = GameObject.FindGameObjectsWithTag("VSCamera"); // 找到所有的Camera
        
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
        
        _playPos = player.transform.position;
    }

    public void SetPlayerPos(Vector3 pos){
        if(player == null)  
            SetMapObject();
        
        player.transform.position = pos;
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
    public void SaveVariable(){
        SetMapObject(); // 重新找mainFlowchart（切場景會消失）
        UseFungus.PlayBlock(mainFlowchart.gameObject, "SaveVariable");
    }

    public void LoadVariable(){
        SetMapObject(); // 重新找mainFlowchart（切場景會消失）
        UseFungus.PlayBlock(mainFlowchart.gameObject, "LoadVariable");
    }
}


