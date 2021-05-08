
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum StoneType{
    Fire , 
    Ice, 
    Rock , 
    Light,
     Water, Wind
}


[CreateAssetMenu(fileName= "StoneData", menuName= "Pratiti/Craete StoneData")]
public class StoneData : ScriptableObject{
    public StoneType _stoneType;
    public string _name;
    public string _descript;
    public Sprite _icon;


}

// + stoneType :StoneType 
// + name : string
// + descript: name
// + icon : sprite
// + addAttr: int