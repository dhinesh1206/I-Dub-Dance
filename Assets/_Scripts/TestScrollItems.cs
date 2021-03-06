﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class TestScrollItems : MonoBehaviour {

	public List<TestScroll> ScrollItems;
	public Animator animationCharecter;
	public Transform logoinitiationParent;
	public Camera renderTextureCamera;
	public Renderer boyBody;
	public bool effects;
	public int ThemeIndex;
	public AudioSource bodyAudio;
	public GameObject effectinstantiated;
	public GameObject effectButton;
	public GameObject headingButton;
	public GameObject renderTexturePrefab;
	public int charecterSelectionindex;
	public Animator renderTexturemodalAnimator;   
    public Expressions expressions;
	public static TestScrollItems instance;
    AudioSource source;
	// Use this for initialization
	void Start () {
        
	}
	
	// Update is called once per frame
	void Update () 
    {
        
    }

	public void DestroyObjects() {
		foreach (Transform obj in ScrollItems[ThemeIndex].titlebuttonParent) {
			Destroy (obj.gameObject);
		}
	}

	public void DifferentTheme() {
		foreach (Transform obj in ScrollItems[ThemeIndex].titlebuttonParent) {
			Destroy (obj.gameObject);
		}
		InstantiateCalled ();
	}

	public void DestroyEffectImages() {
		foreach (Transform obj in ScrollItems[ThemeIndex].effectbuttonParent) {
			Destroy (obj.gameObject);
		}
		Destroy (ScrollItems[ThemeIndex].effectinstantiated);
		InstantiateCalled ();
	}

	void Awake() 
	{
		instance = this;
	}


	public void CloseSideBar() {
		
		foreach (Transform obj in ScrollItems[ThemeIndex].animationbuttinParent) {
			Destroy (obj.gameObject);
		}
		ScrollItems [ThemeIndex].AnimationListView.DOLocalMoveX (200, 0.25f, false).SetEase (Ease.Linear);
	}

	public void AnimateList(List<ButtonNames> animationName, Color color, Color bg, Color smallimage) {
		bodyAudio.Stop ();
		StartCoroutine(StopAnimation(0.1f));
		StartCoroutine (InstantiateAnimationList (animationName, color));
		//ScrollItems[ThemeIndex].bgImahe.GetComponent<Image> ().color = bg;
		foreach(GameObject image in ScrollItems[ThemeIndex].smallcharImagesBg) {
			//image.GetComponent<Image> ().color = smallimage;
		}
	}


	public IEnumerator InstantiateAnimationList(List<ButtonNames> animationName, Color color) {
		foreach (Transform obj in ScrollItems[ThemeIndex].animationbuttinParent) {
			Destroy (obj.gameObject);
		}
		ScrollItems[ThemeIndex].AnimationListView.DOLocalMoveX (200, 0.25f, false).SetEase (Ease.Linear).OnComplete (() => {
			ScrollItems[ThemeIndex].AnimationListView.GetComponent<Image>().color = color;
			ScrollItems[ThemeIndex].AnimationListView.DOLocalMoveX (-150, 0.5f, false).SetEase (Ease.Linear);
		}
		);

		foreach (var name in animationName) {
			yield return new WaitForSeconds (0.5f);
			GameObject obj = Instantiate (renderTexturePrefab, ScrollItems[ThemeIndex].animationbuttinParent, false);
			obj.transform.DOScale (Vector3.one, 0.5f).SetEase(ScrollItems[ThemeIndex].easeType);
			renderTexturemodalAnimator.Play(name.renderTextureAnimationKey);
			RenderTexture rt = new RenderTexture(256, 256, 16, RenderTextureFormat.ARGB32);
			rt.Create();
			renderTextureCamera.targetTexture = rt;
			obj.GetComponentInChildren<ButtonAnimator> ().RenderImage = rt;
			GameObject circ = obj.transform.GetChild(0).gameObject;
			circ.GetComponent<Image> ().color = name.bgColor;
			obj.GetComponentInChildren<RawImage> ().texture = rt;
			//obj.GetComponentInChildren<ButtonAnimator> ().fallingObjects = name.fallinObjects;
			obj.transform.GetChild (1).GetComponent<Image> ().sprite = name.hutBG;
			obj.transform.GetChild (3).GetComponent<Image> ().color = name.iconTextBgColor;
			obj.transform.GetChild (4).GetComponent<Text> ().text = name.IconText;
			obj.transform.GetChild (4).GetComponent<Text> ().font = name.IconTextfontFont;
			obj.transform.GetChild (4).GetComponent<Text> ().color = name.iconTextColor;
			if(name.borderscreen)
			obj.GetComponentInChildren<ButtonAnimator> ().screenToActivate = name.borderscreen;
			obj.GetComponentInChildren<ButtonAnimator> ().audios = name.audios;
			//obj.GetComponentInChildren<ButtonAnimator> ().objs = name.objectsinHand;
			obj.GetComponentInChildren<ButtonAnimator> ().DanceMove = name.animationKey;
			obj.GetComponentInChildren<ButtonAnimator> ().expTime = name.expTime;
		}
	}

	public IEnumerator StopAnimation(float time) 
    {
		yield return new WaitForSeconds (time);
		animationCharecter.Play ("Idle");
	}

	public void InstantiateCalled() {
		if (effects == false) {
				AnimateList (ScrollItems[ThemeIndex].animationTitle[0].Animations,ScrollItems[ThemeIndex].animationTitle[0].colorCode,ScrollItems[ThemeIndex].animationTitle[0].bgColorCode,ScrollItems[ThemeIndex].animationTitle[0].smallImagesColor);
		} else 
		{
			foreach (var effect in ScrollItems[ThemeIndex].effectImage) {
				GameObject obj = Instantiate(effectButton, ScrollItems[ThemeIndex].effectbuttonParent, false);
				obj.transform.localScale = Vector3.one;
				obj.GetComponent<Image>().sprite = effect.icon;
				obj.GetComponent<ButtonAnimator> ().effect = effect.animationKey;
				obj.GetComponent<ButtonAnimator> ().effectGaf = effect.effectGaf;
				obj.GetComponent<ButtonAnimator> ().expTime = effect.expTime;
			}

		}
		int count = transform.childCount;
		var theBarRectTransform = transform as RectTransform;
		if (count > 5) {
			theBarRectTransform.sizeDelta = new Vector2 (theBarRectTransform.sizeDelta.x, count * 300);
		}
	}

	public void SkilPressed() {
		AnimationControlls.instance.EndAnimationKey = null;
		AnimationControlls.instance.effectImage = null;
		AnimationControlls.instance.expTime = null;
	}

	public void AnimationCalled(string key, AudioClip Audio, FaceExpressions Test , objectsinHand objs )
	{
		StopAllCoroutines ();
		ScrollItems[ThemeIndex].SelectOption.SetActive (true);
		if (effectinstantiated) {
			Destroy (effectinstantiated);
		}
		bodyAudio.clip = Audio;
		bodyAudio.Play ();
		emmotionStart (Test);
		StartCoroutine (StopAnimation (Audio.length));
		animationCharecter.Play (key);
		StartCoroutine (ActivateObjects (objs));
		//animationNameText.text = animationCharecter.GetCurrentAnimatorClipInfo(0)[0].clip.name;
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

	public void emmotionStart(FaceExpressions Key)
    {
		int index = 0;
        foreach (var obj in expressions.expressionPoints)
        {
			if (obj.dancename == Key.expressionName) {
				//foreach (var ob in obj.expressionTimes) {
				//		foreach (var time in expressions.expressions) {
				//				if (time.name == ob.name) {
				//					print (index);
				//					index++;
				//
				//					Material[] mat = boyBody.materials;
				//					mat [1] = time.faceactions [charecterSelectionindex];
				//					boyBody.materials = mat;
				//					yield return new WaitForSecondsRealtime (ob.time * 4.166f);
				//				}
				//			}
				//		}
				//		print ("Finished");
				//	}
				StartCoroutine (PlayReactions (obj));
			}
        }
    }



    Material[] SelectMaterial(string MatName)
    {
        foreach (var item in expressions.expressions)
        {
            if (item.name==MatName)
            {
                return item.faceactions.ToArray();
            }
        }
        return null;
    }

	IEnumerator ReactionsPlay(Material[] mat , float time)
	{
		yield return new WaitForSeconds (time/24);
		boyBody.materials = mat;
	}

    IEnumerator PlayReactions(expressionKeys keys)
    {
    //    for (int i = 0; i < keys.expressionTimes.Count; i++)
    //    {
	//		
	//		print (i);
    //        float previousTime = i > 0 ? (keys.expressionTimes[i].time - keys.expressionTimes[i - 1].time) : keys.expressionTimes[i].time;
    //        boyBody.materials = SelectMaterial(keys.expressionTimes[i].name);
	//		print (previousTime);
	//		yield return new WaitForSeconds(previousTime /24);
    //}
		yield return new WaitForSeconds(0f);
		foreach (var obj in keys.expressionTimes) {
			StartCoroutine (ReactionsPlay(SelectMaterial(obj.name),obj.time));
		}
	}

	public void EffectAnimationcalled(string Key, GameObject logo, FaceExpressions expTime) 
	{
		StopAllCoroutines ();
		if (effectinstantiated) {
			Destroy (effectinstantiated);
		}
		animationCharecter.Play(Key);
		emmotionStart (expTime);
		StartCoroutine (starteffect (animationCharecter.GetCurrentAnimatorClipInfo(0).Length,logo));
	}
	public IEnumerator starteffect(float time, GameObject logo)
	{
		yield return new WaitForSeconds (0.5f);
		effectinstantiated = Instantiate (logo, logoinitiationParent, false);
	}

	public void stopallcoroutine()
	{
		StopAllCoroutines ();
	}
}
