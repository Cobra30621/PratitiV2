
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StoneBar : MonoBehaviour
{
    [SerializeField] private Image img_icon;
    [SerializeField] private Button butt_select;

    public IAssetFactory factory;
    public Stone _stone;
    public StoneType _type;

    public void Initialize(Stone stone){
        _stone = stone;
        img_icon.sprite = _stone._stoneData._icon;
        RefreshInfo();
    }   

    public void RefreshInfo(){
        UpdateImageState();
        
    }

    // 更新圖片狀態
    public void UpdateImageState(){
        img_icon.gameObject.SetActive(_stone._hadGet);
    }

    public string GetStoneDes(){
        return _stone._stoneData._descript;
    }
}