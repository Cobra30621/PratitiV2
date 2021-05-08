using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Stone : IItem{
    public StoneData _stoneData;
    public StoneType _stoneType;
    public bool _hadGet = false;

    public Stone(StoneType type, StoneData data){ 
        _stoneType = type;
        _stoneData = data;

    }

    public Stone(StoneType type){
        _stoneType = type;
        IAssetFactory _factory =  MainFactory.GetAssetFactory();

        _stoneData = _factory.LoadStoneData(type);
    }

    public override string ToString(){
        return $"{_stoneType}俗頭";
    }
}