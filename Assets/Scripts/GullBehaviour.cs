using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GullBehaviour : MonoBehaviour {

    [SerializeField]
    private float _acceleration;
    [SerializeField]
    private float _descendSpeed;
    [SerializeField]
    private float _ascendSpeed;
    [SerializeField]
    private float _turnSpeed;

    private Camera _cam;
    private CharacterController _c;
    private Vector3 _velocity;
  
	void Start ()
    {
        _c = GetComponent<CharacterController>();
        _cam = GetComponent<Camera>();
	}

	void Update ()
    {
        HandleInput();
        ForwardVelocity();
        MaxFlyingSpeed();
        Turning();
        AscendDescend();
        Dive();

	}
    private void FixedUpdate()
    {
        _c.Move(_velocity);
    }

    void HandleInput()
    {

    }
    void ForwardVelocity()
    {
        _velocity += Vector3.forward * _acceleration * Time.deltaTime;
    }
    void MaxFlyingSpeed()
    {

    }
    void Turning()
    {

    }
    void AscendDescend()
    {

    }
    void Dive()
    {

    }
}

