using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SteeringNamespace
{
	public class Wander : MonoBehaviour {

		float CircleRadius = 5;
		//float CircleOffset = 10;
		float Angle = 180;

		// Use this for initialization
		void Start () {
			
		}
		
		// Update is called once per frame
		void Update () {
			
		}

		//code modeled after //why the hell doesn't monodevelop handle copy/paste?  Honestly... I can't even paste a web url...
		//https://gamedevelopment.tutsplus.com/tutorials/understanding-steering-behaviors-wander--gamedev-1624
		public Vector3 CalcOffset (Vector3 Direction)
		{
			//start with the direction we are heading and move to the center of the circle
			Vector3 tempVector = Direction;
			tempVector.Normalize();
			Vector3 ToReturn = (tempVector) * (CircleRadius * 2) ;
			Vector3 Offset = new Vector3 (0, 0, -1);
			Offset = Offset * CircleRadius;
			float tempNumber = Random.Range (0, 30);
			if (tempNumber % 2 == 0) 
			{
				Angle += tempNumber;
			} 
			else 
			{
				Angle -= tempNumber;
			}
			float mag = Offset.magnitude;
			Offset.x = Mathf.Cos (Angle) * mag;
			Offset.z = Mathf.Sin (Angle) * mag;

			ToReturn += Offset; 

			return ToReturn;
		}
	}
}
