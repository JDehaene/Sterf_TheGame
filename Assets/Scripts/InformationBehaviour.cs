using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InformationBehaviour : MonoBehaviour
{

    [SerializeField] private GameObject _infoObject;

    private float _distance = 0.0f;
	
	void Update ()
    {
        //CheckObjectsUpdate();
        _distance = Vector3.Distance(transform.position, _infoObject.transform.position);
        Debug.Log("Distance is: " + _distance);
	}

    private void CheckObjectsUpdate()
    {
        CalculatedDistance();

        if(CalculatedDistance() <= 50.0f)
        {
            Debug.Log("In Distance!");
        }

    }

    private float CalculatedDistance()
    {
        _distance = Vector3.Distance(_infoObject.transform.position, this.transform.position);
        return _distance;
    }
}
