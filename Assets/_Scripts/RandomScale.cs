using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class RandomScale : MonoBehaviour {

	public Sprite[] objects; 
	private float speed;
	private float speed2;
	void Start () {
		Animate();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void Animate() {
		speed = Random.Range (0.5f,2);
		speed2 = speed * 2;
		gameObject.GetComponent<Image>().sprite = objects[Random.Range(0,objects.Length)];
		transform.DOScale (Vector3.one, speed).SetEase (Ease.InSine).OnComplete (() => {
			transform.DOScale (Vector3.zero, speed2).SetEase (Ease.InSine).OnComplete (() => {
				Animate();
			});
		});
	}
}
