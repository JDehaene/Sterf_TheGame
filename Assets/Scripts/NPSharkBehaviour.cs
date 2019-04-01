﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPSharkBehaviour : MonoBehaviour
{

    public GrowthTrackingBehaviour Growth;
    private float _timeUntilDeath = 2;
    [SerializeField]
    private GameObject _player;
    private float _speed = 2f;
    private float _maxDetectionDistance = 5;

    private void Update()
    {
        Growth = (GrowthTrackingBehaviour)FindObjectOfType(typeof(GrowthTrackingBehaviour));
        if(Growth._animalIndex == 2)
        {
        _player = GameObject.Find("Zeehond(Clone)");
        FindPlayer();
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Seal")
        {
            KillPlayer(other.gameObject);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        _timeUntilDeath = 2;
    }
    void KillPlayer(GameObject food)
    {
        _timeUntilDeath -= Time.deltaTime;

        if (_timeUntilDeath <= 0)
        {
            Debug.Log("Ded");
            Growth._playerDead = true;
            Destroy(food);
        }
    }
    void FindPlayer()
    {
        if (Vector3.Distance(_player.transform.position, transform.position) < _maxDetectionDistance && _player.transform.tag == "Seal")
        {
            Debug.Log("In range");
            transform.position = Vector3.MoveTowards(transform.position, _player.transform.position, _speed * Time.deltaTime);
            transform.LookAt(_player.transform);
        }
    }
}