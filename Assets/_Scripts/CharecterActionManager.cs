using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharecterActionManager : MonoBehaviour {


	public delegate void AnimationAction ();
	public static event AnimationAction PlayAnimation;
	// Use this for initialization

	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void Play() {
		PlayAnimation ();
	}
}
