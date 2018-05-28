using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class MovementControll : MonoBehaviour {

	public int endPosition;
	public float Time;

	// Use this for initialization
	void Start () {
		MovementStart ();	
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void MovementStart() {
		endPosition = -endPosition;
		transform.DOLocalMoveX(-endPosition, Time, false).SetEase (Ease.Linear).OnComplete (() => {
			MovementStart ();
		});
	}
}
