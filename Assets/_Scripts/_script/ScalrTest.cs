using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ScalrTest : MonoBehaviour {


	//public GameObject target;
	public Vector3 maxScalevalue;
	public Vector3 minScaleValue;
	public float waitTime;
	public float scaleTime;
	public float scalDownTime;
	public float holdTime;
	// Use this for initialization
	void Start () {
		StartCoroutine (scale ());
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	IEnumerator scale() {
		yield return new WaitForSeconds (waitTime);
		transform.DOScale (maxScalevalue, scaleTime).SetEase (Ease.Linear).OnComplete (() => {
			//StartCoroutine(scaleDown());
		});
	}

	IEnumerator scaleDown() {
		yield return new WaitForSeconds (holdTime);
		transform.DOScale (minScaleValue, scalDownTime).SetEase (Ease.Linear);
	}
}
