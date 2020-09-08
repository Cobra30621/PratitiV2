using UnityEngine;
using UnityEngine.UI;
using System.Collections;

// 戰鬥狀態
public class MapState : ISceneState
{
	public MapState(SceneStateController Controller):base(Controller)
	{
		this._sceneState = SceneState.Map;
		this.StateName = "MapState";
		GameMediator.Instance.SetSceneState(this); // 將自己加入GameMediator中
	}

	// 開始
	public override void StateBegin()
	{
		// GameMediator.Instance.Initinal();
	}

	// 結束
	public override void StateEnd()
	{
        // GameMediator.Instance.Release();
		// PBaseDefenseGame.Instance.Release();
	}
			
	// 更新
	public override void StateUpdate()
	{	
		// 遊戲邏輯
		GameMediator.Instance.Update();
		// Render由Unity負責
	}

	public void EnterBattle(){
		m_Controller.SetState(new BattleState(m_Controller), "Battle" );
	}

	public void LeaveGame(){
		m_Controller.SetState(new MainMenuState(m_Controller), "MainMenu" );
	}
}
