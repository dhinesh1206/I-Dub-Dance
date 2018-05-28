using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class BGDoodleScaling : MonoBehaviour {

	public Transform[] spawnPoints;
	private Vector3 TempTransform;
	// Use this for initialization
	void Start () {
		Animation ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
		
	void Animation() {
		int TempNumber = Random.Range (0, 2);
		if(TempNumber == 0) {
			transform.DOScale (new Vector3 (2, 2, 2), 1).SetEase(Ease.Linear).OnComplete(() => {
				StartCoroutine(fadeOut());
			});
		} else if (TempNumber == 1) {
			transform.DOScale (new Vector3 (1, 1, 1), 1).SetEase(Ease.Linear).OnComplete(() => {
				transform.DOLocalRotate (new Vector3(0,0,20), 1f).SetEase(Ease.Linear).OnComplete(() => {
					transform.DOLocalRotate (new Vector3(0,0,-20), 1f).SetEase(Ease.Linear).OnComplete(() => {
						transform.DOLocalRotate (new Vector3(0,0,20), 1f).SetEase(Ease.Linear).OnComplete(() => {
							transform.DOLocalRotate (new Vector3(0,0,-20), 1f).SetEase(Ease.Linear).OnComplete(() => {
								transform.DOLocalRotate (new Vector3(0,0,20), 1f).SetEase(Ease.Linear).OnComplete(() => {
									transform.DOLocalRotate (new Vector3(0,0,-20), 1f).SetEase(Ease.Linear).OnComplete(() => {
										transform.DOLocalRotate (new Vector3(0,0,20), 1f).SetEase(Ease.Linear).OnComplete(() => {
											transform.DOLocalRotate (new Vector3(0,0,-20), 1f).SetEase(Ease.Linear).OnComplete(() => {
												transform.DOLocalRotate (new Vector3(0,0,0), 0.2f).SetEase(Ease.Linear).OnComplete(() => {
													transform.DOScale (new Vector3 (0, 0, 0), 1).SetEase(Ease.InOutSine).OnComplete(() => {
														transform.localPosition = spawnPoints[Random.Range(0,spawnPoints.Length)].localPosition;
														Animation();
													});
												});
											});
										});
									});
								});
							});
						});
					});
				});
			});
		}
	}

	public IEnumerator fadeOut() {
		yield return new WaitForSeconds (0.1f);
		transform.DOScale (new Vector3 (1, 1, 1), 1).SetEase (Ease.InOutSine).OnComplete (() => {
			transform.DOScale (new Vector3 (2, 2, 2), 1).SetEase (Ease.InOutSine).OnComplete (() => {
				transform.DOScale (new Vector3 (1, 1, 1), 1).SetEase (Ease.InOutSine).OnComplete (() => {
					transform.DOScale (new Vector3 (0, 0, 0), 1).SetEase (Ease.InOutSine).OnComplete (() => {
				transform.localPosition = spawnPoints [Random.Range (0, spawnPoints.Length)].localPosition;
				Animation ();
					});
				});
			});
		});
	}


	public IEnumerator Rotateback() {
		yield return new WaitForSeconds (0.1f);
	//	print (Mathf.FloorToInt(transform.localEulerAngles.z));
		print(transform.rotation.z.ToString());
		if (transform.rotation.z.ToString() == "0.1736482") {
			TempTransform = new Vector3 (0, 0, -20);
		} else  {
			TempTransform = new Vector3 (0, 0, 20);
		}
		transform.DOLocalRotate (TempTransform, 1f).SetEase(Ease.Linear).OnComplete(() => {
			StartCoroutine(Rotateback());
		});;


	}

	public IEnumerator stopCoroutie() {
		yield return new WaitForSeconds (5f);
		transform.DOScale (new Vector3 (0, 0, 0), 1).SetEase(Ease.Linear).OnComplete(() => {
			transform.localPosition = spawnPoints[Random.Range(0,spawnPoints.Length)].localPosition;
			StopAllCoroutines();
			Animation();
		});
	}
}
