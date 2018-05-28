using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PlayUiAnimation : MonoBehaviour {

	//public GameObject[] charecterAnimation;

	public List<UIAnimationList> animationList;
	private GameObject currentCharecter;

	public GameObject animationSelectionScreen;
	public GameObject effectSelectionScreen;
	public GameObject effectSelectionCamera;
	RaycastHit Hit;

	public GameObject showoffPartical;
	public GameObject arCamera;
	public GameObject mainMenu;

	public Camera Uicamera;

	void Start () {
		foreach (var objectz in animationList) {
			objectz.character.GetComponent<Animator> ().Play (objectz.animationName);
		}
	}

	void OnEnable() {
		//arCamera.SetActive (false);
	}

	void OnDisable() {
		//arCamera.SetActive (true);
	}

//	void Update () {
//		Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
//		if (Input.GetMouseButton (0)) {
//			if (Physics.Raycast (ray, out Hit)) {
//				if (Physics.Raycast (ray, out Hit)) {
//					foreach (var Uiobjects in animationList) {
//						if (Hit.transform.gameObject == Uiobjects.character) {
//							Uiobjects.character.GetComponent<Animator> ().Play (Uiobjects.animationName);
//						} else {
//							Uiobjects.character.GetComponent<Animator> ().Play ("Idle");
//						}
//					}
//				}
//			}
//		}
//	}
	void Update () {
		if (Input.GetMouseButton (0)) {
			Ray ray = Uicamera.ScreenPointToRay (Input.mousePosition);
			if (Physics.Raycast (ray, out Hit)) {
				if (Physics.Raycast (ray, out Hit)) {
					foreach (var Uiobjects in animationList) {
						if (Hit.transform.gameObject == Uiobjects.character) {
							Uicamera.orthographic = false;
							Uicamera.transform.localRotation = Quaternion.Euler (20, 0, 0);
							Uicamera.transform.DOLocalMove (new Vector3 (Hit.transform.position.x , Hit.transform.position.y+1, Hit.transform.position.z - 1.25f), 1.5f, false).OnComplete(() => {
								AnimationControlls.instance.Keyword = Uiobjects.animationName;
								effectSelectionScreen.SetActive(true);
								effectSelectionCamera.SetActive (true);
								animationSelectionScreen.SetActive(false);
							});
						} else {
							Uiobjects.character.GetComponent<Animator> ().Play ("Idle");
						}
					}
				}
			}
		}
	}

	public void StartAr() {
		StartCoroutine (StartAnimation ());
	}

	IEnumerator StartAnimation() 
	{
		yield return new WaitForSeconds (0.2f);
		arCamera.SetActive (true);
		mainMenu.SetActive (false);
		effectSelectionScreen.SetActive(false);
		effectSelectionCamera.SetActive (false);


	}

	public void HitOnplay() {
		
	}
}

[System.Serializable]
public class UIAnimationList{
	public GameObject character;
	public string animationName;
}
