using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using HedgehogTeam.EasyTouch;
using DG.Tweening;
using DG;

public class ZoomScript : MonoBehaviour {

	public Vector2 startPoint;
	public float distance;
	public string direction;
	public bool swipe = false;
	public bool charecterSelected = false;

	RaycastHit Hit;

	void OnEnable()
	{
		EasyTouch.On_TouchStart2Fingers += On_TouchStart2Fingers;
		EasyTouch.On_PinchIn += On_PinchIn;
		EasyTouch.On_PinchOut += On_PinchOut;
		EasyTouch.On_LongTap += On_LongPress;
	}

	void Update() 
	{
		if (charecterSelected == true) {
			if (Input.GetMouseButton (0)) 
			{
				Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
				if (Physics.Raycast (ray, out Hit)) 
				{
					if (Physics.Raycast (ray, out Hit)) 
					{
						if (Hit.transform.gameObject.tag == "Cube") {
							return;
						} else {
							Vector3 target = Hit.point;
							transform.DOMove (target, 1f, false);
						}
					}
				}
			}
			if (Input.GetMouseButtonUp (0)) 
			{
				charecterSelected = false;
			}
		}
	}

	void OnDisable()
	{
		UnsubscribeEvent();
	}

	void OnDestroy()
	{
		UnsubscribeEvent();
	}
	void UnsubscribeEvent()
	{
		EasyTouch.On_TouchStart2Fingers -= On_TouchStart2Fingers;
		EasyTouch.On_PinchIn -= On_PinchIn;
		EasyTouch.On_PinchOut -= On_PinchOut;
	}
		
	private void On_TouchStart2Fingers( Gesture gesture)
	{
		EasyTouch.SetEnableTwist( false);
		EasyTouch.SetEnablePinch( true);
	}
		
	private void On_PinchIn(Gesture gesture)
	{
		float zoom = Time.deltaTime * gesture.deltaPinch * 0.01f;
		Vector3 scale = transform.localScale;
		if (scale.x > 0.001 && scale.y > 0.001 && scale.z > 0.001) 
		{
			transform.localScale = new Vector3 (scale.x - zoom, scale.y - zoom, scale.z - zoom);
		}
	}

	private void On_LongPress(Gesture gesture) 
	{
		charecterSelected = true;
	}
		
	private void On_PinchOut(Gesture gesture)
	{
			float zoom = Time.deltaTime * gesture.deltaPinch * 0.01f;
			Vector3  scale = transform.localScale ;
			transform.localScale = new Vector3( scale.x + zoom, scale.y +zoom,scale.z+zoom);
	}

	private void On_Swipe(Gesture gesture) 
	{
		if (Input.touchCount == 1) 
		{
			if (gesture.swipe.ToString () == "Left") 
			{
				transform.position -= new Vector3 ((0.02f), 0, 0);
			}
			else if (gesture.swipe.ToString () == "Right") 
			{
				transform.position += new Vector3 ((0.02f), 0, 0);
			}
			else if (gesture.swipe.ToString () == "Up") 
			{
				transform.position += new Vector3 (0, 0, (0.02f));
			}
			else if (gesture.swipe.ToString () == "Down") 
			{
				transform.position -= new Vector3 (0, 0, (0.02f));
			}
		}

	}

	public void EndGuestures() 
	{
		
	}
}
