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
        AudioSource listener;      
        public static GameManagerSVS instance;
        Level level;
        public AudioClip[] clips;
        int currentCLipIndex = -1;
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
            listener = GetComponent<AudioSource>();
        }

        private void Start()
        {
            level = FindObjectOfType<Level>();
            PlaySound();
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

        private void PlaySound()
        {
            if (clips.Length > 0)
            {
                if (currentCLipIndex < 0)
                {
                    currentCLipIndex = 0;

                }
                currentCLipIndex++;
                if (currentCLipIndex >= clips.Length)
                {
                    currentCLipIndex = 0;
                }
                else
                {
                    
                }
                listener.clip = clips[currentCLipIndex];
                listener.Play();
                Invoke("PlaySound", listener.clip.length);
            }
            
        }

        public void StopSound()
        {
            listener.Pause();
        }

        public void RestartSound()
        {
            listener.UnPause();
        }

        


    }
}

