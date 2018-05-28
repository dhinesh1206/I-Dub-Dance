using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class ObjectsTest : MonoBehaviour {


	public List<objetsTobeShown> objects;
 	// Use this for initialization
	void Start () {
		foreach (var obj in objects) {
			StartCoroutine (startAnimation (obj));
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	IEnumerator startAnimation(objetsTobeShown obj) {
		yield return new WaitForSeconds (obj.startTime);
		foreach (GameObject obz in obj.objects) {
			obz.SetActive (true);
			obz.transform.localScale = Vector3.zero;
			obz.transform.DOScale (obj.scaleValue, obj.scaleTime).SetEase (Ease.Linear).OnComplete (() => {
				StartCoroutine(EndAnimation(obz,obj.endTime, obj.scaleTime));
			});
		}
	}

	IEnumerator EndAnimation(GameObject obj, float timez, float scaletime ) {
		yield return new WaitForSeconds (timez);
		obj.transform.DOScale (Vector3.zero, scaletime).SetEase (Ease.Linear);
	}

}


[System.Serializable]

public class objetsTobeShown 
{
	public GameObject[] objects;
	public float startTime;
	public float endTime;
	public float scaleTime;
	public Vector3 scaleValue;
}