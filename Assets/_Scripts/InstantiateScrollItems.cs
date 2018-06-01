using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;


public class InstantiateScrollItems : MonoBehaviour {

	public Animator animationCharecter;
	public Transform animationbuttinParent;
	public Transform effectbuttonParent;
	public Transform AnimationListView;
	public GameObject bgImahe;
	public GameObject[] smallcharImagesBg;
	public Transform logoinitiationParent;
	public Transform titlebuttonParent;
	public Camera renderTextureCamera;
	public Renderer boyBody;
	public GameObject button;
	public GameObject effectButton;
	public GameObject headingButton;
	public GameObject renderTexturePrefab;
	public string[] buttonNames;
	public bool effects;
	public static InstantiateScrollItems instance;
	public GameObject effectinstantiated;
	public List<ButtonNames> animationNames;
	public List<EffectsImage> effectImage;
	public List<AnimationTitle> animationTitle;
	public AudioSource bodyAudio;
	public Ease easeType;
	public Animator renderTexturemodalAnimator;
	public GameObject SelectOption;
    public ExpressionHolder expressionHolder;

	public int charecterSelectionindex;

	void Start () 
	{
		InstantiateCalled ();
	}

	public void AnimateList(List<ButtonNames> animationName, Color color, Color bg, Color smallimage) {
		bodyAudio.Stop ();
		StartCoroutine(StopAnimation(0.1f));
		StartCoroutine (InstantiateAnimationList (animationName, color));
		bgImahe.GetComponent<Image> ().color = bg;
		foreach(GameObject image in smallcharImagesBg) {
			image.GetComponent<Image> ().color = smallimage;
		}
	}

	public IEnumerator InstantiateAnimationList(List<ButtonNames> animationName, Color color) {
		foreach (Transform obj in animationbuttinParent) {
			Destroy (obj.gameObject);
		}
		AnimationListView.DOLocalMoveX (200, 0.25f, false).SetEase (Ease.Linear).OnComplete (() => {
			AnimationListView.GetComponent<Image>().color = color;
			AnimationListView.DOLocalMoveX (-150, 0.25f, false).SetEase (Ease.Linear);
		}
		);
					
		foreach (var name in animationName) {
			yield return new WaitForSeconds (0.5f);
			GameObject obj = Instantiate (renderTexturePrefab, animationbuttinParent, false);
			obj.transform.DOScale (Vector3.one, 0.5f).SetEase(easeType);
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
			obj.GetComponentInChildren<ButtonAnimator> ().audios = name.audios;
			obj.GetComponentInChildren<ButtonAnimator> ().DanceMove = name.animationKey;
			obj.GetComponentInChildren<ButtonAnimator> ().expTime = name.expTime;
		}
	}

	public void InstantiateCalled() {
		if (effects == false) {
			foreach(var objs in animationTitle) {
				GameObject obj = Instantiate(headingButton, titlebuttonParent, false);
				obj.transform.localScale = Vector3.one;
				obj.GetComponent<Image> ().sprite = objs.Icon;
				obj.GetComponent<TitleButtonController> ().bgColor = objs.bgColorCode;
				obj.GetComponent<TitleButtonController> ().smallIconsBgColor = objs.smallImagesColor;
				obj.GetComponent<TitleButtonController> ().animationNames = objs.Animations;
				obj.GetComponent<TitleButtonController> ().TitleParent = titlebuttonParent;
				obj.GetComponent<TitleButtonController> ().color = objs.colorCode;
			}
		} else 
		{
			foreach (var effect in effectImage) {
				GameObject obj = Instantiate(effectButton, effectbuttonParent, false);
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

	public void DestroyObjects() {
		foreach (Transform obj in titlebuttonParent) {
			Destroy (obj.gameObject);
		}
	}

	void Awake() 
	{
		instance = this;

	}

	void Update() {
		//charecterSelectionindex = AnimationControlls.instance.charecterSelectionIndex;

	}

	public void AnimationButtonClicked() 
	{
		print ("Called");
	}


	public void AnimationCalled(string key, AudioClip Audio, FaceExpressions Test  )
	{
		SelectOption.SetActive (true);
		if (effectinstantiated) {
			Destroy (effectinstantiated);
		}
		bodyAudio.clip = Audio;
		bodyAudio.Play ();
		StartCoroutine (emmotionStart (Test));
		StartCoroutine (StopAnimation (Audio.length));
		animationCharecter.Play (key);


	}

	public IEnumerator StopAnimation(float time) {
		yield return new WaitForSeconds (time);
		animationCharecter.Play ("Idle");
	}

	public IEnumerator emmotionStart(FaceExpressions Key) {
		foreach (var obj in Key.expressionTimes) {
					yield return new WaitForSeconds (obj.time);
					Material[] mat = boyBody.materials;
					//mat [2] = obj.faceAction[charecterSelectionindex];
                    foreach(var objects  in expressionHolder.expressions) {
                        if(objects.expressionName == obj.expressionName) {
                          mat[2] = objects.actions[charecterSelectionindex];
                        }
                    }
					boyBody.materials = mat;
				}
	}

	public void EffectAnimationcalled(string Key, GameObject logo, FaceExpressions expTime) 
	{
		StopAllCoroutines ();
		if (effectinstantiated) {
			Destroy (effectinstantiated);
		}
		animationCharecter.Play(Key);
		StartCoroutine (emmotionStart (expTime));
		StartCoroutine (starteffect (animationCharecter.GetCurrentAnimatorClipInfo(0).Length,logo));
	}
	public IEnumerator starteffect(float time, GameObject logo)
	{
		yield return new WaitForSeconds (0.5f);
		effectinstantiated = Instantiate (logo, logoinitiationParent, false);
	}
}


[System.Serializable]
public class ButtonNames {
	//public Sprite[] icon;
	public string renderTextureAnimationKey;
	public string animationKey;
	public AudioClip audios;
	public Sprite hutBG;
	public string IconText;
	public Font IconTextfontFont;
	public Color bgColor;
	//public float[] fallingObjectsTiming;
	public Color iconTextBgColor;
	public Color iconTextColor;

	public FaceExpressions expTime;
	//public Sprite[] fallingobjects;
	//public List<FallObjectsIming> fallinObjects;
	public objectsinHand objectsinHand;
	public GameObject borderscreen;
}

[System.Serializable]
public class ObjectsInHand {
	public float startTime;
	public float endTime;
	public GameObject objectToactivate;
	public string objectName;
}

[System.Serializable]
public class ObjectsList {
	public List<GameObject> objectsToactivate;
}

[System.Serializable]
public class objectsinHand {
	public List<ObjectsList> objectList;
	public List<ObjectsInHand> objectinHand;
}

[System.Serializable]
public class EffectsImage {
	public string animationKey;
	public Sprite icon;
	public GameObject effectGaf;
	public FaceExpressions expTime;
}

[System.Serializable]

public class FaceExpressions {
	public List<expressionTime> expressionTimes;
}

[System.Serializable]
public class expressionTime {
	public float time;
    public string expressionName;
	//public List<Material> faceAction;
}

[System.Serializable]
public class ExpressionsNames {
    public string expressionName;
    public List<Material> actions;
}

[System.Serializable]
public class AnimationTitle {
	public Sprite Icon;
	public Color colorCode;
	public Color bgColorCode;
	public Color smallImagesColor;
	public List<ButtonNames> Animations;

}

[System.Serializable]
public class TestScroll {
	public Transform animationbuttinParent;
	public Transform effectbuttonParent;
	public Transform AnimationListView;
	public GameObject[] smallcharImagesBg;
	public Transform titlebuttonParent;
	public GameObject screenName;
	public bool effects;
	public static InstantiateScrollItems instance;
	public GameObject effectinstantiated;
	public List<EffectsImage> effectImage;
	public List<AnimationTitle> animationTitle;
    public Toggle borderToggle;
	public Ease easeType;
	public GameObject SelectOption;
}