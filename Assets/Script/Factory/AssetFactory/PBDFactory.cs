using UnityEngine;
using System.Collections;

// 取得P-BaseDefenseGame中所使用的工廠
public static class MainFactory
{
	private static bool   		 m_bLoadFromResource = true;
	private static IAssetFactory 	 m_AssetFactory = null;
	

	// 取得將Unity Asset實作化的工廠
	public static IAssetFactory GetAssetFactory()
	{
		if( m_AssetFactory == null)
		{
				m_AssetFactory = new ResourceAssetFactory();
		}
		return m_AssetFactory;
	}

}
