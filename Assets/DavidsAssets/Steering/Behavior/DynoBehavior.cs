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
        }

        // Update is called once per frame
        void Update()
        {
            // Decide on behavior
            ds_force = arrive.getSteering();
            ds_torque = align.getSteering();

            ds = new DynoSteering();
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
        }
    }
}