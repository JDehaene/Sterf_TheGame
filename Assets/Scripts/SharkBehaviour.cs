using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SharkBehaviour : MonoBehaviour
{

    [SerializeField]
    private float _acceleration;
    [SerializeField]
    private float _heightSpeed;
    [SerializeField]
    private float _turnSpeed;
    [SerializeField]
    private float _maxSpeed;
    [SerializeField]
    private GameObject _player;

    private CharacterController _c;
    public GrowthTrackingBehaviour Growth;

    private Vector3 _movement;
    private Vector3 _velocity = Vector3.zero;

    private float _horizInputL;
    private float _vertInputR;
    private float _vertInputL;
    private float _turnSpeedIncrease;
    private float _rotationY;
    private float _rotationZ;

    private Quaternion _currentYRotation;
    private Quaternion _prevRotation;

    private bool _turning;
    [SerializeField]
    private float _dragOnGround;

    void Start()
    {
        _c = GetComponent<CharacterController>();
    }

    void Update()
    {
        _currentYRotation = this.transform.rotation;

        HandleInput();
        ForwardVelocity();
        MaxFlyingSpeed();
        Turning();
        AscendDescend();

        _c.Move(_velocity);
    }

    void HandleInput()
    {

        _horizInputL = Input.GetAxis("HorizontalL");
        _vertInputR = Input.GetAxis("VerticalR");
        _vertInputL = Input.GetAxis("VerticalL");
        _movement = new Vector3(_horizInputL, _vertInputR, 0);
    }
    void ForwardVelocity()
    {
        if (_vertInputL >= 1)
            _velocity += transform.forward * _acceleration * Mathf.Abs(_vertInputL) * Time.deltaTime;
        else
            _velocity = _velocity * (1 - 0.1f * _dragOnGround);

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
    void AscendDescend()
    {
        _rotationZ = Mathf.Clamp(_rotationZ, -0.5f, 0.5f);

        if (Mathf.Abs(_vertInputR) > 0.1f && _turning == false)
        {

            _velocity.y += _heightSpeed * _movement.y * Time.deltaTime;
        }
        if (_turning == false && Mathf.Abs(_vertInputR) < 0.1f)
        {
            _prevRotation = _currentYRotation;
            _velocity.y = 0;
            transform.rotation = Quaternion.Lerp(transform.rotation, _prevRotation, 1);
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (Input.GetButtonDown("Eat") && other.gameObject.tag == "Seal")
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
        if (Growth._growthStage < 4)
        {
            transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
        }
        else
        {
            transform.localScale = new Vector3(1f, 1f, 1f);
        }
    }
}

