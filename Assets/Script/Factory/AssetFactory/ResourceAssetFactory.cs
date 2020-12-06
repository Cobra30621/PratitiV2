using UnityEngine;
using System.Collections;
using System;

// 從專案的Resource中,將Unity Asset實體化成GameObject的工廠類別
public class ResourceAssetFactory : IAssetFactory 
{
	public const string PratitiDataPath = "PratitiData/";
	public const string StickerDataPath = "StickerData/";
	public const string StoneDataPath = "StoneData/";
	public const string TalkIconPath = "TalkIcon/";
	public const string PratitiSpritePath = "Pratiti/";
	
	public override StoneData LoadStoneData(StoneType type){
		string stoneName = Enum.GetName(typeof(StoneType), type );

		return Resources.Load( StoneDataPath + stoneName,typeof(StoneData)) as StoneData;
	}

	public override PratitiData LoadPratitiData(PratitiType type){
		string pratitiName = "feather";
        switch(type){
            case PratitiType.Feather:
                pratitiName = "feather";
                break;
            case PratitiType.Pig:
                pratitiName = "pig";
                break;
			case PratitiType.Cloud:
                pratitiName = "cloud";
                break;
            default:
                Debug.LogError("找不到Icon");
                break;
        }

		return Resources.Load(  PratitiDataPath + pratitiName,typeof(PratitiData)) as PratitiData;
	}

	public override StickerData LoadStickerData(StickerType type){
		string name = "attr";
        switch(type){
            case StickerType.Attr:
                name = "attr";
                break;
			case StickerType.Hp:
                name = "hp";
                break;
			case StickerType.Def:
                name = "def";
                break;
			case StickerType.Null:
                name = "null";
                break;
            default:
                Debug.LogError("找不到StickerData");
                break;
        }

		return Resources.Load(  StickerDataPath + name,typeof(StickerData)) as StickerData;
	}

	// 產生Icon
	public override GameObject LoadTalkIcon( string AssetName )
	{	
		return InstantiateGameObject( TalkIconPath + AssetName );
	}

	// 產生GameObject
	private GameObject InstantiateGameObject( string AssetName )
	{
		// 從Resrouce中載入
		UnityEngine.Object res = LoadGameObjectFromResourcePath( AssetName );
		if(res==null)
			return null;
		return  UnityEngine.Object.Instantiate(res) as GameObject;
	}

	// 從Resrouce中載入
	public UnityEngine.Object LoadGameObjectFromResourcePath( string AssetPath)
	{
		UnityEngine.Object res = Resources.Load(AssetPath);
		if( res == null)
		{
			Debug.LogWarning("無法載入路徑["+AssetPath+"]上的Asset");
			return null;
		}		
		return res;
	}
}
