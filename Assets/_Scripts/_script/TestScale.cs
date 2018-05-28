using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class TestScale : MonoBehaviour {

	public GameObject[] objects;
	public Vector3 maxSize;
	public float scaleTime;
	public float starttime;
	public float waitTime;

	// Use this for initialization
	void Start () {
		StartCoroutine (Startt ());
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	IEnumerator Startt() {
		yield return new WaitForSeconds (starttime);
		StartCoroutine (ScaleObjects());
	}

	IEnumerator ScaleObjects() {
		foreach (GameObject Obj in objects) {
			yield return new WaitForSeconds (waitTime);
			Obj.transform.DOScale (maxSize, scaleTime).SetEase (Ease.Linear);
		}
	}

}
