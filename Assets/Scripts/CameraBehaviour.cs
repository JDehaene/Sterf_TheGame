using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBehaviour : MonoBehaviour
{
    public GameObject TargetPos;
    public GameObject PlayerPos;
    public float SmoothingSpeed = 0.4f;

    private Vector3 _cameraMovingVelocity = Vector3.zero;


    void Update()
    {
        TargetPos = GameObject.Find("TargetPos");
        PlayerPos = GameObject.Find("PlayerPos");
        SmoothFollowTarget();
    }

    private void SmoothFollowTarget()
    {
        this.transform.position = Vector3.SmoothDamp(transform.position,TargetPos.transform.position, ref _cameraMovingVelocity,0.1f,SmoothingSpeed);

        transform.LookAt(PlayerPos.transform);
    }
}
