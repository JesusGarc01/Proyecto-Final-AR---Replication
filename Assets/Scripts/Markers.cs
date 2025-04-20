using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vuforia;
using static UnityEngine.GraphicsBuffer;

public class Markers : MonoBehaviour
{

    public ObserverBehaviour[] Targets;

    public GameObject[] Objects;


    public GameObject gmOb_jumper;
    public GameObject gmOb_mad;
    public GameObject gmOb_Anim;

    private GameObject cub1;
    private GameObject cub2;
        
    private bool id_fago = false;
    private bool id_bact = false;

    private int mnuElm = 0;

    
    public void detected_fago()
    {
        id_fago = true;
    }
    public void detected_bact()
    {
        id_bact = true;
    }

    public void lost_fago()
    {
        id_fago = false;
    }
    public void lost_bact()
    {
        id_bact = false;
    }

    private void Start()
    {

        gmOb_mad = transform.GetChild(0).GetChild(2).GetChild(0).gameObject;
        gmOb_jumper = transform.GetChild(0).GetChild(2).GetChild(1).gameObject;

        gmOb_Anim = transform.GetChild(0).GetChild(2).gameObject;

        gmOb_Anim.SetActive(false);

        cub1 = Objects[0];
        cub2 = Objects[1];
        
    }
    private void Update()
    {
        Vector3 midpoint;

        Debug.Log("fago = "+ id_fago);
        Debug.Log("Bacteria = " + id_bact);

        
        if (id_fago && id_bact)
        {
            midpoint = ((Targets[0].transform.position + Targets[1].transform.position) / 2f) - new Vector3(0,0,1);

            gmOb_Anim.transform.position = midpoint;
            
        }
        else
        {
            midpoint = new Vector3(0,0,0);
        }


        ActivarAnim();
    }

    private void ActivarAnim()
    {
        Animator jumper = gmOb_jumper.GetComponent<Animator>();
        Animator mad = gmOb_mad.GetComponent<Animator>();

        if (id_bact && id_fago)
        {
            gmOb_Anim.SetActive(true);
            //gmOb_sphere.SetActive(true);
            cub1.SetActive(false);
            cub2.SetActive(false);
            
            mad.SetBool("Mad", true);
            jumper.SetBool("Jump", true);

        }
        else
        {
            gmOb_Anim.SetActive(false);
            //gmOb_sphere.SetActive(false);
            cub1.SetActive(true);
            cub2.SetActive(true);

            mad.SetBool("Mad", false);
            jumper.SetBool("Jump", false);
        }
    }

    public void MenuElement()
    {
        switch (mnuElm) {
            case 0:
                break;
        }
    }

}
