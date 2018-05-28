using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class pngSequesnce : MonoBehaviour {

	public Sprite[] images;
	public Image parent;
	public float speed;

	// Use this for initialization
	IEnumerator Start () {
		foreach(Sprite img in images) {
			parent.sprite = img;
				yield return new WaitForSeconds(speed);
		}
		StartCoroutine (Start ());
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
