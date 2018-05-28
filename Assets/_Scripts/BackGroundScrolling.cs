using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackGroundScrolling : MonoBehaviour {

	public float scrollSpeed;
	private Vector2 savedOffset;

	void Start () {
		savedOffset = this.gameObject.GetComponent<Renderer>().material.GetTextureOffset ("_MainTex");
	}

	void Update () {
		float x = Mathf.Repeat (Time.time * scrollSpeed, 1);
		Vector2 offset = new Vector2 (x, savedOffset.y);
		this.gameObject.GetComponent<Renderer>().material.SetTextureOffset ("_MainTex", offset);
	}

	void OnDisable () {
		this.gameObject.GetComponent<Renderer>().material.SetTextureOffset ("_MainTex", savedOffset);
	}
}
