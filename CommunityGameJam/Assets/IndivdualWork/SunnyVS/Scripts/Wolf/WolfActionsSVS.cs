using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SVSFlocking;
using SVSGuards;
using SVSAI;
using SVSPredictor;
using SVSInput;
using SVSGame;

namespace SVSWolf
{
    public class WolfActionsSVS : MonoBehaviour, IAttackableSVS
    {
        Vector3 startingPosition;
        public LayerMask sheepMask;
        public LayerMask wolfLair;
        public float wolfRadius = 3f;
        public float breakUpDistance = 10f;
        public float sheepSpeed = 6f;
        public GameObject sheepFollowing = null;
        public Flock3dSVS flock;
        public GuardAISVS followingGuard;
        public bool isFighting = false;
        public KeyCode KeyForFighting1 = KeyCode.Z;
        public KeyCode KeyForFighting2 = KeyCode.X;
        InputPredictorSVS predictionInputSystem;
        Vector3 lastPrediction = Vector3.zero;
        float timeFromLastClick = 0;
        public float maxInputDelay = 3f;
        public float fightTime = 5.0f;
        float currentFightTime = 0;
        float accuracy = 50f;
        Vector3 currentKeyVector = Vector3.zero;
        [Tooltip("Acc from small amount of good bad guesses vary very much 50%->30% -> 80%. If we preset negative and positive answers higt mean change will be around 50%")]
        public int startingOrcleScoreValues = 50;
        int positiveGuesses = 0;
        int negativeGuesses = 0;
        [Range(0, 1f)]
        public float algorithmAccuracyLimitToWin = 0.6f;
        IAttackableSVS currentlyAttackedUnit = null;
        bool fightingSheep = false;
        [Range(1f, 4f)]
        public float maxDistanceToAttack = 4f;
        public bool isTired;
        [Tooltip("How long before wolf can fight again")]
        public float tiredStateLength = 3f;
        float waitedAfterWasTiredTime = 0;


        // Start is called before the first frame update
        void Start()
        {
            predictionInputSystem = GetComponent<InputPredictorSVS>();
            predictionInputSystem.PrepareThePredictorClass(Vector3.forward);
            startingPosition = transform.position;
            positiveGuesses = startingOrcleScoreValues;
            negativeGuesses = startingOrcleScoreValues;
        }

        // Update is called once per frame
        void Update()
        {
            if (Static.paused==false)
            {
                if (isTired)
                {
                    waitedAfterWasTiredTime += Time.deltaTime;
                    if (waitedAfterWasTiredTime >= tiredStateLength)
                    {
                        isTired = false;
                        waitedAfterWasTiredTime = 0;
                    }
                }

                if (isFighting == false)
                {
                    if (sheepFollowing == null)
                    {
                        Collider[] contextColliders = Physics.OverlapSphere(transform.position, wolfRadius, sheepMask);
                        if (contextColliders.Length > 0)
                        {
                            sheepFollowing = contextColliders[0].gameObject;
                            sheepFollowing.GetComponent<FlockAgent3dSVS>().StartFollowingWOlf(gameObject, sheepSpeed);
                        }
                    }
                    else
                    {
                        if (Vector3.Distance(sheepFollowing.transform.position, transform.position) > breakUpDistance)
                        {

                            sheepFollowing.GetComponent<FlockAgent3dSVS>().StopFollowingWolf();
                            sheepFollowing = null;


                        }
                    }
                    if (isTired == false)
                    {
                        if ((sheepFollowing != null && (Vector3.Distance(sheepFollowing.transform.position, gameObject.transform.position) < maxDistanceToAttack))
                                                || (followingGuard != null && (Vector3.Distance(followingGuard.transform.position, gameObject.transform.position) < maxDistanceToAttack))
                                                )
                        {
                            Debug.Log("YOU CAN ATTACK NOW. PRESS Z or X");

                            if (Input.GetKeyDown(KeyForFighting1))
                            {
                                lastPrediction = GetVectorFromInput(KeyForFighting1);
                            }
                            else if (Input.GetKeyDown(KeyForFighting2))
                            {
                                lastPrediction = GetVectorFromInput(KeyForFighting2);
                            }
                            if (lastPrediction.magnitude > 0.01f)
                            {
                                if (followingGuard != null)
                                {
                                    if (sheepFollowing != null)
                                    {
                                        sheepFollowing.GetComponent<FlockAgent3dSVS>().StopFollowingWolf();
                                        sheepFollowing = null;
                                    }
                                    fightingSheep = false;
                                    currentlyAttackedUnit = (IAttackableSVS)followingGuard;
                                    StartAFight();
                                }
                                else
                                {
                                    fightingSheep = true;
                                    currentlyAttackedUnit = (IAttackableSVS)sheepFollowing.GetComponent<FlockAgent3dSVS>();
                                    StartAFight();
                                }
                            }
                        }
                    }
                    

                }
                else
                {

                    if (Input.GetKeyDown(KeyForFighting1))
                    {
                        currentKeyVector = GetVectorFromInput(KeyForFighting1);
                    }
                    else if (Input.GetKeyDown(KeyForFighting2))
                    {
                        currentKeyVector = GetVectorFromInput(KeyForFighting2);
                    }
                    if (Vector3.Distance(Vector3.zero, currentKeyVector) > 0.1f)
                    {
                        timeFromLastClick = 0;
                        if (Vector3.Distance(currentKeyVector, lastPrediction) < 0.01f)
                        {
                            positiveGuesses++;
                        }
                        else
                        {
                            negativeGuesses++;
                        }
                        lastPrediction = predictionInputSystem.PredictNextInput(currentKeyVector);
                        currentKeyVector = Vector3.zero;
                        accuracy = ((float)(positiveGuesses) / (positiveGuesses + negativeGuesses));
                        Debug.Log("Algorithm accuracy: " + accuracy);
                        GameManagerSVS.instance.UpdateFightingMenu(Mathf.Clamp(currentFightTime, 0, fightTime), accuracy);
                    }
                    else
                    {
                        timeFromLastClick += Time.deltaTime;
                    }

                    if (timeFromLastClick > maxInputDelay)
                    {
                        positiveGuesses++;
                        timeFromLastClick = 0;
                        accuracy = ((float)(positiveGuesses) / (positiveGuesses + negativeGuesses));
                        Debug.Log("Algorithm accuracy: " + accuracy);
                        GameManagerSVS.instance.UpdateFightingMenu(Mathf.Clamp(currentFightTime, 0, fightTime), accuracy);
                    }

                    currentFightTime += Time.deltaTime;
                    if (currentFightTime >= fightTime)
                    {
                        currentFightTime = 0;
                        isFighting = false;
                        lastPrediction = Vector3.zero;
                        Debug.Log("END FIGHT ACCURACY: " + accuracy);
                        GameManagerSVS.instance.UpdateFightingMenu(Mathf.Clamp(currentFightTime, 0, fightTime), accuracy);
                        GameManagerSVS.instance.FightOff();
                        isTired = true;
                        waitedAfterWasTiredTime = 0;
                        if (accuracy <= algorithmAccuracyLimitToWin)
                        {
                            
                            OnWInFight();
                            
                            currentlyAttackedUnit.OnLoseFight();
                        }
                        else
                        {
                            isTired = false;
                            OnLoseFight();
                            currentlyAttackedUnit.OnWInFight();
                        }
                        currentlyAttackedUnit = null;

                    }
                }
            }

        }

        private void OnTriggerStay(Collider other)
        {
            if (sheepFollowing != null && other.gameObject.layer == Mathf.Log(wolfLair.value, 2))
            {
                sheepFollowing.SetActive(false);
                sheepFollowing = null;
                Debug.Log("Yummy!");
            }
        }

        public void SetGuardFollow(GuardAISVS guard)
        {
            if (followingGuard == null)
            {
                followingGuard = guard;
            }
        }

        public void ResetGuardFollowing()
        {
            followingGuard = null;
        }

        public void StartAFight()
        {
            GameManagerSVS.instance.FightOn(fightTime);
            if (lastPrediction == Vector3.zero)
            {
                lastPrediction = GetVectorFromInput(KeyForFighting1);
            }
            if (currentlyAttackedUnit == null)
            {
                currentlyAttackedUnit = (IAttackableSVS)followingGuard;
            }
            positiveGuesses = startingOrcleScoreValues;
            negativeGuesses = startingOrcleScoreValues;
            Debug.Log("Staring a fight");
            isFighting = true;
            GetComponent<PlayerInputSVS>().movementBlockDuringFIght = isFighting;
        }
        public void OnWInFight()
        {
            if (fightingSheep)
            {
                sheepFollowing = null;
                
            }
            else
            {
                followingGuard = null;
                
            }
            fightingSheep = false;

            GetComponent<PlayerInputSVS>().movementBlockDuringFIght = false;
        }

        public void OnLoseFight()
        {
            GetComponent<PlayerInputSVS>().movementBlockDuringFIght = false;

            sheepFollowing = null;

            followingGuard = null;
            gameObject.SetActive(false);
            transform.position = startingPosition;
            gameObject.SetActive(true);
            GameManagerSVS.instance.LoseLift();
        }

        private Vector3 GetVectorFromInput(KeyCode code)
        {
            if (code == KeyForFighting1)
            {
                return -Vector3.forward;
            }
            else if (code == KeyForFighting2)
            {
                return Vector3.forward;
            }
            return Vector3.zero;
        }

        public void FInishFightBeforeTime()
        {
            currentFightTime = 0;
            isFighting = false;
            lastPrediction = Vector3.zero;
            Debug.Log("END FIGHT ACCURACY: " + accuracy);

            OnLoseFight();
            currentlyAttackedUnit.OnWInFight();

            currentlyAttackedUnit = null;
        }
    }
}

