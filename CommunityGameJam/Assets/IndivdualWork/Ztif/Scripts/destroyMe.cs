using UnityEngine;
using System.Collections;

public class destroyMe : MonoBehaviour{

    public float deathtimer = 10;


	// Use this for initialization
	void Start () {
        //Destroy(gameObject, deathtimer);
        //gameObject.SetActive(true);

	}
	
	// Update is called once per frame
	void Update ()
    {

	
	}

    private void OnEnable()
    {
        Destroy(gameObject, deathtimer);
    }
}
