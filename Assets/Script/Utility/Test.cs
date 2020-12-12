using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
   
    /// <summary>
    /// Start is called on the frame when a script is enabled just before
    /// any of the Update methods is called the first time.
    /// </summary>

    public StoneType type; 
    void Start()
    {
        GameMediator.Instance.GetStone(type);
    }

    [ContextMenu("Save")]
    public void Save(){
        GameMediator.Instance.Save();
    }

    [ContextMenu("Load")]
    public void Load(){
        GameMediator.Instance.Load();
    }

}
