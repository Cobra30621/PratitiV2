using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fungus;

public class SaveSystem : IGameSystem
{
    public SaveSystem(GameMediator mediator):base(mediator)
	{
		Initialize();
    }

    public Vector3 _playerPos;
    
}