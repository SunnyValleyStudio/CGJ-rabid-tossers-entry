using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SVSInput;
using SVSPredictor;
using System;

namespace SVSPlayer
{
public class PlayerControllerSVS_ztif : MonoBehaviour
{
        public GameObject salmonPrefab;
        public float thrust;
        public Text swimPowerText;
        public Text riverSpeedText;
        public Text pointsText;
        public Rigidbody rb;
        
IInputManagerSVS _inputManager;
InputPredictorSVS inputPredictor;
Vector3 lastInput = Vector3.zero;
public Vector3 LastInput {
        get => lastInput; set => lastInput = value;
}

// Start is called before the first frame update
void Start()
{
        _inputManager = new InputManagerArrowsSVS(InputAxis.Horizontal);
        inputPredictor = GetComponent<InputPredictorSVS>();
        

}

// Update is called once per frame
void Update()
{
        Vector3 movementVector = _inputManager.GetMainMovementInput();
        if (movementVector.magnitude > 0 && LastInput == Vector3.zero)
        {
                
                //Swim forward:
                rb.AddForce(transform.forward * thrust);
                
                if (!inputPredictor.SequenceReady)
                {
                        inputPredictor.PrepareThePredictorClass(movementVector);
                }
                LastInput = movementVector;
                Debug.Log(movementVector);
                PrintPrediction(movementVector);
        }
        else if (movementVector.magnitude == 0)
        {
                LastInput = Vector3.zero;
        }
}

public void PrintPrediction(Vector3 input)
{
        Vector3 prediction = inputPredictor.PredictNextInput(input);
        Debug.Log("Expected next input: "+prediction);
}
}
}
