
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Fungus;

public class PratitiSystem : IGameSystem
{
    public List<BagPratiti> _bagPratitis = new List<BagPratiti>();
    public BagPratiti _startPratiti;

    public PratitiSystem(GameMediator mediator):base(mediator)
	{
		Initialize();
    }

    public override void Initialize(){
        CreateBagPratiti(PratitiType.Pig);
        CreateBagPratiti(PratitiType.Feather);

        _startPratiti = _bagPratitis[0];
    }

    public void CreateBagPratiti(PratitiType type){
        BagPratiti pratiti = new BagPratiti(type);
        pratiti._ID = _bagPratitis.Count; // 設定帕拉緹緹ID
        _bagPratitis.Add(pratiti);

    }

    public void SetStartPratiti(int ID){
        if(ID > _bagPratitis.Count){
            Debug.LogError("設定初始帕拉提提超過包包上限");
            return;
        }
        _startPratiti = _bagPratitis[ID];

        // 刷新UI介面
        PratitiUI.Refresh();
    }
}