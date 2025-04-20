using UnityEngine;
using Vuforia;

public class jump : MonoBehaviour
{
    public Animator jumper;
    public Animator mad;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // jumper = GetComponent<Animator>();
        //mad = mad.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            jumper.SetBool("Jump", true); // Activa 
            mad.SetBool("Mad", true);
        }
        if (Input.GetKeyDown(KeyCode.B))
        {
            jumper.SetBool("Jump", false); // Desactiva
            mad.SetBool("Mad", false); 
        }
    }
}
