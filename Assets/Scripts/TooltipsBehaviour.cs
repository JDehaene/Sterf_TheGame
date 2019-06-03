using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TooltipsBehaviour : MonoBehaviour {

    private bool _toonKeer = false;
    public GameObject Tooltip;

	void Update () {
        if (_toonKeer == true)
        {
            Tooltip.SetActive(true);
        }
        else
        {
            Tooltip.SetActive(false);
        }
	}

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            _toonKeer = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            _toonKeer = false;
        }
    }
}
