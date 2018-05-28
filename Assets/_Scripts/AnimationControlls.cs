using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
using DG.Tweening;

public class AnimationControlls : MonoBehaviour {

	public static AnimationControlls instance;

	public Animator charAnimation;
	public AudioSource audio;
	public AudioClip audioTest;
	public objectsinHand objectinHand;
	public GameObject effectImage;
	public List<AudioNames> audioname;
	public float audiolenght;
	public string Keyword = "TakeL";
	public string audioKey;
	public string EndAnimationKey = null;
	public List<EffectName> effects;

	public Animator effectcharAnimator;
	public Transform[] instantiationPoint;
	public Transform initialCharPoint;


	public GameObject effectSelectionScreen;
	public GameObject effectSelectionCamera;
	public GameObject arCamera;
	public GameObject mainMenu;

	public List<FallObjectsIming> fallingObjects;

	public FaceExpressions expTime;
	public Renderer mainBody;

	public GameObject instantiatedImage;
	public int charecterSelectionIndex;
	public Transform[] fallingObjectsParent;
	public GameObject fallingObjectsEndParent;

	private GameObject[] temp;

	void OnEnable() 
	{
		RecScript.RecordPlayer += On_play;
		CharecterActionManager.PlayAnimation += On_play;
	}

	void Awake () {
		instance = this;
	}

	void OnDisable() 
	{
		RecScript.RecordPlayer -= On_play;
		CharecterActionManager.PlayAnimation -= On_play;
	}

	void Start () {
		
	}

	void Update () {
		
	}

	void On_play() {
		StopAllCoroutines ();
		charAnimation.Play (Keyword);
		if (instantiatedImage) {
			Destroy (instantiatedImage);
		}
		audio.clip = audioTest;
		audiolenght = audioTest.length;
		audio.Play ();
		StartCoroutine (stopFirstanimation ());
		StartCoroutine (ActivateObjects (objectinHand));
	}

	public IEnumerator ActivateObjects (objectsinHand Keys) {
		foreach (var obj in Keys.objectinHand) {
			yield return new WaitForSeconds (obj.startTime);
			foreach (var objs in Keys.objectList) {
				foreach (GameObject pickup in objs.objectsToactivate) {
					if (pickup.name == obj.objectName) {
						pickup.SetActive (true);
						StartCoroutine (DisableObjects (obj.endTime, pickup));
					}
				}
			}
		}
	}

	public IEnumerator DisableObjects( float time, GameObject obj ) {
		yield return new WaitForSeconds (time);
		obj.SetActive (false);
	}

	public IEnumerator InstantiateFallingObjects(GameObject[] Obj, float timing) {
		foreach (Transform position in fallingObjectsParent) {
			GameObject clone = Instantiate (Obj [0], position,false);
			clone.transform.parent = null;
			StartCoroutine (DestroyObject (clone));
		}
		yield return new WaitForSeconds (timing);
		StartCoroutine (InstantiateFallingObjects (Obj,timing));
	}

	public IEnumerator DestroyObject(GameObject obj) {
		yield return new WaitForSeconds (3f);
		Destroy (obj);
	}

	public IEnumerator MovebgFallObjects(GameObject clone, Transform pos) {
		yield return new WaitForSeconds (0.2f);
		clone.transform.DOLocalMove (new Vector3(pos.localPosition.x,-pos.localPosition.y,pos.localPosition.z), 0.5f, false);
		clone.transform.parent = null;
	}

	public IEnumerator emmotionStart(FaceExpressions Key) {
		foreach (var obj in Key.expressionTimes) {
			Material[] mat = mainBody.materials;
			mat [2] = obj.faceAction[charecterSelectionIndex];
			mainBody.materials = mat;
			yield return new WaitForSeconds (obj.time);
		}
	}

	public IEnumerator stopFirstanimation() {
		yield return new WaitForSeconds (audiolenght);
		StartCoroutine (EndActions ());
	}

	public IEnumerator EndActions()
	{
		print (EndAnimationKey);
		if (EndAnimationKey != "") {
			charAnimation.Play (EndAnimationKey);
			StartCoroutine (emmotionStart(expTime));
			yield return new WaitForSeconds (1f);
			instantiatedImage = Instantiate (effectImage, instantiationPoint[UnityEngine.Random.Range(0,instantiationPoint.Length)], false);
			StartCoroutine (StopANimation(3f));
		} else {
			print ("Skipped");
			StartCoroutine(StopANimation (2f));
		}
	}

	public IEnumerator StopANimation(float time) 
	{
		yield return new WaitForSeconds (time);
		RecScript.instance.StopRecord ();
	}

	public void Animation(string Key) {
		Keyword = Key;
		audioKey = Key;
	}

	public void EndActoionSelection(string actionName)
	{
		EndAnimationKey = actionName;
	}

	public void EffectSelected(int Index) 
	{
		StopAllCoroutines ();
		EndAnimationKey = effects [Index].animationname;
		effectcharAnimator.Play (effects [Index].animationname);
		StartCoroutine (effectInstantiation (effectcharAnimator.GetCurrentAnimatorClipInfo (0).Length));
	}

	public IEnumerator effectInstantiation(float time)
	{
		yield return new WaitForSeconds (time);
	}

	public void StartAR() 
	{
		arCamera.SetActive (true);
		mainMenu.SetActive (false);
		effectSelectionScreen.SetActive(false);
		effectSelectionCamera.SetActive (false);	
	}

}

[System.Serializable]
public class AudioNames {
	
	public string Audioname;
	public AudioClip audio;
}


[System.Serializable]
public class EffectName {
	public string animationname;
	public GameObject partical;
}