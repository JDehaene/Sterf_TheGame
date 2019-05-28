using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPFishBehaviour : MonoBehaviour
{

    public GrowthTrackingBehaviour Growth;
    private float _timeUntilDeath = 0.1f;
    [SerializeField]
    private GameObject _player;
    private float _speed = 1f;
    [SerializeField]
    private float _maxDetectionDistance;

    public WaypointBehaviour WP;

    private void Update()
    {
        Growth = (GrowthTrackingBehaviour)FindObjectOfType(typeof(GrowthTrackingBehaviour));
        if (Growth._animalIndex == 0)
        {
            _player = GameObject.Find("Garnaal");
            FindPlayer();
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.layer == 9 && Growth._growthStage >= 4)
        {
            KillPlayer(other.gameObject);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        _timeUntilDeath = 0.1f;
    }
    void KillPlayer(GameObject food)
    {
        _timeUntilDeath -= Time.deltaTime;

        if (_timeUntilDeath <= 0)
        {
            Growth._playerDead = true;
            Destroy(food);
        }
    }
    void FindPlayer()
    {
        if (Growth._growthStage >= 4 && Vector3.Distance(_player.transform.position,transform.position) < _maxDetectionDistance && _player.layer == 9)
        {
            transform.position = Vector3.MoveTowards(transform.position, _player.transform.position, _speed * Time.deltaTime);
            transform.LookAt(_player.transform);
        }
    }
}
