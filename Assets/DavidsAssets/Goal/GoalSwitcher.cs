using System.Collections;
using System.Collections.Generic;
using SteeringNamespace;
using UnityEngine;
 

namespace GoalNamespace
{

    public class GoalSwitcher : MonoBehaviour
    {

        public float goalSwitchTime = 2f;
        private Goal goal_script;
        private int goal_index = 0;
        public List<GameObject> ordered_goals;
        private float time_since_goal_switch = 0f;
		private Kinematic KinData;
        // Use this for initialization


		//Change goal switcher so that it only changes if the boid is at rest
		//private bool CanSwitch = false;

        void Start()
        {
            goal_script = GetComponent<Goal>();
            goal_script.setGoal(ordered_goals[goal_index]);

			KinData = (GetComponent<Kinematic>());
        }

        // Update is called once per frame
        void Update()
        {
			
            time_since_goal_switch += Time.deltaTime;
			Vector3 CurrentSpeed = KinData.getVelocity(); //(GetComponent<Kinematic>()).getVelocity();

			if ( (time_since_goal_switch > goalSwitchTime) && (  Mathf.Abs(CurrentSpeed.x) < 0.2f ) && (Mathf.Abs( CurrentSpeed.z ) < 0.2f ))
            {
                if (goal_index >= ordered_goals.Count - 1)
                {
                    goal_index = 0;
                }
                else
                {
                    goal_index += 1;
                }
                goal_script.setGoal(ordered_goals[goal_index]);
                time_since_goal_switch = 0f;
            }
            
        }


    }
}