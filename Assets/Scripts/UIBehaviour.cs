using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIBehaviour : MonoBehaviour {

    public GrowthTrackingBehaviour Growth;
    [SerializeField]
    private Text _growthTracker;
    [SerializeField]
    private Image _animalHuntIcon;
    [SerializeField]
    private Image _huntingAnimalIcon;
    public Sprite[] _animalToHunt;
	
	
	void Update ()
    {
        UpdateGrowthTracker();
        UpdateHuntAnimal();
        UpdateHuntingAnimal();
	}
    void UpdateGrowthTracker()
    {
        _growthTracker.text =  Growth._growthStage + "/4";        
    }
    void UpdateHuntAnimal()
    {
        _animalHuntIcon.sprite = _animalToHunt[Growth._animalIndex - 1];
    }
    void UpdateHuntingAnimal()
    {
       _huntingAnimalIcon.sprite = _animalToHunt[Growth._animalIndex  + 1];
    }
}
