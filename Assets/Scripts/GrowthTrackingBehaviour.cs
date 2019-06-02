using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrowthTrackingBehaviour : MonoBehaviour {

    public GameObject[] _Animals;
    public int _animalIndex = 0;
    public int _growthStage;

    public bool _playerDead;
    [SerializeField] private Transform _spawnLocation;
    [SerializeField] private GameObject _cam;
    void Update ()
    {
        SpawnAnimal();
    }

    void SpawnAnimal()
    {
        if(_growthStage >= 4 && _playerDead)
        {
            _animalIndex++;
            Instantiate(_Animals[_animalIndex],_spawnLocation.position,transform.rotation);
            _cam.transform.position = _spawnLocation.position;
            _growthStage = 0;
            _playerDead = false;           
        }
    }
}
