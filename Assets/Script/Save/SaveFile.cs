using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using LitJson;

public class SaveData{
    public List<PratitiSaveData> _pratitiSaveDatas;
    public List<StickerSaveData> _stickerSaveDatas;
    public List<StickerSaveData> _stickerChipSaveDatas;
    public MapSaveData _mapSaveData;

    public void SetPratitiData(List<PratitiSaveData> pratitis){
        _pratitiSaveDatas = pratitis;
    }

    public void SetStickerData(List<StickerSaveData> stickers, List<StickerSaveData> stickerChips){
        _stickerSaveDatas = stickers;
        _stickerChipSaveDatas = stickerChips;
    }

    public void SetMapData(MapSaveData map){
        _mapSaveData = map;
    }
}


public class SaveFile
{
    static private SaveData saveData;
    static private string key = "Pratiti_Save";
    static private IAssetFactory _factory;

    static public void Save()
    {
        CreateSaveData();
        string json = JsonMapper.ToJson(saveData);
        // string json = JsonUtility.ToJson(saveData);
        PlayerPrefs.SetString(key , json);
        PlayerPrefs.Save();
        Debug.Log($"Save{key}"+ PlayerPrefs.GetString(key));
    }

    static public void CreateSaveData(){
        saveData = new SaveData();

        PratitiSystem pratitiSystem = GameMediator.Instance.GetPratitiSystem();
        List<PratitiSaveData> pratitiSaveDatas = pratitiSystem.CreateSaveData();
        saveData.SetPratitiData(pratitiSaveDatas);

        ItemSystem _itemSystem = GameMediator.Instance.GetItemSystem();
        List<StickerSaveData> stickerSaveDatas = _itemSystem.CreateStickerSaveData();
        List<StickerSaveData> stickerChipSaveDatas = _itemSystem.CreateStickerChipSaveData();
        saveData.SetStickerData(stickerSaveDatas, stickerChipSaveDatas);
        
        MapSystem _mapSystem = GameMediator.Instance.GetMapSystem();
        MapSaveData mapSaveData = _mapSystem.CreateSaveData();
        saveData.SetMapData(mapSaveData);

        Debug.Log("創造SaveData"+saveData);
    }
    
    // Load
    static public List<PratitiSaveData> LoadPratitiData(){
        if(Load())
        {
            Debug.Log($"讀取帕拉提提資料：{saveData._pratitiSaveDatas}");
            return saveData._pratitiSaveDatas;
        }
            
        else
            return null;
    }

    static public List<StickerSaveData> LoadStickerData(){
        if(Load())
        {
            Debug.Log($"讀取帕拉提提資料：{saveData._stickerSaveDatas}");
            return saveData._stickerSaveDatas;
        }
            
        else
            return null;
    }

    static public List<StickerSaveData> LoadStickerChipData(){
        if(Load())
        {
            Debug.Log($"讀取帕拉提提資料：{saveData._stickerSaveDatas}");
            return saveData._stickerChipSaveDatas;
        }
            
        else
            return null;
    }

    static public MapSaveData LoadMapData(){
        if(Load())
        {
            return saveData._mapSaveData;
        }
        else
            return null;
    }

    // 回傳值為Null，代表目前沒資料
    static public bool Load(){
        string json = PlayerPrefs.GetString(key);
        saveData  = JsonUtility.FromJson<SaveData>(json);
        if(saveData == null){
            Debug.Log("Load:目前沒資料");
            return false;
        }
        else{
            Debug.Log("Load:"+ PlayerPrefs.GetString(key));
            return true;
        }
    }

    static public void DeleteAll()
    {   
        PlayerPrefs.DeleteAll();
        Debug.Log("清除資料");
    }

    static public void SetAssetFactory(){
        if(_factory == null)
            _factory = MainFactory.GetAssetFactory();
    }

}


// static public void CreateSaveData(){

// List<BagPratiti> bagPratitis = GameMediator.Instance.GetBagPratitis();
        // SetAssetFactory();

        // foreach (BagPratiti pratiti in bagPratitis) // Json無法讀取Data裡的sprite
        // {
        //     pratiti._pratitiData = _factory.LoadPratitiData(PratitiType.Null);;
        // }

        // foreach (Sticker sticker in _dicStickers.Values) // Json無法讀取Data裡的sprite
        // {
        //     sticker._stickerData = null;
        // }