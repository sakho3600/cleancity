﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class changeScene : MonoBehaviour
{

	public GameObject[] arrows;
	public GameObject text;
	public GameObject aerosolText;
	public GameObject newspaperText;
	public GameObject appleText;
	public GameObject waterText;
	public string sceneName = "NA";

	private GameObject[] waste;
	private int n;
	private int lastN;

	// Use this for initialization
	void Start ()
	{
		waste = GameObject.FindGameObjectsWithTag ("waste");
		lastN = waste.Length;
		if (lastN != 4) {
			throw new System.NotSupportedException ();
		}
	}
	
	// Update is called once per frame
	void Update ()
	{
		waste = GameObject.FindGameObjectsWithTag ("waste");
		n = waste.Length;
		if (n != lastN) {
			lastN = n;
			if (n > 0) {
				arrows [3 - n].SetActive (false);
				arrows [4 - n].SetActive (true);
				if (n == 1) {
					appleText.SetActive (false);
					newspaperText.SetActive (true);
					text.SetActive (true);
				}
				if (n == 2) {
					waterText.SetActive (false);
					appleText.SetActive (true);
				}
				if (n == 3) {
					aerosolText.SetActive (false);
					waterText.SetActive (true);
				}

			} else {
				// n == 0
				SceneManager.LoadScene (sceneName);
			}

		}
		
	}
}
