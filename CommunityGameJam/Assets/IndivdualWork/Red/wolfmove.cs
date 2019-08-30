using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class wolfmove : MonoBehaviour
{
    public float speed = 2;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Input.GetAxis("horizontal"), 0, -Input.GetAxisRaw("Vertical")+Time.deltaTime*speed);
       
    }
}
