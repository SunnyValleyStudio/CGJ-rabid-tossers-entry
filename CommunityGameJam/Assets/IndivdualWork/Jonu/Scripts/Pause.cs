using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Pause : MonoBehaviour
{
    // keycode used to pause / unpause
    public KeyCode pauseKey = KeyCode.Escape;

    // pause panel's script
    public Panel pausePanel;

    // win panel
    public Panel winPanel;

    // lose panel
    public Panel losePanel;

    // if the game was started (main menu is hidden)
    bool started = false;

    void Start()
    {
        // pause the game at the beginning
        Static.paused = true;
    }
    
    void Update()
    {
        // when the key is pressed and game is started
        if (Input.GetKeyDown(pauseKey) && started)
        {
            // if the game is not paused
            if (!Static.paused)
            {
                // show the pause panel
                pausePanel.Enter();
            }
            // if the game is paused
            else
            {
                //hide the pause panel
                pausePanel.Exit();
            }

            // change the paused status
            Static.paused = !Static.paused;
        }
    }

    // used by "Begin" button on main menu panel
    public void Begin()
    {
        // set as started
        started = true;

        // unpause the game
        Static.paused = false;
    }

    // restart the game / return to menu
    public void Restart()
    {
        // load the active scene
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    // when the game is won
    public void Won()
    {
        winPanel.Enter();


        Static.paused = true;
    }

    // when the game is lost
    public void Lost()
    {
        losePanel.Enter();

        Static.paused = true;
    }
}
