﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class deleteWaste : MonoBehaviour {

	public Sprite cross;
	public Sprite check;
	public float time = 0.5f;

	private List<char> bins =  new List<char> (new char[]{'C', 'R', 'T'});
	private char binType;
	private SpriteRenderer sr;
	private Sprite oldSprite;
	private Sprite newSprite;
	private GameObject waste;
	private bool drop;
	private bool rPoint;
	private bool cPoint;
	private bool tPoint;


	// Use this for initialization
	void Start () {
		sr = gameObject.GetComponent<SpriteRenderer> ();
		oldSprite = sr.sprite;
		binType = gameObject.tag [0];
		if (!bins.Contains (binType)) {
			throw new System.ArgumentException ("Invalid binType", "binType");
		}
			
	}

	void OnTriggerEnter (Collider other){
		// other is waste being dragged into bins

		waste = other.gameObject;
		wasteComponent wc = other.gameObject.GetComponent<wasteComponent>();
		rPoint = binType == 'R' && wc.r;
		cPoint = binType == 'C' && wc.c;
		tPoint = binType == 'T' && wc.t;

	}

	void OnTriggerStay(){
		if (Input.GetMouseButtonUp (0)) {
			drop = true;
			if (rPoint || cPoint || tPoint) {
				newSprite = check;
				ScoreManager.IncScore ();
			} else {
				newSprite = cross;
				StrikeManager.Strike ();
			}

			StartCoroutine (ChangeSprite ());
		} else {
			drop = false;
		}
	}

	IEnumerator ChangeSprite(){
		sr.sprite = newSprite;
		yield return new WaitForSecondsRealtime (time);
		sr.sprite = oldSprite;

	}

	void Update(){
		if (drop) {
			Cursor.visible = true;
			Destroy (waste);
		}
		drop = false;
	}
}
