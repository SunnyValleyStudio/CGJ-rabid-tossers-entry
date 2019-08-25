using UnityEngine;

public class Door : MonoBehaviour, IInterActable
{
    private Animator Anim;

    public void Interacted()
    {
        print("pressed");
        Anim.SetTrigger("Pressed");
    }

    // Start is called before the first frame update
    private void Start()
    {
        Anim = GetComponent<Animator>();
    }

    // Update is called once per frame
}