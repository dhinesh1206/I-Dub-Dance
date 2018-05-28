using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class TitleButtonController : MonoBehaviour {

	public List<ButtonNames> animationNames;
	public Color color;
	public Transform TitleParent;
	public Color bgColor;
	public Color smallIconsBgColor;


	// Use this for initialization
	void Start () {
			
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void onClick() {
//		InstantiateScrollItems.instance.StopAllCoroutines ();
		TestScrollItems.instance.StopAllCoroutines ();
		foreach (Transform obj in TitleParent) {
			obj.DOScale (Vector3.one,0.5f);
		}
		transform.DOScale (new Vector3 (1.25f, 1.25f, 1.25f), 0.5f);
		//InstantiateScrollItems.instance.AnimateList (animationNames, color,bgColor,smallIconsBgColor);
		TestScrollItems.instance.AnimateList (animationNames, color,bgColor,smallIconsBgColor);
	}
}
