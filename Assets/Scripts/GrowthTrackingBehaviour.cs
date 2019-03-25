using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrowthTrackingBehaviour : MonoBehaviour {

    public GameObject[] _Animals;
    private int _animalIndex = 1;
    public int _growthStage = 0;

    public bool _playerDead;

	void Update ()
    {
        SpawnAnimal();
    }

    void SpawnAnimal()
    {
        if(_growthStage >= 4 && _playerDead)
        {
            Debug.Log("Spawned");
            Instantiate(_Animals[_animalIndex]);
            _animalIndex++;
            Debug.Log(_animalIndex);
            _growthStage = 0;
            _playerDead = false;           
        }
    }
}
