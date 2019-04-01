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


    void Start()
    {

    }


    void Update()
    {
        MoveToWaypoint();
    }
    void MoveToWaypoint()
    {
        if (Vector3.Distance(new Vector3(transform.position.x, transform.position.y, transform.position.z), new Vector3(Waypoints[_index + 1].position.x, transform.position.y, Waypoints[_index + 1].position.z)) <= _maxDistance)
        {
            ++_index;
            if (_index >= Waypoints.Length - 1)
            {
                _index = -1;
            }
            //_transform.rotation = Quaternion.LookRotation(WayPoints[_index + 1].position - _transform.position, Vector3.up);
            transform.LookAt(Waypoints[_index + 1].transform);
        }
        transform.Translate(Vector3.forward * _swimmingSpeed * Time.deltaTime);
    }
}