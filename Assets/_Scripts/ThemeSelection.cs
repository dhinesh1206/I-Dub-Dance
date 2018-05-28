using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThemeSelection : MonoBehaviour {

	public List<ScreensList> screensList;
	public int Index;
	// Use this for initialization
	void Start () {
		ChangeTheme (0);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void ChangeTheme(int ThemeNumber) {
		print (ThemeNumber);
		foreach (var obj in screensList) {
			foreach (GameObject screen in obj.Screens) {
				Index = obj.Screens.IndexOf (screen);
				if (Index == ThemeNumber) {
					MainUiController.instance.themeIndex = Index;
					TestScrollItems.instance.ThemeIndex = Index;
					//TestScrollItems.instance.DifferentTheme ();
					screen.SetActive (true);
				} else {
					screen.SetActive (false);
				}
			}
		}

	}
}

[System.Serializable]

public class ScreensList {
	public string ScreenName;
	public List<GameObject> Screens;
}

