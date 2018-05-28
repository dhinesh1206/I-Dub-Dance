using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class BusMovement : MonoBehaviour {
	public List<PositionValues> positions;

	// Use this for initialization
	void Start () {
		StartCoroutine(Movement ());

	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public IEnumerator Movement() {
		foreach (var obj in positions) {
			if (obj.Direction == "X" || obj.Direction == "x") {
				transform.DOLocalMoveX (obj.EndPosition, obj.movementTime, false).SetEase (Ease.Linear).OnComplete (() => {
					transform.DOLocalRotate (new Vector3 (0, 0, obj.EndRotations), obj.rotationTime).SetEase(Ease.Linear);
					transform.DOLocalMove (new Vector3 (obj.endPositonsRotation [0], obj.endPositonsRotation [1], 0), obj.rotationTime, false).SetEase (Ease.Linear);
				});
			} else if(obj.Direction == "Y" || obj.Direction == "y") {
				transform.DOLocalMoveY (obj.EndPosition, obj.movementTime, false).SetEase (Ease.Linear).OnComplete (() => {
					transform.DOLocalRotate (new Vector3 (0, 0, obj.EndRotations), obj.rotationTime).SetEase(Ease.Linear);
					transform.DOLocalMove (new Vector3 (obj.endPositonsRotation [0], obj.endPositonsRotation [1], 0), obj.rotationTime, false).SetEase (Ease.Linear);
				});
			}

			yield return new WaitForSeconds (obj.movementTime + obj.rotationTime);
		}
		StartCoroutine (Movement ());
	}

}


[System.Serializable]
public class PositionValues {
	public float EndPosition;
	public float EndRotations;
	public float[] endPositonsRotation;
	public string Direction;
	public float movementTime;
	public float rotationTime;
}
