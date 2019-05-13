﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseBehaviour : MonoBehaviour
{


    private CharacterController _c;
    public GrowthTrackingBehaviour Growth;
    public CameraBehaviour Cam;
    private Vector3 _velocity = Vector3.zero;

    private float _horizInputL;
    private float _vertInputL;
    private float _swimForward;
    private float _deflectorValue;

    [SerializeField] private GameObject _player;
    [SerializeField] private float _acceleration;
    [SerializeField] private float _heightSpeed;
    [SerializeField] private float _maxSpeed;
    [SerializeField] private float _swimmingAnimSpeed;

    [SerializeField] private float _rotationSpeed;
    [SerializeField] private float _dragOnGround;
    [SerializeField] private float _turnSpeedLimiter;
    [SerializeField] private float _resetRotation;
    [SerializeField] private float _resetRotationSpeed;
    [SerializeField] private float _collisionDistance;


    void Start()
    {
        _c = GetComponent<CharacterController>();
        Cam = GameObject.FindObjectOfType<CameraBehaviour>();
    }

    void Update()
    {
        Growth = (GrowthTrackingBehaviour)FindObjectOfType(typeof(GrowthTrackingBehaviour));

        HandleInput();
        ForwardVelocity();
        Turning();
        AscendDescend();
        MaxFlyingSpeed();


        _c.Move(_velocity);
        CheckSituation();
        CollisionDetector();
    }

    void HandleInput()
    {
        _horizInputL = Input.GetAxis("HorizontalL");
        _vertInputL = Input.GetAxis("VerticalL");
        _swimForward = Input.GetAxis("SwimForward");
    }


    void ForwardVelocity()
    {
        if (_swimForward >= 1)
        {
            _velocity += transform.forward * _acceleration * _swimForward * Time.deltaTime;
        }
        else
        {
            _velocity = _velocity * (1 - 0.1f * _dragOnGround);
        }

    }
    void MaxFlyingSpeed()
    {
        //Vector3 yVelocity = Vector3.Scale(_velocity, new Vector3(0, , 0));
        Vector3 xyzVelocity = Vector3.Scale(_velocity, new Vector3(1, 1, 1));


        Vector3 clampedXyzVelocity = Vector3.ClampMagnitude(xyzVelocity, _maxSpeed);
        //Vector3 clampedYVelocity = Vector3.ClampMagnitude(yVelocity, _heightSpeed);

        _velocity = clampedXyzVelocity;
    }
    void Turning()
    {
        if (Mathf.Abs(_horizInputL) > 0.1f)
        {
            CheckDirection();
            transform.eulerAngles += new Vector3(0, _horizInputL * _rotationSpeed * Time.deltaTime, 0);
            _velocity += transform.forward * _acceleration * _turnSpeedLimiter * Time.deltaTime;
        }
    }
    void AscendDescend()
    {
        if (Mathf.Abs(_vertInputL) > 0.9f)
        {
            float angle = _vertInputL * _rotationSpeed * Time.deltaTime;
            transform.rotation *= Quaternion.AngleAxis(angle, Vector3.left);
        }
    }
    void CheckSituation()
    {
        if (Mathf.Abs(_horizInputL) < 0.1f || Mathf.Abs(_vertInputL) < 0.1f)
            Cam._isTurningAround = false;
        else
            Cam._isTurningAround = true;

        if (_velocity.magnitude <= 0.3f)
            Cam._isMoving = false;
        else
            Cam._isMoving = true;
    }
    void CheckDirection()
    {
        if (_horizInputL < 0.5f)
            _deflectorValue = -5;
        if (_horizInputL > 0.5f)
            _deflectorValue = 5;

        Debug.Log(_deflectorValue);
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Shrimp")
        {
            Eat(other.gameObject);
        }

    }
    void Eat(GameObject food)
    {
        Destroy(food);
        Growth._growthStage++;
    }
    void CollisionReset()
    {
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(_resetRotation, transform.eulerAngles.y + _deflectorValue, 0), _resetRotationSpeed * Time.deltaTime);
    }
    void CollisionDetector()
    {
        if (Physics.Raycast(transform.position, transform.forward, _collisionDistance))
            CollisionReset();
    }

}


