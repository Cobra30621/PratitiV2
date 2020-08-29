using UnityEngine;
using System;
using System.Collections;

// 遊戲主迴圈
public class GameLoop : MonoBehaviour 
{
	// 場景狀態
	SceneStateController m_SceneStateController = new SceneStateController();

	static GameLoop instance;

	public SceneState startScene = SceneState.Map;

    void Awake () {
        if (instance==null)
        {
            instance = this;
            DontDestroyOnLoad(this);
        }
        else if (this!=instance)
        {
            Destroy(gameObject);
        }      
	}
    

	// Use this for initialization
	void Start () 
	{
		// 設定起始的場景
		SetStartStage(startScene);
	}

	private void SetStartStage(SceneState sceneState){
		switch(sceneState){
			case SceneState.MainMenu:
				m_SceneStateController.SetState(new MainMenuState(m_SceneStateController), "");
				break;
			case SceneState.Map:
				m_SceneStateController.SetState(new MapState(m_SceneStateController), "");
				break;
			case SceneState.Battle:
				m_SceneStateController.SetState(new BattleState(m_SceneStateController), "");
				break;
		}
	}

	// Update is called once per frame
	void Update () 
	{
		m_SceneStateController.StateUpdate();	
	}
}
