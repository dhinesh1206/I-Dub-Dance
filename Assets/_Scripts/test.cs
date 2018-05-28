using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test : MonoBehaviour {

	// Use this for initialization
	void Start () {
		transform.LookAt (Camera.main.transform);
		transform.localRotation = Quaternion.Euler (0, transform.localRotation.y+180, 0);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
