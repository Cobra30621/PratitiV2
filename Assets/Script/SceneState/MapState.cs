using UnityEngine;
using UnityEngine.UI;
using System.Collections;

// 戰鬥狀態
public class MapState : ISceneState
{
	public MapState(SceneStateController Controller):base(Controller)
	{
		this.StateName = "MapState";
	}

	// 開始
	public override void StateBegin()
	{
		GameMediator.Instance.Initinal();
	}

	// 結束
	public override void StateEnd()
	{
        GameMediator.Instance.Release();
		// PBaseDefenseGame.Instance.Release();
	}
			
	// 更新
	public override void StateUpdate()
	{	
		// 遊戲邏輯
		GameMediator.Instance.Update();
		// Render由Unity負責

        // 是否要進入戰鬥
        if(GameMediator.Instance.GetSceneState() == SceneState.Battle)
            m_Controller.SetState(new MainMenuState(m_Controller), "Battle" );

        // 是否要離開遊戲
        if(GameMediator.Instance.GetSceneState() == SceneState.MainMenu)
            m_Controller.SetState(new MainMenuState(m_Controller), "MainMenu" );
	}

}
