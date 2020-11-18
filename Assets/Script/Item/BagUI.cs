using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class BagUI : MonoBehaviour
{
    private ItemSystem _itemSystem;
    public Dictionary<StickerType, Sticker> _dicStickers ;
    public Dictionary<StickerType, StickerChip> _dicStickerChips;

    public List<CompositeStickerBar> _compostierStickerBars;

    [SerializeField] GameObject compositeStickerBarPrefab;
    [SerializeField] Transform transform_book;
    // Start is called before the first frame update
    void Start()
    {
        Initialize();
    }

    public void Initialize(){
        _itemSystem = GameMediator.Instance.GetItemSystem();
        _dicStickers = _itemSystem._dicStickers;
        _dicStickerChips = _itemSystem._dicStickerChips;

        CreateAllCompostieStickerBars();
        RefreshInfo();
    }  

    public void RefreshInfo(){
        foreach(CompositeStickerBar bar in _compostierStickerBars){
            bar.RefreshInfo();
        }
    }

    public void CompositeStickerChip(StickerType type){
        _itemSystem.CompositeStickerChip(type);
    }



    public void CreateAllCompostieStickerBars(){
        foreach (StickerType type in Enum.GetValues( typeof( StickerType ) )){
            if (type != StickerType.Null)
            {
                Sticker sticker = _dicStickers[type];
                StickerChip chip = _dicStickerChips[type];
                CreateCompostieStickerBar(sticker, chip);
            }
        }
    }

    /// <summary>
    /// 製作一個CompostieStickerBar
    /// </summary>
    public void CreateCompostieStickerBar(Sticker sticker, StickerChip chip)
    {
        var g = Instantiate(compositeStickerBarPrefab, transform_book);
        // _barGameObjects.Add(g);

        var l = g.GetComponent<CompositeStickerBar>();
        l.Initialize(sticker, chip); 
        l.SetBagUI(this);
        _compostierStickerBars.Add(l);
        // Debug.Log($"產生{pratiti._pratitiType}類，編號{pratiti._ID}的帕拉提提Bar");
    }
}
