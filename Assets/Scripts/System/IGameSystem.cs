


using UnityEngine;
using System.Collections;

// 遊戲子系統共用界面
public abstract class IGameSystem
{
    protected SystemController m_SControl = null;
    public IGameSystem(SystemController SControl)
    {
        m_SControl = SControl;
    }

    public virtual void Initialize() { }
    public virtual void Release() { }
    public virtual void Update() { }

}
