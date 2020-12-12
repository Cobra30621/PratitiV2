using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MapPratiti : MonoBehaviour
{
    public PratitiType _pratitiType;
    public PratitiAttr _pratitiAttr;

    public StickerType[] _stickerTypes ;
    public LootTable _lootTable;
    
    /// <summary>
    /// Start is called on the frame when a script is enabled just before
    /// any of the Update methods is called the first time.
    /// </summary>
    void Start()
    {
        IAssetFactory _factory = MainFactory.GetAssetFactory();
        PratitiData data = _factory.LoadPratitiData(_pratitiType);
        _pratitiAttr = new PratitiAttr(_pratitiType, data);
        _pratitiAttr.SetPlusData(_stickerTypes);
    }

    public void SetEnemyAttr(){
        GameMediator.Instance.SetEnemyPratiti(this);
    }

    public string GetID(){
        if (GetComponent<SaveableEntity>() == null){
            Debug.Log("沒掛SaveableEntity，請掛上");
            return "";
        }
        if ( GetComponent<SaveableEntity>().ID == ""){
            Debug.Log("尚未產生ID，請產生");
            return "";
        }

        return GetComponent<SaveableEntity>().ID;
    }


}