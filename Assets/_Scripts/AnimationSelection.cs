using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationSelection : MonoBehaviour {

	public Animator charecter;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void SelectAnimation(string name) {
		charecter.Play (name);
	}

}
