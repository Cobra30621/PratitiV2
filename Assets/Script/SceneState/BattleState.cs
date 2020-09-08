using UnityEngine;
using UnityEngine.UI;
using System.Collections;

// 戰鬥狀態
public class BattleState : ISceneState
{
	public BattleState(SceneStateController Controller):base(Controller)
	{
		this._sceneState = SceneState.Battle;
		this.StateName = "BattleState";
		GameMediator.Instance.SetSceneState(this); // 將自己加入GameMediator中
	}

	// 開始
	public override void StateBegin()
	{
		
	}

	// 結束
	public override void StateEnd()
	{
		
	}
			
	// 更新
	public override void StateUpdate()
	{	

	}

	public void BackToMap(){
		m_Controller.SetState(new MapState(m_Controller), "Map" );
	}
}
