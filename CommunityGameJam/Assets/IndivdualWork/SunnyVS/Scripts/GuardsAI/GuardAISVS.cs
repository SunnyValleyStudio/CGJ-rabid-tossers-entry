﻿using SVSAI;
using SVSGame;
using SVSWolf;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace SVSGuards
{
    [RequireComponent(typeof(NavMeshAgent))]
    public class GuardAISVS : MonoBehaviour, IAttackableSVS
    {
        public Transform player;
        public float playerDistance;
        public float aiAwarnessDistance = 10f;
        public float aiChaseDIstance = 20f;
        public float agentSpeedDefault = 3.5f;
        public float AIChaseSpeed = 10f;
        public float damping;
        float caughtPlayerStoppingDistance = 3f;


        public Transform[] navPoint;
        public NavMeshAgent agent;
        public int destPoint = 0;
        public Transform goal;

        public float fieldOfViewAngle = 110f;
        public bool playerInSIght;
        public bool isFIghting = false;


        private void Start()
        {
            agent = GetComponent<NavMeshAgent>();
            if (goal != null)
            {
                agent.destination = goal.position;
            }

            else if (agent.destination == null && navPoint.Length > 0)
            {
                goal = navPoint[0];
                agent.destination = goal.position;

            }
            agent.autoBraking = false;
            player = FindObjectOfType<WolfActionsSVS>().transform;
            agent.speed = agentSpeedDefault;
        }

        private void Update()
        {
            if (isFIghting == false)
            {
                playerDistance = Vector3.Distance(player.position, transform.position);
                if (playerDistance < aiAwarnessDistance)
                {
                    Vector3 direction = player.transform.position - transform.position;
                    float angle = Vector3.Angle(direction, transform.forward);
                    if (angle < fieldOfViewAngle * 0.5f)
                    {
                        RaycastHit hit;
                        Debug.DrawRay(transform.position, direction.normalized * aiAwarnessDistance, Color.red);
                        if (Physics.Raycast(transform.position, direction.normalized, out hit, aiAwarnessDistance))
                        {

                            if (hit.collider.gameObject == player.gameObject)
                            {
                                playerInSIght = true;
                                if (player == null)
                                {
                                    player = FindObjectOfType<WolfActionsSVS>().transform;
                                    if (player == null)
                                    {
                                        return;
                                    }
                                }
                                player.gameObject.GetComponent<WolfActionsSVS>().SetGuardFollow(this);
                            }
                        }
                    }
                    RotateTOwardsNoise();

                }

                if (playerInSIght)
                {
                    if (playerDistance < aiChaseDIstance)
                    {

                        if (playerDistance <= caughtPlayerStoppingDistance)
                        {
                            agent.isStopped = true;

                            WolfActionsSVS wolfScript = player.gameObject.GetComponent<WolfActionsSVS>();
                            if (wolfScript.isFighting == false || wolfScript.isTired == false)
                            {

                                wolfScript.StartAFight();
                                isFIghting = true;
                            }
                            else if (wolfScript.isTired || wolfScript.followingGuard != this || wolfScript.sheepFollowing != null)
                            {
                                wolfScript.FInishFightBeforeTime();
                            }

                        }
                        else
                        {
                            Chase();
                        }
                    }
                    else
                    {
                        playerInSIght = false;
                        if (player == null)
                        {
                            player = FindObjectOfType<WolfActionsSVS>().transform;
                            if (player == null)
                            {
                                TravelToNextPoint();
                                return;
                            }
                        }
                        player.gameObject.GetComponent<WolfActionsSVS>().ResetGuardFollowing(); ;
                        TravelToNextPoint();
                    }
                }


                if (agent.isOnNavMesh && agent.isStopped == false && agent.remainingDistance < 0.5f)
                {
                    TravelToNextPoint();
                }
            }
        }

        private void RotateTOwardsNoise()
        {
            transform.LookAt(player);
            //Quaternion neededRotation = Quaternion.LookRotation((player.transform.position - transform.position));
            //Quaternion.RotateTowards(transform.rotation, neededRotation, Time.deltaTime * 10f);
        }

        private void Chase()
        {
            //transform.Translate(Vector3.forward * AIChaseSpeed * Time.deltaTime);
            agent.speed = AIChaseSpeed;
            agent.stoppingDistance = caughtPlayerStoppingDistance;
            agent.SetDestination(player.transform.position);
        }

        private void TravelToNextPoint()
        {
            agent.speed = agentSpeedDefault;
            if (navPoint.Length == 0)
            {
                return;
            }
            else
            {
                if (navPoint[destPoint] == null)
                {
                    destPoint = 0;
                }
                agent.destination = navPoint[destPoint].position;

                destPoint = (destPoint + 1) % navPoint.Length;
            }
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(transform.position, aiAwarnessDistance);
        }

        public void OnWInFight()
        {
            playerInSIght = false;
            TravelToNextPoint();
            Debug.Log("Guard won - respawn wolf?");
            isFIghting = false;
        }

        public void OnLoseFight()
        {
            GameManagerSVS.instance.Killed(EnemyType.Dog);
            Destroy(gameObject);
            //Debug.Log("Guard down");
        }
    }
}

