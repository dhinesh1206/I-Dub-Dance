using UnityEngine;
using System.Collections;

public class Done_BGScroller : MonoBehaviour
{
	public float scrollSpeed;
	//public float tileSizeZ;

	private Vector3 startPosition;
	public float TileSize;
	private float tilesize;
	public string direction;
	void Start ()
	{
		startPosition = transform.localPosition;

	}

	void Update ()
	{
		switch (direction) {
		case "Right":
			tilesize = (transform.localScale.x) * TileSize;
			float newPosition = Mathf.Repeat (Time.time * scrollSpeed, tilesize);
			transform.localPosition = startPosition + Vector3.right * newPosition;
			break;
		case "Left":
			tilesize = (transform.localScale.x) * TileSize;
			float newPositions = Mathf.Repeat (Time.time * scrollSpeed, tilesize);
			transform.localPosition = startPosition + Vector3.left * newPositions;
			break;
		case "Up":
			tilesize = (transform.localScale.x) * TileSize;
			float newPositionss = Mathf.Repeat (Time.time * scrollSpeed, tilesize);
			transform.localPosition = startPosition + Vector3.up * newPositionss;
			break;
		case "Down":
			tilesize = (transform.localScale.x) * TileSize;
			float newPositionz = Mathf.Repeat (Time.time * scrollSpeed, tilesize);
			transform.localPosition = startPosition + Vector3.down * newPositionz;
			break;
		default:
			print ("Incorrect intelligence level.");
			break;
		}
	}
}
