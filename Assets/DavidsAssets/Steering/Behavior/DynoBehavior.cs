using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SteeringNamespace
{

    public class DynoBehavior : MonoBehaviour
    {

        private Kinematic char_RigidBody;
        private KinematicSteering ks;
        private DynoSteering ds;

        private KinematicSteeringOutput kso;
        private DynoSeek seek;
        private DynoArrive arrive;
        private DynoAlign align;

        private DynoSteering ds_force;
        private DynoSteering ds_torque;

		/*record and write out speed on timer*/
		private float StartTime;
		private string FileName;
		/*END record and write out speed on timer*/

		/*Drop a bread crumb*/
		public GameObject crumb;
		private float AppearanceRate;
		private float LastDroppedCrumb;
		/*END Drop a bread crumb*/

		/*Determine wander or seek behaviour*/
		private bool bHasGoal;
		private DynoSteering ds_wander;
		float toCenterWanderCircle;
		float wanderCircleRad;
		/*set level bounds*/
		public float UpperBoundX;
		public float LowerBoundX;
		public float UpperBoundZ;
		public float LowerBoundZ;



        // Use this for initialization
        void Start()
        { 
            char_RigidBody = GetComponent<Kinematic>();
            //seek = GetComponent<DynoSeek>();
            arrive = GetComponent<DynoArrive>();
            align = GetComponent<DynoAlign>();
			/*record and write out speed on timer*/
			StartTime = Time.time;
			FileName = "DynoTimer.txt";
			System.IO.File.WriteAllText (@"C:\Grad School\AI\A1\" + FileName ,"0");
			AppearanceRate = 0.1f;
			LastDroppedCrumb = 0.0f;
			bHasGoal = false;
			ds_wander = new DynoSteering ();
			toCenterWanderCircle = 10;
			wanderCircleRad = 5;


			UpperBoundX = -125;
			LowerBoundX = -175;
			UpperBoundZ = 40;
			LowerBoundZ = -10;

        }

        // Update is called once per frame
        void Update()
        {

			ds = new DynoSteering();

			if (bHasGoal) 
			{
				// Decide on behavior
				ds_force = arrive.getSteering ();
				ds_torque = align.getSteering ();

			} 
			else 
			{
				Vector3 tempGoal = ds_wander.getWanderGoal (char_RigidBody.getVelocity (), transform.position, toCenterWanderCircle, wanderCircleRad);   //, UpperBoundX, LowerBoundX, UpperBoundZ, LowerBoundZ); 
				ds_force = arrive.getSteering (tempGoal);
				ds_torque = align.getSteering (tempGoal);
			}

			ds.force = ds_force.force;
			ds.torque = ds_torque.torque;


            // Update Kinematic Steering
            kso = char_RigidBody.updateSteering(ds, Time.deltaTime);
            //Debug.Log(kso.position);
         	transform.position = new Vector3(kso.position.x, transform.position.y, kso.position.z);
            transform.rotation = Quaternion.Euler(0f, kso.orientation * Mathf.Rad2Deg, 0f);

			/*record and write out speed on timer*/
			string t = (Time.time - StartTime).ToString();

			using (System.IO.StreamWriter file = 
				       new System.IO.StreamWriter (@"C:\Grad School\AI\A1\" + FileName, true)) 
			{
				file.WriteLine (t + " | " + char_RigidBody.getVelocity().ToString() );
			}

			DropCrumb (char_RigidBody.getVelocity());

			if ( !(CheckInBounds(transform.position, UpperBoundX, LowerBoundX, UpperBoundZ, LowerBoundZ)))
			{
				
				char_RigidBody.setVelocity(char_RigidBody.getVelocity () * -1);
				//char_RigidBody.setRotation(char_RigidBody.getRotation () * -1);
				//char_RigidBody.setOrientation(char_RigidBody.getOrientation () - 180);
				//transform.Rotate (0, 180, 0);


				//float targetOrientation = char_RigidBody.getNewOrientation ((transform.position + char_RigidBody.getVelocity ()));
				//char_RigidBody.setRotation (targetOrientation - char_RigidBody.getOrientation ());

				//char_RigidBody.setOrientation (targetOrientation);
				//char_RigidBody.setRotation (char_RigidBody.getRotation () - 180);
				//char_RigidBody.setOrientation
				/*
				if (CheckInBounds (transform.position + (char_RigidBody.getVelocity () * -1), UpperBoundX, LowerBoundX, UpperBoundZ, LowerBoundZ)) 
				{
					char_RigidBody.setVelocity (char_RigidBody.getVelocity () * -1);
				}
				*/
				//transform.Rotate (0, 180, 0);
				//char_RigidBody.setOrientation( char_RigidBody() * (-1)  );//char_RigidBody.getNewOrientation())//  char_RigidBody.getOrientation() - 180  );//char_RigidBody.getNewOrientation (char_RigidBody.getVelocity ()));
				//transform.rotation = Quaternion.Euler (0, kso.orientation * Mathf.Rad2Deg * -1 ,0);
				//char_RigidBody.setRotation(char_RigidBody.getRotation () * -1);
				
			}
        }

		void DropCrumb(Vector3 scaler)
		{
			if (AppearanceRate < LastDroppedCrumb) 
			{
				LastDroppedCrumb = 0.0f;
				GameObject toDrop = Instantiate (crumb, new Vector3 (transform.position.x, transform.position.y, transform.position.z), transform.rotation);
				toDrop.transform.localScale = toDrop.transform.localScale + (new Vector3 (0, ( (Mathf.Abs( scaler.x)) + (Mathf.Abs( scaler.z))), 0));
			} 
			else 
			{
				LastDroppedCrumb = LastDroppedCrumb + (Time.time - LastDroppedCrumb);
			}
		}

		/*check if we have stepped off the edge.  
		params are the bounds of the space in relative terms ie. PosX need not be a positive number but rather the X bound in the positive direction in world space*/
		bool CheckInBounds(Vector3 location, float PosX, float NegX, float PosZ, float NegZ)
		{
			if ((location.x > PosX) || (location.x < NegX) || (location.z < NegZ) || (location.z > PosZ)) 
			{
				return false;
			}
			return true;
		}


		/*Worry about this later
		public void SetGoal()
		{
		}
		*/

    }
}