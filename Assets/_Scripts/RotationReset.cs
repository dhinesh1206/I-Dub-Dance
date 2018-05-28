using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationReset : MonoBehaviour {

	public GameObject arcam;



	RaycastHit Hit;

	void Start () {
		transform.LookAt (arcam.transform);
		transform.rotation = Quaternion.Euler (0, transform.rotation.y, 0);
	}

	void Update () {
			
	}


}
