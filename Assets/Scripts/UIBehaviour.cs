using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIBehaviour : MonoBehaviour
{

    public GrowthTrackingBehaviour Growth;
    [SerializeField]
    private Image _animalHuntIcon;
    public Sprite[] _animalToHunt;


    void Update()
    {
        UpdateAnimalIcon();
    }
    void UpdateAnimalIcon()
    {
        if (Growth._growthStage <= 3)
            _animalHuntIcon.sprite = _animalToHunt[Growth._animalIndex];
        if (Growth._growthStage == 4)
            _animalHuntIcon.sprite = _animalToHunt[Growth._animalIndex + 2];
    }
}
