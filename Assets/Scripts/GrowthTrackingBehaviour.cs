using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrowthTrackingBehaviour : MonoBehaviour {

    public GameObject[] _Animals;
    public int _animalIndex = 0;
    public int _growthStage;

    public bool _playerDead;
    [SerializeField]
    private Transform _spawnLocation;

    void Update ()
    {
        Debug.Log(_animalIndex);
        SpawnAnimal();
    }

    void SpawnAnimal()
    {
        if(_growthStage >= 4 && _playerDead)
        {
            _animalIndex++;
            Instantiate(_Animals[_animalIndex]);
            
            _growthStage = 0;
            _playerDead = false;           
        }
    }
}
