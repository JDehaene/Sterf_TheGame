using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShrimpBehaviour : MonoBehaviour {

    [SerializeField]
    private float _acceleration;
    [SerializeField]
    private float _turnSpeed;
    [SerializeField]
    private float _maxSpeed;
    [SerializeField]
    private Transform _absoluteForward;
    [SerializeField]
    private GameObject _player;
    [SerializeField]
    private float _gravity;

    private CharacterController _c;


    private Vector3 _movement;
    private Vector3 _velocity = Vector3.zero;

    private float _horizInput;
    private float _vertInput;
    private float _rotationY;

    private bool _turning;
 

    void Start ()
    {
        _c = GetComponent<CharacterController>();
    }


	void Update ()
    {
        HandleInput();
        Turning();
        ForwardVelocity();
        MaxSpeed();
        ApplyGravity();
        
        _c.Move(_velocity);
    }
    void HandleInput()
    {

        _horizInput = Input.GetAxis("Horizontal");
        _vertInput = Input.GetAxis("Vertical");
        _movement = new Vector3(_horizInput, _vertInput, 0);
    }
    void ForwardVelocity()
    {
        if(_c.isGrounded)
        {
            _velocity += transform.forward * _acceleration * Time.deltaTime;
        }
    }
    void Turning()
    {
        _rotationY = Mathf.Clamp(_rotationY, -0.5f, 0.5f);

        if (Mathf.Abs(_horizInput) > 0.1f)
        {
            _turning = true;
            _rotationY += _turnSpeed * _horizInput * Time.deltaTime;
            transform.Rotate(Vector3.up, _rotationY);
        }

        if (Mathf.Abs(_horizInput) < 0.1f)
        {
            _rotationY = 0;
            _turning = false;
        }
    }
    void MaxSpeed()
    {
        Vector3 yVelocity = Vector3.Scale(_velocity, new Vector3(0, 1, 0));

        Vector3 xzVelocity = Vector3.Scale(_velocity, new Vector3(1, 0, 1));
        Vector3 clampedXzVelocity = Vector3.ClampMagnitude(xzVelocity, _maxSpeed);

        _velocity = yVelocity + clampedXzVelocity;
    }
    void ApplyGravity()
    {
        if(!_c.isGrounded)
        _velocity -= Vector3.down *_gravity * Time.deltaTime;
    }
}
