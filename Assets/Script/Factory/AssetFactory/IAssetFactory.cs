using UnityEngine;
using System.Collections;

// 將Unity Asset實體化成GameObject的工廠類別
public abstract class IAssetFactory
{
	// 讀取照片
	public abstract Sprite LoadPratitiSprite(PratitiType type); 

	// 產生物件
	public abstract GameObject LoadTalkIcon( string AssetName );


}

/*
 * 使用Abstract Factory Patterny簡化版,
 * 讓GameObject的產生可以依Uniyt Asset放置的位置來載入Asset
 * 先實作放在Resource目錄下的Asset及Remote(Web Server)上的
 * 當Unity隨著版本的演進，也許會提供不同的載入方式，那麼我們就可以
 * 再先將一個IAssetFactory的子類別來因應變化
 */