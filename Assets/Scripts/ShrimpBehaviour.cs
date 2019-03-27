﻿using System.Collections;
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
    private GameObject _player;
    [SerializeField]
    private float _gravity;
    [SerializeField]
    private float _dragOnGround;

    private CharacterController _c;
    public GrowthTrackingBehaviour Growth;


    private Vector3 _movement;
    private Vector3 _velocity = Vector3.zero;

    private bool _eatInput;
    private float _horizInputL;
    private float _vertInputR;
    private float _vertInputL;
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
        Grow();
        
        _c.Move(_velocity);
    }
    void HandleInput()
    {
        _eatInput = Input.GetButtonDown("Eat");
        _horizInputL = Input.GetAxis("HorizontalL");
        _vertInputR = Input.GetAxis("VerticalR");
        _vertInputL = Input.GetAxis("VerticalL");
        _movement = new Vector3(_horizInputL, _vertInputR, 0);
    }
    void ForwardVelocity()
    {
        if(_vertInputL >= 1)        
            _velocity += transform.forward * _acceleration * Mathf.Abs(_vertInputL) * Time.deltaTime;        
        else
            _velocity = _velocity * (1 - 0.1f * _dragOnGround);
        
    }
    void Turning()
    {
        _rotationY = Mathf.Clamp(_rotationY, -0.5f, 0.5f);

        if (Mathf.Abs(_horizInputL) > 0.1f)
        {
            _turning = true;
            _rotationY += _turnSpeed * _horizInputL * Time.deltaTime;
            transform.Rotate(Vector3.up, _rotationY);
        }

        if (Mathf.Abs(_horizInputL) < 0.1f)
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
    private void OnTriggerStay(Collider other)
    {
        if (Input.GetButtonDown("Eat") && other.gameObject.tag == "Plankton")
        {
            Eat(other.gameObject);
        }
            
    }
    void Eat(GameObject food)
    {
        Destroy(food);
        Growth._growthStage++;
        Debug.Log(Growth._growthStage); 
    }
    void Grow()
    {
       if(Growth._growthStage < 4)
        {
            transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
        }
       else
        {
            transform.localScale = new Vector3(1f, 1f, 1f);
        }
    }
}