using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class BusAttachedObjectsAnimation : MonoBehaviour {

	public List<AttachmentImages> imagesAttached;
	public List<AttachmentImageAnimation> imagesAnimation;

	// Use this for initialization
	void Start () {
		StartAnimation ();	
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void Startz() {
	}

	public void StartAnimation () {
		foreach (var obj in imagesAnimation) {
			StartCoroutine (StartAnimating(obj));
		}
	}

	public IEnumerator StartAnimating(AttachmentImageAnimation obj) {
		yield return new WaitForSeconds (obj.startTime);
		foreach (var objects in imagesAttached) {
			if (objects.name == obj.objectName) {
				//objects.parentObjects.transform.localScale = Vector3.zero;
				//objects.parentObjects.SetActive (true);
				foreach(GameObject objs in objects.parentObjects ) {
					objs.transform.localScale = Vector3.zero;
					objs.SetActive (true);
					StartCoroutine (HideObjects (obj.endTime,objs));
					StartCoroutine (parentimageAnimation(objects.parentOjectImages, objs, objects.spriteSpeed,objects.spriteSizeReducer));
					foreach (childObjectsAnimation objsz in objects.childObject) {
						StartCoroutine (enableChildObjects (objsz));
					}

					foreach (var effect in obj.animationtypes) {
						StartCoroutine (Startsmallactionsparent(effect,objs));
					}
				}

			}
		}
	}

	public IEnumerator HideObjects(float time, GameObject Hideobject) {
		yield return new WaitForSeconds (time);
		Hideobject.transform.DOScale (Vector3.zero, 1f).OnComplete(() => {
			Hideobject.SetActive(false);
		});
	}

	public IEnumerator parentimageAnimation(Sprite[] images, GameObject parent, float speed,float sizeReducer) {
		foreach (Sprite img in images) {
			yield return new WaitForSeconds(speed);
			parent.GetComponent<RectTransform> ().sizeDelta = new Vector2(img.rect.width/sizeReducer,img.rect.height/sizeReducer);
			parent.GetComponent<Image> ().sprite = img;	

		}
		StartCoroutine (parentimageAnimation(images, parent, speed, sizeReducer));
	}

	public IEnumerator Startsmallactionsparent(AttachmentimageAnimationType obj, GameObject objects) {
		yield return new WaitForSeconds (obj.starttime);
				if (obj.scaleactions.scale) {
					objects.transform.DOScale (obj.scaleactions.scaleValue, obj.scaleactions.scaleTime).SetEase (obj.scaleactions.easetype);
				}
				if (obj.rotateaction.rotate) {
					
			Vector3 newRotation = obj.rotateaction.rotationvalue + new Vector3(objects.transform.localEulerAngles.x,objects.transform.localEulerAngles.y,objects.transform.localEulerAngles.z);
					objects.transform.DOLocalRotate (newRotation, obj.rotateaction.rotationTime).SetEase (obj.rotateaction.easetype);
				}
		}

	public IEnumerator enableChildObjects(childObjectsAnimation objects) {
		yield return new WaitForSeconds (objects.starttime);
		objects.chidObject.SetActive (true);
		StartCoroutine (DisableChildObject (objects.endTime, objects.chidObject));
	}

	public IEnumerator DisableChildObject(float time, GameObject obj) {
		yield return new WaitForSeconds (time);
		obj.SetActive (false);
	}
}




[System.Serializable]
public class AttachmentImages {
	public string name;
	public GameObject[] parentObjects;
	public Sprite[] parentOjectImages;
	public float spriteSpeed;
	public float spriteSizeReducer;
	public List<childObjectsAnimation> childObject;
}
	
[System.Serializable]
public class AttachmentImageAnimation {
	public float startTime;
	public float endTime;
	public string objectName;
	public List<AttachmentimageAnimationType> animationtypes;
}

[System.Serializable]
public class childObjectsAnimation {
	public float starttime;
	public float endTime;
	public GameObject chidObject;
}

[System.Serializable]
public class AttachmentimageAnimationType {
	public float starttime;
	public ScaleAction scaleactions;
	public RotateAction rotateaction;
}

[System.Serializable]
public class FadeAction {
	public bool fade;
	public float fadetime;
	public float fadevalue;
	public Ease easetype;
}
[System.Serializable]
public class ScaleAction {
	public bool scale;
	public float scaleTime;
	public Vector3 scaleValue;
	public Ease easetype;
}
[System.Serializable]
public class RotateAction {
	public bool rotate;
	public Vector3 rotationvalue;
	public float rotationTime;
	public Ease easetype;
}