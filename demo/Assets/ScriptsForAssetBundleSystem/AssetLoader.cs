using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class AssetLoader : BaseLoader
{
	private static AssetLoader msInstance = null;
	private const string ObjectName = "AssetLoader";

	public static AssetLoader Instance
	{
		get
		{
			if (msInstance == null)
			{
				msInstance = new GameObject( ObjectName, typeof( AssetLoader ) ).GetComponent<AssetLoader>();
			}

			return msInstance;
		}
	}

	private static bool isReady = false;

	public bool IsReady
	{
		get
		{
			return isReady;
		}
	}

	IEnumerator Start()
	{
		yield return StartCoroutine( Initialize() );
		isReady = true;
	}

	public static void LoadRequest( string assetBundleName, string assetName, Action<UnityEngine.Object> callback )
	{
		AssetLoader.Instance.StartCoroutine( AssetLoader.Instance.Load( assetBundleName, assetName, ( assetObject ) =>
		{
			if (callback != null)
			{
				callback( assetObject );
			}

			AssetBundleManager.UnloadAssetBundle( assetBundleName );
		} ) );
	}
}