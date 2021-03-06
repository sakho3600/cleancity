﻿using UnityEngine;
using System.Collections;

public class WasteGenerator : MonoBehaviour
{

	public int count = 20;
	private static int _count;
	public float waitTime = 5f;
	public int numCoords = 14;

	public GameObject wastePrefab;
	public Vector3[] coords;
	public Sprite[] recycleSprites;
	// 6 sprites
	public Sprite[] trashSprites;
	// 5 sprites
	public Sprite[] compostSprites;
	// 5 sprites

	// Use this for initialization
	void Start ()
	{
		_count = count;
		StartCoroutine (Generate ());
	

	}

	IEnumerator Generate ()
	{
		while (_count > 0 && !Game.status) {
			Sprite newSprite;
			Waste waste;
			GameObject wasteObj;

			Vector3 position = coords [Random.Range (0, numCoords)];
			int n = Random.Range (0, 4);

			if (n == 0) {
				// compost only
				int s = Random.Range (0, 5);
				newSprite = compostSprites [s];
				if (s < 3) {
					waste = new compostAndRecycle ();
				} else {
					waste = new Compost ();
				}
			} else if (n == 1) {
				// recyclable only
				int s = Random.Range (0, 6);
				newSprite = recycleSprites [s];
				if (s >= 4) {
					waste = new compostAndRecycle ();
				} else {
					waste = new Recyclable ();
				}
			} else if (n == 2) {
				// trash only
				int s = Random.Range (0, 5);
				waste = new Trash ();
				newSprite = trashSprites [s];
			} else {
				// n == 3
				// compost and recyclables
				int rand = Random.Range (0, 2);
				waste = new compostAndRecycle ();
				if (rand == 0) {
					// pick sprite from end of recyclable sprite array
					int s = Random.Range (4, 6);
					newSprite = recycleSprites [s];
				} else {
					// pick sprite from start of compost sprite array
					int s = Random.Range (0, 3);
					newSprite = compostSprites [s];
				}
			}
			wasteObj = wastePrefab;
			wasteComponent wc = wasteObj.GetComponent<wasteComponent> ();
			wc.waste = waste;
			wc.UpdateValues ();
			SpriteRenderer oldSprite = wasteObj.GetComponent<SpriteRenderer> ();
			oldSprite.sprite = newSprite;
			yield return new WaitForSeconds (waitTime);
			Instantiate (wasteObj, position, Quaternion.identity);
			_count--;
		}

	}

	public static int Count {
		get { return _count; }
	}

	void ChangeBoxColliderSize ()
	{
		SpriteRenderer compRenderer = GetComponent<SpriteRenderer> (); // sprite renderer for compost
		SpriteRenderer recRenderer = GetComponent<SpriteRenderer> (); // sprite renderer for recyclables
		SpriteRenderer trashRenderer = GetComponent<SpriteRenderer> (); // sprite rend. for trash 

		compRenderer.sprite = compostSprites [Random.Range (0, compostSprites.Length)];
		recRenderer.sprite = recycleSprites [Random.Range (0, recycleSprites.Length)];
		trashRenderer.sprite = trashSprites [Random.Range (0, trashSprites.Length)];

		BoxCollider compCollider = GetComponent<BoxCollider> (); // for compost
		BoxCollider recCollider = GetComponent<BoxCollider> (); // for recyclables
		BoxCollider trashCollider = GetComponent<BoxCollider> (); // for trash

		compCollider.size = compRenderer.bounds.size;
		recCollider.size = recRenderer.bounds.size;
		trashCollider.size = trashRenderer.bounds.size;



	}
			

}
