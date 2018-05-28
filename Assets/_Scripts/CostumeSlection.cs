using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vuforia;
using DG.Tweening;
using UnityEngine.Rendering;

public class CostumeSlection : MonoBehaviour {
	
	public bool hairSelected;
	public bool clothSelected;
	public bool fullConstume;

	public Transform CharecterStage;

	public PlaneFinderBehaviour planefinder;
	public List<CharecterStyle> stylebodyparts;
	public Material transparentMaterial;
	public Ease stageease;
	public List<TestMateria> materials;

	void Start () {
		foreach (var obj in stylebodyparts) {
			Material[] mat = obj.body.materials;
			mat[0] = Resources.Load("Test") as Material;
			obj.body.materials = mat;
		}

	}

	public void Select() {
		MainUiController.instance.CharecterSelectionDone ();
	}

	public void Next()
	{
		if (hairSelected == true) {
//			styles.hair [hairIndex].SetActive (false);
//			maincharecter.hair [hairIndex].SetActive (false);
//			hairIndex = hairIndex + 1 != styles.hair.Count ? hairIndex + 1 : 0; 
//			maincharecter.hair [hairIndex].SetActive (true);
//			styles.hair [hairIndex].SetActive (true);
		} else if (clothSelected == true) {
//			styles.Dress [clothIndex].SetActive (false);
//			maincharecter.Dress [clothIndex].SetActive (false);
//			clothIndex = clothIndex + 1 != styles.Dress.Count ? clothIndex + 1 : 0; 
//			styles.Dress [clothIndex].SetActive (true);
//			maincharecter.Dress [clothIndex].SetActive (true);
		} else if(fullConstume == true) {
			CharecterStage.DOLocalMoveY (-5, 0.5f, false).SetEase (stageease).OnComplete (() => {
				foreach (var objects in stylebodyparts) 
				{
					objects.fullConstume [objects.Index].SetActive (false);
					objects.Index = objects.Index + 1 != objects.fullConstume.Count ? objects.Index + 1 : 0; 
					objects.fullConstume [objects.Index].SetActive (true);
					Material[] mat = objects.body.materials;
					mat [1] = objects.bodyMaterial[objects.Index];
					mat [2] = objects.faceexpression [objects.Index];
					objects.body.materials = mat;
					AnimationControlls.instance.charecterSelectionIndex = objects.Index;
					TestScrollItems.instance.charecterSelectionindex = objects.Index;
					CharecterStage.DOLocalMoveY (-0.7f, 0.5f, false).SetEase (stageease);
				}
			});
		}
	}

	public void Previous()
	{
		if (hairSelected == true) {
//			styles.hair [hairIndex].SetActive (false);
//			maincharecter.hair [hairIndex].SetActive (false);
//			hairIndex = hairIndex - 1 < 0 ? hairIndex - 1 : styles.hair.Count -1; 
//			maincharecter.hair [hairIndex].SetActive (true);
//			styles.hair [hairIndex].SetActive (true);
		} else if (clothSelected == true) {
//			styles.Dress [clothIndex].SetActive (false);
//			maincharecter.Dress [clothIndex].SetActive (false);
//			clothIndex = clothIndex - 1 < 0 ? clothIndex - 1 : styles.Dress.Count -1; 
//			styles.Dress [clothIndex].SetActive (true);
//			maincharecter.Dress [clothIndex].SetActive (true);
		} else if(fullConstume == true) {
			CharecterStage.DOLocalMoveY (-5, 0.5f, false).SetEase (stageease).OnComplete (() => {
				foreach (var objects in stylebodyparts) {
					objects.fullConstume [objects.Index].SetActive (false);
					objects.Index = objects.Index - 1 > -1 ? objects.Index - 1 : objects.fullConstume.Count - 1; 
					objects.fullConstume [objects.Index].SetActive (true);
					Material[] mat = objects.body.materials;
					mat [1] = objects.bodyMaterial [objects.Index];
					mat [2] = objects.faceexpression [objects.Index];
					objects.body.materials = mat;
					AnimationControlls.instance.charecterSelectionIndex = objects.Index;
					TestScrollItems.instance.charecterSelectionindex = objects.Index;
					CharecterStage.DOLocalMoveY (-0.7f, 0.25f, false).SetEase (Ease.Linear);
				}
			});
		}
	}
}
	
[System.Serializable]
public class CharecterStyle
{
	public List<GameObject> Dress;
	public List<GameObject> hair;
	public List<GameObject> fullConstume;
	public List<Material> bodyMaterial;
	public List<Material> faceexpression;
	public Renderer body;
	public int Index = 0;
}

[System.Serializable]
public class TestMateria 
{
	public Material bodymaterial;
	public Material hairmaterial;
}
