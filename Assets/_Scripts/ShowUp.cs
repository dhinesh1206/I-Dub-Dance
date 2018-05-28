using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ShowUp : MonoBehaviour {

	// Use this for initialization
	void Start () {
		transform.DOLocalMoveY (1, Random.Range (0,4), false);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
