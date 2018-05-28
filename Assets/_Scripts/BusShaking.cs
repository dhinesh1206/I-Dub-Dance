using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class BusShaking : MonoBehaviour {

	// Use this for initialization
	void Start () {
		StartCoroutine(busShaking());
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public IEnumerator busShaking() {
		yield return new WaitForSeconds (0.01f);
		transform.DOLocalMove (new Vector3(transform.localPosition.x-10, transform.localPosition.y-10, 0),0.2f,false).OnComplete (() => {
			transform.DOLocalMove (new Vector3(transform.localPosition.x+10, transform.localPosition.y+10, 0),0.2f,false).OnComplete (() => {
				StartCoroutine(busShaking());
			});
		});
	}
}
