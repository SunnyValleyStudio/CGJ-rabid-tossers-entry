using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Static
{
    // if the game is currently paused
    public static bool paused;

    // number of lives left
    public static int lives;

    // number of kills left
    public static int[] kills = new int[3];
}
