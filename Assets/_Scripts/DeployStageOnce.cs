using System;
using UnityEngine;
using Vuforia;

public class DeployStageOnce : MonoBehaviour {

	public GameObject AnchorStage;
	private PositionalDeviceTracker _deviceTracker;
	private GameObject _previousAnchor;

	public static DeployStageOnce instance;

	public GameObject mainMenu;


	public GameObject boy;

	public bool placed = false;

	public void Start ()
	{
		if (AnchorStage == null)
		{
			Debug.Log("AnchorStage must be specified");
			return;
		}
		AnchorStage.SetActive(false);
	}

	public void Awake()
	{
		instance = this;
		VuforiaARController.Instance.RegisterVuforiaStartedCallback(OnVuforiaStarted);
	}

	public void OnDestroy()
	{
		VuforiaARController.Instance.UnregisterVuforiaStartedCallback(OnVuforiaStarted);
	}

	private void OnVuforiaStarted()
	{
		_deviceTracker = TrackerManager.Instance.GetTracker<PositionalDeviceTracker>();
	}

	public void OnInteractiveHitTest(HitTestResult result)
	{
		if (placed == false && Input.GetMouseButton (0)) {
			if (result == null || AnchorStage == null) {
				Debug.LogWarning ("Hit test is invalid or AnchorStage not set");
				return;
			}
			var anchor = _deviceTracker.CreatePlaneAnchor (Guid.NewGuid ().ToString (), result);
			if (anchor != null) {
				placed = true;
				transform.GetComponent<PlaneFinderBehaviour> ().enabled = false;
				AnchorStage.transform.parent = anchor.transform;
				AnchorStage.transform.localPosition = Vector3.zero;
				AnchorStage.transform.localRotation = Quaternion.identity;
				AnchorStage.SetActive (true);
				//mainMenu.SetActive (true);
				MainUiController.instance.ActivateMainScreen();
				RotateTowardCamera (boy);
			}
			if (_previousAnchor != null) {
				Destroy (_previousAnchor);
			}
			_previousAnchor = anchor;
		}
	}

	void RotateTowardCamera(GameObject augmentation)
	{
		var lookAtPosition = Camera.main.transform.position - augmentation.transform.position;
		lookAtPosition.y = 0;
		var rotation = Quaternion.LookRotation(lookAtPosition);
		augmentation.transform.rotation = rotation;
	}

	public void ResetPressed() 
	{
		if (AnimationControlls.instance. instantiatedImage) {
			Destroy (AnimationControlls.instance. instantiatedImage);
		}
		if(RecScript.instance.screenName)
		RecScript.instance.screenName.SetActive (false);
		AnchorStage.SetActive (false);
		//mainMenu.SetActive (false);
		MainUiController.instance.DeActivateMainScreen();
		transform.GetComponent<PlaneFinderBehaviour> ().enabled = true;
		placed = false;
	}
}
