using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

[System.Serializable]
public class Objective
{
    // how many to kill
    public int startingNumber;

    // corresponding UI text
    public TextMeshProUGUI text;
}

public class Level : MonoBehaviour
{
    // the number of lives at the beginning;
    public int startingLives;

    // image showing number of lives left
    public Image livesImage;

    // width of one wolf on the image
    public float wolfImageWidth = 100;

    // Pause script
    public Pause pause;

    // objectives (sheep, dogs, shepherds)
    public Objective[] objectives = new Objective[3]; 

    private void Awake()
    {
        // set number of lives
        Static.lives = startingLives;

        // set number of kills
        for (int i = 0; i < objectives.Length; i++)
        {
            Static.kills[i] = objectives[i].startingNumber;
        }
    }

    void Start()
    {
        // update the UI with values
        UpdateUI();
    }

    void Update()
    {
        // debug input
        /*
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            LoseLife();
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            Killed(0);
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            Killed(1);
        }
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            Killed(2);
        }*/
    }

    // update the number of kills left and lives left on the UI
    private void UpdateUI()
    {
        // the lives left image is tiled, so setting the width determines the number of wolves shown 
        livesImage.rectTransform.sizeDelta = new Vector2(Static.lives * wolfImageWidth, livesImage.rectTransform.sizeDelta.y);

        // for every objective set its number of kills left
        for (int i=0; i<objectives.Length; i++)
        {
            if(Static.kills[i] != 0)
            {
                objectives[i].text.text = "x" + Static.kills[i];
            }
            else
            {
                objectives[i].text.text = "";
            }
        }
    }

    // when you die
    public void LoseLife()
    {
        // lose one life
        Static.lives -= 1;

        // update the UI with values
        UpdateUI();

        // die if no lives left
        if (Static.lives <= 0)
        {
            pause.Lost();
        }
    }

    // when you kill something
    // which - determines which objective you killed, so i.e. 0 for sheep and 1 for dogs
    public void Killed(int which)
    {
        // add a kill if able
        if(Static.kills[which] > 0)
        {
            Static.kills[which] -= 1;
        }

        // update the UI with values
        UpdateUI();

        // win if no kills left
        bool won = true;
        foreach(int number in Static.kills)
        {
            if(number != 0)
            {
                won = false;
            }
        }
        if (won)
        {
            pause.Won();
        }
    }
}
