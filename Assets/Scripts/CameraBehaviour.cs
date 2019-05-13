using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBehaviour : MonoBehaviour
{
    private float _horizontalL, _verticalL, _horizontalR, _verticalR, _swimForward, _resetCamera;

    private GameObject _targetPlayer;
    private bool _horizontalOrbit;
    private bool _verticalOrbit;
    [SerializeField] private float _smoothingSpeed;
    [SerializeField] private float _smoothTime;
    [SerializeField] private int _rotatingSpeed;
    [SerializeField] private float _maxDistance;
    [SerializeField] private float _resetCamSpeed;
    [SerializeField] private float _resetCameraTimer;
    [SerializeField] private Vector2 _clampValues = new Vector2(-70, 70);
    private float _rotationX;
    private Quaternion _targetRotation;

    private bool _isTurning = false;
    public bool _isMoving;
    public bool _isTurningAround;


    private Vector3 _cameraMovingVelocity = Vector3.zero;
    public bool _resetCameraBool = false;


    [SerializeField] private Transform target;
    [SerializeField] private float bumperDistanceCheck = 2.5f; // length of bumper ray
    [SerializeField] private Vector3 _bumpOffset;
    [SerializeField] private float _dampSpeed;


    void Update()
    {
        _targetPlayer = GameObject.FindGameObjectWithTag("Player");
        _targetRotation = Quaternion.Euler(_targetPlayer.transform.eulerAngles.x, _targetPlayer.transform.eulerAngles.y, 0);
        
        HandleInputs();
        SmoothFollowAlong();     
        TurnCamera();
        SmoothReset();
        ResetCamera();
    }

    void HandleInputs()
    {
        _horizontalL = Input.GetAxis("HorizontalL");
        _horizontalR = Input.GetAxis("HorizontalR");
        _verticalR = Input.GetAxis("VerticalR");
        _resetCamera = Input.GetAxis("ResetCamera");

        if (_isTurning == false && _isTurningAround == false && _isMoving == false)
            _resetCameraTimer -= Time.deltaTime;
        else
            _resetCameraTimer = 3;


    }

    private void SmoothFollowAlong()
    {
        if (Vector3.Distance(transform.position, _targetPlayer.transform.position) > _maxDistance)
        {
            this.transform.position = Vector3.SmoothDamp(transform.position, _targetPlayer.transform.position, ref _cameraMovingVelocity, _smoothTime, _smoothingSpeed);
        }
    }
    private void ResetCamera()
    {
        float _currentAngleX = transform.eulerAngles.x;
        float _currentAngleY = transform.eulerAngles.y;

        float _currentPlayerX = _targetPlayer.transform.eulerAngles.x;
        float _currentPlayerY = _targetPlayer.transform.eulerAngles.y;

        if (_resetCamera < -0.1f || _resetCameraBool || Mathf.Abs(_horizontalL) > 0.1f)
        {
            float resetTime = _resetCamSpeed * Time.deltaTime;
            transform.rotation = Quaternion.Slerp(transform.rotation, _targetRotation, resetTime);
            if (resetTime >= 0.9f)
            {
                _resetCameraBool = false;
                _resetCameraTimer = 3;
            }
        }

    }
    private void SmoothReset()
    {
        if (_resetCameraTimer <= 0)
        {
            _resetCameraBool = true;
        }
        else
            _resetCameraBool = false;
    }
    private void TurnCamera()
    {     
        float _rotationY = _horizontalR * _rotatingSpeed * Time.deltaTime;

        if (Mathf.Abs(_verticalR) > 0.01f || Mathf.Abs(_rotationY) > 0.01f)
            _isTurning = true;
        else
            _isTurning = false;


        _rotationX += _verticalR * _rotatingSpeed * Time.deltaTime;
        float _clampRotationX = Mathf.Clamp(_rotationX, _clampValues.x, _clampValues.y);

        transform.rotation = Quaternion.Euler(_clampRotationX, transform.eulerAngles.y, 0);
        transform.rotation *= Quaternion.AngleAxis(_rotationY, Vector3.up);
        transform.eulerAngles -= new Vector3(0, 0, transform.eulerAngles.z);

    }
}
