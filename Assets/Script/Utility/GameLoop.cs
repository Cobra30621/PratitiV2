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
		// 設定起始的場景
		SetStartStage(startScene);
		GameMediator.Instance.Initinal();
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
		if (Input.GetKey(KeyCode.Q))
			GameMediator.Instance.FadeIn();
		if (Input.GetKey(KeyCode.W))
			GameMediator.Instance.FadeOut();
		if (Input.GetKey(KeyCode.E))
		{
			GameMediator.Instance.FadeOut();
			GameMediator.Instance.FadeIn();
		}
		if (Input.GetKey(KeyCode.R))
			GameMediator.Instance.FadeInAndOut();

			

	}


	// =============== 測試用方法 ＝==============
	public void EndBattle(){
		GameMediator.Instance.BackToMap();
	}

}
