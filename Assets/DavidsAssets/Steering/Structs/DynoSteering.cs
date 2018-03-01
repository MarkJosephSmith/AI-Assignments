using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SteeringNamespace
{

    public class DynoSteering
    {
        public Vector3 force = new Vector3(0f, 0f, 0f);
        public float torque = 0f;


		/*Perform a basic wander behaviour*/
		public Vector3 getWanderGoal(Vector3 i_Vel, Vector3 i_Pos, float i_ToCenter, float i_Rad) //, bool i_ReverseDirection = false)
		{
			Vector3 normVel = i_Vel;
			if ( Mathf.Abs(i_Vel.x) + Mathf.Abs(i_Vel.z) < 0.5 ) 
			{
				normVel = new Vector3 (1, 0, 0);
			} 
			else 
			{
				normVel.Normalize ();
			}
			/*
			if (i_ReverseDirection) 
			{
				normVel = (-1) * normVel;
			}
			*/
			Vector3 CirclePos = i_Pos + (normVel * i_ToCenter);
			Vector3 TargetOnCircle = CirclePos + (normVel * i_Rad);
			//Quaternion ToRotate = Quaternion.AngleAxis((Random.Range(0, 360)), Vector3.up);
			//o_TargetOnCircle = ToRotate * o_TargetOnCircle;
			Vector3 toReturn = Quaternion.Euler(0, (Random.Range(0, 360)) ,0) * (TargetOnCircle - CirclePos) + CirclePos;

			return toReturn;

		}



    }
}