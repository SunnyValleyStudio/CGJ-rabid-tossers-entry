using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Bulb : MonoBehaviour
{

    public Image lightImage;

    void Start()
    {
        lightImage.enabled = false;
    }

    void Update()
    {
        
    }

    public void TurnOn()
    {
        lightImage.enabled = true;
    }

    public void TurnOff()
    {
        lightImage.enabled = false;
    }
}
