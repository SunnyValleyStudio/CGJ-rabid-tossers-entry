using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Panel : MonoBehaviour
{
// panel's animator
public Animator anim;
public GameObject introcam;

// name of the bool in the animator to change
public string animBoolName = "enter";

// starting state of the bool
public bool startingState = true;

void Start()
{
        // set starting state
        anim.SetBool(animBoolName, startingState);
}

void Update()
{

}

// show the panel. Used by buttons etc
public void Enter()
{
        anim.SetBool(animBoolName, true);

}

// hide the panel. Used by buttons etc
public void Exit()
{
        anim.SetBool(animBoolName, false);

}
public void HideCam()
{
        introcam.SetActive(false);
}
public void ShowCam()
{
        introcam.SetActive(true);
}
}
