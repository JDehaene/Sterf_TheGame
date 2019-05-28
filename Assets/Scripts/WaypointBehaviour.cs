using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaypointBehaviour : MonoBehaviour
{

    public Transform[] Waypoints;
    [SerializeField]
    private int _index;
    [SerializeField]
    private float _swimmingSpeed;
    private bool _waypointReached = true;
    [SerializeField]
    private float _maxDistance;
    private Quaternion _wantedRotation;

    void Update()
    {
        MoveToWaypoint();
        
    }
    void MoveToWaypoint()
    {
        if (Vector3.Distance(transform.position, new Vector3(Waypoints[_index + 1].position.x, Waypoints[_index + 1].position.y, Waypoints[_index + 1].position.z)) <= _maxDistance)
        {
            ++_index;
            if (_index >= Waypoints.Length - 1)
            {
                _index = -1;
            }
        }
        _wantedRotation = Quaternion.LookRotation(Waypoints[_index + 1].position - transform.position, Vector3.up);
        transform.rotation = Quaternion.Slerp(transform.rotation, _wantedRotation,Time.deltaTime);
        transform.position = Vector3.MoveTowards(transform.position, Waypoints[_index + 1].position, _swimmingSpeed * Time.deltaTime);
    }
}