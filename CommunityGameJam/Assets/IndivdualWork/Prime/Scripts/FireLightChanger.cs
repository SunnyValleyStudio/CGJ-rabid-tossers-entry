using UnityEngine;

public class FireLightChanger : MonoBehaviour
{
    public Color col1;
    public Color col2;
    public Color col3;
    public Color col4;
    private Light Light;
    private Vector3 startpos;
    private float StartIntensity;

    // Use this for initialization
    private void Start()
    {
        startpos = transform.localPosition;
        Light = GetComponent<Light>();
        StartIntensity = Light.intensity;
        InvokeRepeating("changelight", 0.1f, 0.1f);
    }

    // Update is called once per frame
    private void Update()
    {
    }

    private void changelight()
    {
        int TheLightColourNumber = Random.Range(1, 4);
        Light.intensity = Random.Range(StartIntensity - 0.3f, StartIntensity + 0.3f);
        switch (TheLightColourNumber)
        {
            case 1:
                Light.color = col1;

                break;

            case 2:
                Light.color = col2;
                break;

            case 3:
                Light.color = col3;
                break;

            case 4:
                Light.color = col4;
                break;
        }
    }
}