using UnityEngine;
using UnityEngine.UI;
using System.Collections;

// 戰鬥狀態
public class BattleState : ISceneState
{
	public BattleState(SceneStateController Controller):base(Controller)
	{
		this.StateName = "BattleState";
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

		// 是否要離開戰鬥
        if(GameMediator.Instance.GetSceneState() == SceneState.Battle)
            m_Controller.SetState(new MainMenuState(m_Controller), "Map" );
	}
}
