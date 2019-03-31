using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaypointBehaviour : MonoBehaviour {

    public Transform[] Waypoints;
    private int _waypointIndex = 0;
    [SerializeField]
    private float _swimmingSpeed;
    private bool _waypointReached;

    void Start ()
    {
		
	}
	
	
	void Update ()
    {
       
	}
    void MoveToWaypoint()
    {
        if(_waypointReached)
            transform.position = Vector3.MoveTowards(transform.position, Waypoints[_waypointIndex].position, _swimmingSpeed * Time.deltaTime);
    }
    void NextWaypoint()
    {
        if (_waypointReached = false && Vector3.Distance(new Vector3(transform.position.x, transform.position.z), new Vector3(Waypoints[_waypointIndex].position.x, 0, Waypoints[_waypointIndex].position.z)) > 2)
        {
            _waypointIndex++;
            _waypointReached = true;
        }
    }
}
