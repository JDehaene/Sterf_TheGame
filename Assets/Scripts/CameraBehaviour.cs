using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBehaviour : MonoBehaviour
{
    public Transform TargetPos;
    public Transform PlayerPos;
    public float SmoothingSpeed = 0.4f;

    private Vector3 _cameraMovingVelocity = Vector3.zero;


    void Update()
    {
        SmoothFollowTarget();
    }

    private void SmoothFollowTarget()
    {
        this.transform.position = Vector3.SmoothDamp(transform.position,TargetPos.position, ref _cameraMovingVelocity,0.1f,SmoothingSpeed);

        transform.LookAt(PlayerPos);
    }
}
