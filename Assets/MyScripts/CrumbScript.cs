using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrumbScript : MonoBehaviour {

	private float StartTime { get; set;}
	private float ExistedTime { get; set;}
	private float DisappearanceRate { get; set;}

	// Use this for initialization
	void Start () {

		ExistedTime = 0.0f;
		StartTime = Time.time;
		DisappearanceRate = 7.0f;
		 
	}
	
	// Update is called once per frame
	void Update () {
		ExistedTime = Time.time - StartTime;
		if (ExistedTime > DisappearanceRate) 
		{
			Destroy (gameObject);
		}
	}
}
