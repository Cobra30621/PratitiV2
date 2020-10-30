
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

[CreateAssetMenu(fileName= "PratitiData", menuName= "Pratiti/Craete PratitiData")]
public class PratitiData : ScriptableObject
{
    public string _name;
    public PratitiType _pratitiType;
    public Sprite _icon;

    public int _rawHp;
    public int _rawAttr;
    public int _rawDef;
    public int _rawSpeed;
}