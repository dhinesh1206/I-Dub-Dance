#if UNITY_EDITOR
using UnityEditor;
using System.IO;

public class AssetBundels
{
	[MenuItem("Assets/Build AssetBundles")]
	static void BuildAllAssetBundles()
	{
		string assetBundleDirectory = "Assets/AssetBundles";
		if(!Directory.Exists(assetBundleDirectory))
		{
			Directory.CreateDirectory(assetBundleDirectory);
		}
		BuildPipeline.BuildAssetBundles(assetBundleDirectory, BuildAssetBundleOptions.StrictMode, BuildTarget.iOS);
	}
}
#endif