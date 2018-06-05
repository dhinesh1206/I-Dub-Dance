using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vuforia;

public class MainUiController : MonoBehaviour {

	public static MainUiController instance;
	public PlaneFinderBehaviour plane;
	public GameObject groundplane;
	public AudioSource uiCharecterAudio;
	public Animator uicharecterAnimator;
	public GameObject[] screens;
	public List<ScreensList> allScreenSet;
	public GameObject Arcamera;
    public Camera maincam;
	public int themeIndex;
	public List<recodingActivatingscreen> recScreens;
	public GameObject activeRecordScreen;

	public bool effectsinstantiated =  false;

    public void Start()
    {
        if (allScreenSet[3].Screens[themeIndex])
            allScreenSet[3].Screens[themeIndex].SetActive(false);
        Arcamera.SetActive(false);
    }

	void Awake() {
		instance = this;
	}

	public void CharecterSelectionDone(){
		//InstantiateScrollItems.instance.effects = false;

		allScreenSet [0].Screens [themeIndex].SetActive (false);
		//screens [0].SetActive (false);

		allScreenSet[1].Screens[themeIndex].SetActive(true);
		TestScrollItems.instance.effects = false;
		TestScrollItems.instance.InstantiateCalled ();
	}

	public void AnimationSelectionDone() {
		TestScrollItems.instance.effects = true;
		//TestScrollItems.instance.effects = true;

		if (!effectsinstantiated) {
			TestScrollItems.instance.InstantiateCalled ();
			effectsinstantiated = true;
		}
		allScreenSet[1].Screens[themeIndex].SetActive(false);
		allScreenSet[2].Screens[themeIndex].SetActive(true);
		uiCharecterAudio.Stop ();
		TestScrollItems.instance.CloseSideBar();
		TestScrollItems.instance.DestroyEffectImages ();
		uicharecterAnimator.Play ("Idle");
	}

	public void EffectSelectionDone() {
        Arcamera.SetActive(true);
        maincam.gameObject.SetActive(false);
		allScreenSet[2].Screens[themeIndex].SetActive(false);
		plane.enabled = true;
		TestScrollItems.instance.effects = false;
	}

	public void DeActivateMainScreen() {
		allScreenSet[3].Screens[themeIndex].SetActive(false);
	}
	public void ActivateMainScreen() {
		allScreenSet[3].Screens[themeIndex].SetActive(true);
	}


	public void HomeScreenPressed() {
		if(RecScript.instance.screenName)
		RecScript.instance.screenName.SetActive (false);
		RecScript.instance.screenName = null;
        maincam.gameObject.SetActive(true);
        Arcamera.SetActive(false);
		foreach (GameObject mainScreen in allScreenSet[3].Screens) {
			mainScreen.SetActive (false);
		}
		if (TestScrollItems.instance.effectinstantiated) {
			
			Destroy (TestScrollItems.instance.effectinstantiated);
		}
		TestScrollItems.instance.InstantiateCalled ();
		groundplane.SetActive (false);
		DeployStageOnce.instance.placed = false;
		plane.enabled = true;
		allScreenSet[1].Screens[themeIndex].SetActive(true);
	}

	public void SkipPressed() {
		allScreenSet[2].Screens[themeIndex].SetActive(false);
		plane.enabled = true;
	}

	public void EditProfileSelected() {
		TestScrollItems.instance.bodyAudio.Stop ();
		TestScrollItems.instance.animationCharecter.Play ("Idle");
		TestScrollItems.instance.CloseSideBar ();
		allScreenSet [1].Screens [themeIndex].SetActive (false);
		allScreenSet [0].Screens [themeIndex].SetActive (true);
	}

	public void ChangeAnimation() {
		StartCoroutine (TestScrollItems.instance.StopAnimation (0.1f));
		Destroy (TestScrollItems.instance.effectinstantiated);
		TestScrollItems.instance.effects = false;
		allScreenSet[2].Screens[themeIndex].SetActive(false);
		allScreenSet[1].Screens[themeIndex].SetActive(true);
		TestScrollItems.instance.InstantiateCalled ();
	}

	public void RecordingScreensActivate(string name) {
		foreach (var obj in recScreens) {
			if (name == obj.screenname) {
				obj.screen.SetActive (true);
				activeRecordScreen = obj.screen;
			}
		}
	}
}

[System.Serializable]
public class recodingActivatingscreen
{
	public string screenname;
	public GameObject screen;
}
