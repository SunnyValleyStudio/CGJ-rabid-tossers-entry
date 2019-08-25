using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProgressBar : MonoBehaviour
{
    public GameObject bulbPrefab; // the prefabs to spawn
    public Transform parent; // the parent for the bulbs

    public int number; // how many lights there are

    public float bulbWidth; // how wide one bulb is

    GameObject[] bulbs;
    Bulb[] scripts;

    public int progress; // how many bulbs are on

    void Start()
    { 
        bulbs = new GameObject[number];
        scripts = new Bulb[number];

        // spawn the bulbs
        for(int i=0; i<number; i++)
        {
            bulbs[i] = Instantiate(bulbPrefab, parent);
            bulbs[i].transform.localPosition = new Vector2(parent.position.x + i * bulbWidth, parent.position.y);

            scripts[i] = bulbs[i].GetComponent<Bulb>();
        }
    }
    
    void Update()
    {
        //ChangeProgress(0);
    }

    // a change to progress, changing the amount of lit bulbs
    public void ChangeProgress(int amount)
    {
        // apply the change
        progress += amount;

        // update the bulbs
        for(int i = 0; i < number; i++)
        {
            if(i < progress)
            {
                scripts[i].TurnOn();
            }
            else
            {
                scripts[i].TurnOff();
            }
        }
    }
}
