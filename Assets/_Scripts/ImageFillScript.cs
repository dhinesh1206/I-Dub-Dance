using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class ImageFillScript : MonoBehaviour {

//	public GameObject ObjectTomove;
//	public float fromPoint;
//	public float endPoint;
//	public float traveltime;
	public Image fillImage;

	public List<EndPoints> endPoints;

	private EndPoints tempEndpoint;

	// Use this for initialization
	void Start () {
		//fromPoint = transform.localPosition.x;
		Move ();
	}
	
	// Update is called once per frame
	void Update () {
		float fulldistance = Vector3.Distance (tempEndpoint.EndPoint, tempEndpoint.startpoint);
		float remainingDistance = Vector3.Distance(tempEndpoint.EndPoint,transform.localPosition);
		float progress = 1 - (remainingDistance/ fulldistance);
		tempEndpoint.fillImage.fillAmount = progress;
	}

	void Move() {
		StartCoroutine (Movement());

		//transform.DOLocalMoveX (endPoint, traveltime, false).SetEase(Ease.Linear);
	}

	public IEnumerator Movement() {
		foreach (var obj in endPoints) {
			tempEndpoint = obj;
			transform.DOLocalMove (obj.EndPoint, obj.endTime, false).SetEase(Ease.Linear);
			yield return new WaitForSeconds (obj.endTime);
		}

	}


}

[System.Serializable]
public class EndPoints {
	public Vector3 startpoint;
	public Vector3 EndPoint;
	public Image fillImage;
	public float endTime;
}
