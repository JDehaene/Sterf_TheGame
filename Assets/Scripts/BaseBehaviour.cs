using System.Collections;
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

    [SerializeField] private string _foodName;
    [SerializeField] private float _horizRotationSpeed;
    [SerializeField] private float _vertRotationSpeed;
    [SerializeField] private float _dragOnGround;
    [SerializeField] private float _turnSpeedLimiter;
    [SerializeField] private float _resetRotation;
    [SerializeField] private float _resetRotationSpeed;
    [SerializeField] private float _collisionDistance;
    [SerializeField] private float _gravity;
    [SerializeField] private bool _affectedByGravity;
    [SerializeField] private float _camPosOffset;

    void Start()
    {
        _c = GetComponent<CharacterController>();
        Cam = GameObject.FindObjectOfType<CameraBehaviour>();

        CameraPlacement();
    }

    void Update()
    {
        Growth = (GrowthTrackingBehaviour)FindObjectOfType(typeof(GrowthTrackingBehaviour));

        HandleInput();
        ForwardVelocity();
        Turning();
        AscendDescend();
        MaxSwimnmingSpeed();
        ApplyGravity();

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
    void MaxSwimnmingSpeed()
    {
        Vector3 xyzVelocity = Vector3.Scale(_velocity, new Vector3(1, 1, 1));


        Vector3 clampedXyzVelocity = Vector3.ClampMagnitude(xyzVelocity, _maxSpeed);

        _velocity = clampedXyzVelocity;
    }
    void Turning()
    {
        if (Mathf.Abs(_horizInputL) > 0.1f)
        {
            CheckDirection();
            transform.eulerAngles += new Vector3(0, _horizInputL * _horizRotationSpeed * Time.deltaTime, 0);
            _velocity += transform.forward * _acceleration * _turnSpeedLimiter * Time.deltaTime;
        }
    }
    void AscendDescend()
    {
        if (Mathf.Abs(_vertInputL) > 0.9f)
        {
            float angle = _vertInputL * _vertRotationSpeed * Time.deltaTime;
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
        if (other.gameObject.tag == _foodName)
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

    void ApplyGravity()
    {
        if (!_c.isGrounded && _affectedByGravity)
            _velocity -= Vector3.down * _gravity * Time.deltaTime;
    }

    void CameraPlacement()
    {
        float _camposZ = Camera.main.transform.localPosition.z;

        Camera.main.transform.localPosition = new Vector3(0, 0, _camposZ * _camPosOffset);
    }
}


