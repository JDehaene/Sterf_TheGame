using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GullBehaviour : MonoBehaviour {

    [SerializeField]
    private float _flightSpeed;
    [SerializeField]
    private float _descendSpeed;
    [SerializeField]
    private float _ascendSpeed;
    [SerializeField]
    private float _turnSpeed;

    private Camera _cam;
    private CharacterController _c;
  
	void Start ()
    {
        _c = GetComponent<CharacterController>();
        _cam = GetComponent<Camera>();
	}

	void Update ()
    {
        _c.Move(_velocity);
	}
}
