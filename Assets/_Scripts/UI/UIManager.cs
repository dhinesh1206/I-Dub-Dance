using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Apple.ReplayKit;
using UnityEngine.Playables;
using DG.Tweening;

public class UIManager : MonoBehaviour {
	public RectTransform[] SideBtns;
	public float sideBtnAnimTime;
	bool isExpanded;
	public GameObject arCamera;
	public GameObject bg;
	public List<ListScreens> screens;

	void Start () {
		StartCoroutine (FakeRecord ());
	}

	IEnumerator FakeRecord(){
		ReplayKit.StartRecording (true);
		while (!ReplayKit.isRecording)
			yield return null;
		yield return new WaitForSeconds(2);
		ReplayKit.StopRecording ();
		while (!ReplayKit.recordingAvailable)
			yield return null;
		ReplayKit.Discard ();
	}

	void Update () {
		
	}

	public void OnSideBtnClick(){
		if (isExpanded)
			StartCoroutine(ShrinkSide ());
		else
			StartCoroutine(ExpandSide ());
	}

	IEnumerator ExpandSide(){
		isExpanded = true;
		for (int i = 0; i < SideBtns.Length; i++) {
			SideBtns[i].DOScale (Vector3.one, sideBtnAnimTime).SetEase (Ease.OutBack);
			yield return new WaitForSeconds (sideBtnAnimTime/2);
		}
	}

	IEnumerator ShrinkSide(){
		isExpanded = false;
		for (int i = SideBtns.Length; i > 0; i--) {
			SideBtns[i-1].DOScale (Vector3.zero, sideBtnAnimTime).SetEase (Ease.Linear);
			yield return new WaitForSeconds (sideBtnAnimTime/2);
		}
	}

	public void OnscreenSelection(int ButtonNumber) {
		foreach (var screen in screens) {
			if (screen.buttonNumber == ButtonNumber) {
				ExpandScreen (ButtonNumber, screen.ScreenTarget);
			} else {
				screen.ScreenTarget.SetActive (false);
			}
		}
	}

	public void ExpandScreen(int buttonnumber,GameObject Target) {
		SideBtns[buttonnumber].DOScale(new Vector3(6,6,6), 0.5f).SetEase(Ease.Linear).OnComplete(() => {
			Target.SetActive(true);
			bg.SetActive(false);
			arCamera.SetActive(false);
			InstantShrink();
		});
	}

	public void InstantShrink() {
		isExpanded = false;
		for (int i = SideBtns.Length; i > 0; i--) {
			SideBtns[i-1].DOScale (Vector3.zero,0).SetEase (Ease.Linear);
	     	}
    	}
   }

[System.Serializable]

public class ListScreens{
	public int buttonNumber;
	public GameObject ScreenTarget;
}