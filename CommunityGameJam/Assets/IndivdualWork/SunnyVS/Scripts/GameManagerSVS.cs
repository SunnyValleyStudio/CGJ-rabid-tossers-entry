using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SVSGame
{
    public enum EnemyType
    {
        Sheep=0,
        Dog=1
    }
    public class GameManagerSVS : MonoBehaviour
    {
       
        public static GameManagerSVS instance;
        Level level;

        private void Awake()
        {
            if (instance == null)
            {
                instance = this;
            }
            else if (instance != this)
            {
                Destroy(gameObject);
            }
        }

        private void Start()
        {
            level = FindObjectOfType<Level>();
        }

        public void Killed(EnemyType enemyType)
        {
            level.Killed((int)enemyType);
        }

        public void LoseLift()
        {
            level.LoseLife();
        }

        public void FightOn(float timeToGo)
        {
            Debug.Log("Need UI fighting connection");
        }

        public void UpdateFightingMenu(float remainingTime, float accuracy)
        {
            Debug.Log("Need to update accuracy and time remaining");
        }

        public void FightOff()
        {
            Debug.Log("Hide fighting ui");
        }
    }
}

