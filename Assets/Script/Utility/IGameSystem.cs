using UnityEngine;
using System.Collections;

// 遊戲子系統共用界面
public abstract class IGameSystem
{
	protected GameMediator m_PBDGame = null;
	public IGameSystem( GameMediator PBDGame )
	{
		m_PBDGame = PBDGame;
	}

	public virtual void Initialize(){}
	public virtual void Release(){}
	public virtual void Update(){}
	public virtual void OnSceneLoad(){}

}
