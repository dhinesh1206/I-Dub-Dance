using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class MoveANdscale : MonoBehaviour {

	public Vector3 targetPosition;

	public Vector3 initialPosition;
	// Use this for initialization
	void OnEnable () {
		transform.DOScale (Vector3.one, 1);
		transform.DOLocalMove (targetPosition, 1, false);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnDisable() {
		transform.DOScale (Vector3.zero, 1);
		transform.DOLocalMove (initialPosition, 1, false);
	}
}
