using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class SpawnerMovement : MonoBehaviour {

//	public float startRange;
//	public float endRange;
	public float movementTime;
//	public float Distance;
	public float Speed;
	public Vector3 Target;
	public string Directions;
	public Ease easeType;

//	public Transform TopSpwner;
//	public Transform bottomspawner;

	private float VisualTargetRange = 9;
	// Use this for initialization
	void Start () {
		MovementControll ();

	}

	void Update() {
//		if (Directions == "y") {
//			if (Time.timeSinceLevelLoad > 100) {
//				TopSpwner.localPosition = new Vector3 (0, 3f, 0);
//				bottomspawner.localPosition = new Vector3 (0, -3f, 0);
//			}
//			if (Time.timeSinceLevelLoad > 150) {
//				TopSpwner.localPosition = new Vector3 (0, 2.7f, 0);
//				bottomspawner.localPosition = new Vector3 (0, -2.7f, 0);
//			}
//		}
	}

	void MovementControll() {
		
		if (Directions == "y") {
			//Target = new Vector3 (25, Random.Range (startRange, endRange), 10);
		//	Distance = Vector3.Distance (transform.position, Target);

		//	movementTime = Distance / Speed;

			transform.DOLocalMoveY (Target.y, movementTime, false).SetEase (easeType).OnComplete (() => {
				MovementControll ();
			
			});
		}

		if (Directions == "x") {
				VisualTargetRange = -(VisualTargetRange);
				Target = new Vector3 (VisualTargetRange, 0f, 0f);
				//Distance = Vector3.Distance (transform.position, Target);

			//	movementTime = Distance / Speed;

				transform.DOLocalMoveX (Target.x, movementTime, false).SetEase (easeType).OnComplete (() => {
					MovementControll ();
				});
			
		}
	}
}