using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class ButtonAnimator : MonoBehaviour {
	public string DanceMove;
	public string effect;
	public GameObject effectGaf;
	public FaceExpressions expTime;
	public AudioClip audios;
	public RenderTexture RenderImage;
	public Ease easeType;
	public objectsinHand objs;
	public GameObject screenToActivate;

	void Start () {
		
	}

	void Update () {
		
	}

	public void OnClick(){
		//InstantiateScrollItems.instance.AnimationCalled (DanceMove, audios, expTime);
		TestScrollItems.instance.AnimationCalled (DanceMove, audios, expTime, objs);
		RecScript.instance.screenName = screenToActivate;
		AnimationControlls.instance.Keyword = DanceMove;
		AnimationControlls.instance.audioTest = audios;
		AnimationControlls.instance.expTime = expTime;
		AnimationControlls.instance.objectinHand = objs;
	}

	public void OnEffectClick() {
		//InstantiateScrollItems.instance.EffectAnimationcalled (effect,effectGaf,expTime);
		TestScrollItems.instance.EffectAnimationcalled (effect,effectGaf,expTime);
		AnimationControlls.instance.EndAnimationKey = effect;
		AnimationControlls.instance.effectImage = effectGaf;
		AnimationControlls.instance.expTime = expTime;
	}
}
