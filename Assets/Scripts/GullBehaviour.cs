using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GullBehaviour : MonoBehaviour {

    [SerializeField]
    private float _acceleration;
    [SerializeField]
    private float _heightSpeed;
    [SerializeField]
    private float _turnSpeed;
    [SerializeField]
    private float _maxSpeed;
    [SerializeField]
    private Transform _absoluteForward;
    [SerializeField]
    private GameObject _player;

    private CharacterController _c;


    private Vector3 _movement;
    private Vector3 _velocity = Vector3.zero;

    private float _horizInput;
    private float _vertInput; 
    private float _turnSpeedIncrease;   
    private float _rotationY;
    private float _rotationZ;
    private Quaternion _currentYRotation;
    private Quaternion _prevRotation;

    private bool _turning;
    private bool _movingUp;
    private bool _movingDown;
    

    void Start ()
    {
        _c = GetComponent<CharacterController>();
	}

	void Update ()
    {
        _currentYRotation = this.transform.rotation;

        HandleInput();
        ForwardVelocity();
        MaxFlyingSpeed();
        Turning();
        AscendDescend();
        Dive();

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
        _velocity += transform.forward * _acceleration * Time.deltaTime;
    
    }
    void MaxFlyingSpeed()
    {
        Vector3 yVelocity = Vector3.Scale(_velocity, new Vector3(0, 1, 0));

        Vector3 xzVelocity = Vector3.Scale(_velocity, new Vector3(1, 0, 1));
        Vector3 clampedXzVelocity = Vector3.ClampMagnitude(xzVelocity, _maxSpeed);

        _velocity = yVelocity + clampedXzVelocity;
    }
    void Turning()
    {
        _rotationY = Mathf.Clamp(_rotationY, -0.5f,0.5f);

        if (Mathf.Abs(_horizInput) > 0.1f)
        {
            _turning = true;
            _rotationY += _turnSpeed*_horizInput * Time.deltaTime;
            _velocity.x += _horizInput * Time.deltaTime;
            transform.Rotate(Vector3.up, _rotationY);
        }

        if (Mathf.Abs(_horizInput) < 0.1f)
        {
            _rotationY = 0;
            _turning = false;
        }
            
        
    }
    void AscendDescend()
    {
        _rotationZ = Mathf.Clamp(_rotationZ, -0.5f, 0.5f);
        
        if (Mathf.Abs(_vertInput) > 0.1f && _turning == false)
        {
            Debug.Log("Movin on down/up");
            _velocity.y += _heightSpeed*_movement.y * Time.deltaTime;
        }
        if(_turning == false && Mathf.Abs(_vertInput) < 0.1f)
        {
            _prevRotation = _currentYRotation;
            _velocity.y = 0;
            transform.rotation = Quaternion.Lerp(transform.rotation,_prevRotation, 1);
        }
    }
    
    void Dive()
    {

    }
}

