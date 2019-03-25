using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPFishBehaviour : MonoBehaviour
{

    public GrowthTrackingBehaviour Growth;
    private float _timeUntilDeath = 2;

    void Start()
    {

    }
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Shrimp")
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
            Growth._playerDead = true;
            Destroy(food);
        }
    }
}
