using System.Collections;
using System.Collections.Generic;
using System.Drawing.Text;
using System.Net;
using UnityEngine;
using Vuforia;
using static UnityEngine.GraphicsBuffer;

public class Markers : MonoBehaviour
{
    //TODO: Mejorar sistema de animaciones
          //Desactivar solo modelos de animaciones dependiendo el caso si no se muestran los dos marcadores, *no desactivar gmOb_Anim*
          //    Agregar funcion para desactivar los modelos en scripts de animaciones

    //Lista de marcadores
    public ObserverBehaviour[] Targets;

    //Lista de modelos por marcador
    public GameObject[] Objects;
    
    //Lista de animaciones a ejecutar
    public GameObject[] Anim;

    public GameObject[] BTNs;
    
    public GameObject[] Info_AR;

    // Variables que guardan objetos animados
    public GameObject gmOb_Anim_1;
    public GameObject gmOb_Anim_2;
    public GameObject gmOb_Anim;    //General

    //Variables para guardar modelos de cada marcador (de Objects[])
    private GameObject fago;
    private GameObject bact;
    private GameObject antib;

    //Script UI_AR
    public UI_AR script_UI_AR;
    private GameObject gmObj_UI_AR_new;      //Lista de menus UI_AR
    private int numMenu_new;                 //# de menu UI_AR activo
    
    //Variables que indican si marcadores están visibles
    private bool id_fago = false;
    private bool id_bact = false;
    private bool id_antib = false;

    private int mnuElm;
    //private GameObject[] gmOb_ui;

    public bool activeAnim = false;

    private Coroutine corutinaModel;

    public Litic_Anim script_Litic;
    public Lisog_Anim script_Lisog;
    public Antib_Anim script_Antib;
    //public Antib_Anim script_Antib;

    private void Start()
    {

        //gmOb_Anim = transform.GetChild(0).GetChild(2).gameObject;  //Contenedor de animaciones

        //gmOb_mad = gmOb_Anim.transform.GetChild(0).gameObject;  //Obteniendo objetos para controlar las animaciones
        //gmOb_jumper = gmOb_Anim.transform.GetChild(1).gameObject;

        fago = Objects[0];
        bact = Objects[1];
        antib = Objects[2];

    }
    private void Update()
    {
        

        //Debug.Log("fago = "+ id_fago);
        //Debug.Log("Bacteria = " + id_bact);

        

        //if (activeAnim) //Si dos marcadores están visibles
        //{
            
        //    midpoint = ((Targets[0].transform.position + Targets[1].transform.position) / 2f) - new Vector3(0,0,1);

        //    gmOb_Anim.transform.position = midpoint;
            
        //}
        //else
        //{
        //    midpoint = new Vector3(0,0,0);
        //}

        
    }

    public void MenuElement()
    {
        mnuElm = script_UI_AR.numMenu;
        Debug.Log("Menu Element: " + mnuElm);
        switch (mnuElm)
        {

            case 1:
                gmOb_Anim = Anim[0];
                break;
            case 2:
                gmOb_Anim = Anim[1];
                break;
            case 5:
                gmOb_Anim = Anim[2];
                break;
            default:
                gmOb_Anim = new GameObject();
                break;
        }

        gmOb_Anim.SetActive(false);
        corutinaModel =  StartCoroutine(ActivarModel());

    }

    public void DetenerCorutinaModel()
    {
        if (corutinaModel != null) { 
            StopCoroutine(corutinaModel);
        }

        switch (mnuElm)
        {
            case 1:
                script_Litic.FinishAnim();      //Reinicia Animacion
                break;
            case 2:
                script_Lisog.FinishAnim();
                break;
            case 5:
                script_Antib.FinishAnim();
                break;
            default:
                break;
        }
        

        fago.SetActive(true);           //Activa Modelos Simples
        bact.SetActive(true);
        antib.SetActive(true);

        foreach(ObserverBehaviour target in Targets) { 
            target.enabled = true;
        }

        foreach (GameObject btn in BTNs) { 
            btn.SetActive(false);
        }

        gmOb_Anim.SetActive(false);
    }

    public IEnumerator ActivarModel()
    {
        Vector3 midpoint;
        //Animator jumper = gmOb_jumper.GetComponent<Animator>();
        //Animator mad = gmOb_mad.GetComponent<Animator>();

        while (true)
        {
            //Debug.Log("mnuElem: " + mnuElm);
            switch (mnuElm)
            {
                case 0:     //Fago Simple
                    Targets[1].enabled = false;
                    Targets[2].enabled = false;

                    Debug.Log("Fago activo");
                    break;

                case 1:     //Fago Litico
                    Debug.Log("\nid_fago_2: " + id_fago);
                    Debug.Log(" id_bact_2: " + id_bact);


                    if (id_bact && id_fago)     //Se confirma que los marcadores son visibles para ejecutar animación
                    {
                        Targets[2].enabled = false;
                        gmOb_Anim.SetActive(true);
                        //gmOb_sphere.SetActive(true);
                        fago.SetActive(false);
                        bact.SetActive(false);

                        script_Litic.Shown();   //Con

                        //if (!script_Litic.BTN_pressed)
                        //{
                        //    BTNs[0].SetActive(true);
                        //}
                        //else
                        //{
                        //    BTNs[0].SetActive(false);
                        //}

                        midpoint = ((Targets[0].transform.position + Targets[1].transform.position) / 2f) - new Vector3(0, 0, 1); //Vector3(0,0,1)

                        gmOb_Anim.transform.position = midpoint;
                        Info_AR[0].transform.position = midpoint;
                        Debug.Log("Anim Fago activa");

                    }
                    else                        //Se desactiva la animacion
                    {
                        //gmOb_Anim.SetActive(false);
                        //gmOb_sphere.SetActive(false);
                        fago.SetActive(true);
                        bact.SetActive(true);
                        antib.SetActive(true);

                        script_Litic.NotShown();

                        BTNs[0].SetActive(false);

                        

                        midpoint = new Vector3(0, 0, 0);
                        Debug.Log("Anim Fago inactiva");
                    }
                    break;

                case 2:         //Fago Lisogenico
                    if (id_bact && id_fago)     //Se confirma que los marcadores son visibles para ejecutar animación
                    {
                        Targets[2].enabled = false;
                        gmOb_Anim.SetActive(true);
                        //gmOb_sphere.SetActive(true);
                        fago.SetActive(false);
                        bact.SetActive(false);

                        script_Lisog.Shown();

                        //if (!script_Lisog.BTN_pressed)
                        //{
                        //    BTNs[1].SetActive(true);
                        //}
                        //else
                        //{
                        //    BTNs[1].SetActive(false);
                        //}

                        //mad.SetBool("Mad", true);
                        //jumper.SetBool("Jump", true);

                        midpoint = ((Targets[0].transform.position + Targets[1].transform.position) / 2f) - new Vector3(0, 0, 1);

                        gmOb_Anim.transform.position = midpoint;

                    }
                    else                        //Se desactiva la animacion
                    {
                        //gmOb_Anim.SetActive(false);
                        //gmOb_sphere.SetActive(false);
                        fago.SetActive(true);
                        bact.SetActive(true);
                        antib.SetActive(true);

                        script_Lisog.NotShown();

                        BTNs[1].SetActive(false);

                        //mad.SetBool("Mad", false);
                        //jumper.SetBool("Jump", false);
                        midpoint = new Vector3(0, 0, 0);

                    }
                    break;

                case 3:         //Bacteria Simple
                    Targets[0].enabled = false;
                    Targets[2].enabled = false;

                    Debug.Log("Bacteria Simple activo");
                    break;

                case 4:         //Antibiotico Simple
                    Targets[0].enabled = false;
                    Targets[1].enabled = false;

                    Debug.Log("Antib Simple activo");
                    break;

                case 5:         //Antibiotico Mecanismo
                    Debug.Log("\nid_antib_2: " + id_antib);
                    Debug.Log(" id_bact_2: " + id_bact);
                    if (id_bact && id_antib)     //Se confirma que los marcadores son visibles para ejecutar animación
                    {
                        Targets[0].enabled = false;
                        gmOb_Anim.SetActive(true);
                        //gmOb_sphere.SetActive(true);
                        bact.SetActive(false);
                        antib.SetActive(false);

                        script_Antib.Shown();
                        
                        //if (!script_Antib.BTN_pressed)
                        //{
                        //    BTNs[2].SetActive(true);
                        //}
                        //else
                        //{
                        //    BTNs[2].SetActive(false);
                        //}
                        
                        midpoint = ((Targets[2].transform.position + Targets[1].transform.position) / 2f) - new Vector3(0, 0, 1);

                        gmOb_Anim.transform.position = midpoint;
                        Debug.Log("Anim antib activa");

                    }
                    else                        //Se desactiva la animacion
                    {
                        //gmOb_Anim.SetActive(false);
                        //gmOb_sphere.SetActive(false);
                        fago.SetActive(true);
                        bact.SetActive(true);
                        antib.SetActive(true);

                        script_Antib.NotShown();

                        BTNs[2].SetActive(false);

                        midpoint = new Vector3(0, 0, 0);
                        Debug.Log("Anim antib inactiva");
                    }
                    break;

                default:        //Ninguno de los anteriores
                    gmOb_Anim.SetActive(false);
                    //gmOb_sphere.SetActive(false);
                    fago.SetActive(true);
                    bact.SetActive(true);
                    antib.SetActive(true);

                    activeAnim = false;
                    midpoint = new Vector3(0, 0, 0);
                    Debug.Log("Nada");
                    break;
            }
            yield return null;
        }
        
    }

    

    //************* Funciones de reconocimiento de Marcadores ******************+*
    public void detected_fago() //Detecta marcador de fago
    {
        id_fago = true;
        //int n = script_UI_AR.numMenu;
        //gmOb_ui = ui_AR.gmObj_UI_AR;
        Debug.Log("id_Fago:" + id_fago);
    }
    public void detected_bact() //Detecta marcador de bacteria
    {
        id_bact = true;
        Debug.Log("id_Bact:" + id_bact);
    }
    public void detected_antib() //Detecta marcador de bacteria
    {
        id_antib = true;
        Debug.Log("id_antib:" + id_antib);
    }

    public void lost_fago() // No se detecta marcador de fago
    {
        id_fago = false;
        Debug.Log("id_Fago:" + id_fago);
    }
    public void lost_bact() // No se detecta marcador de bacteria
    {
        id_bact = false;
        Debug.Log("id_Bact:" + id_bact);
    }
    
    public void lost_antib() // No se detecta marcador de bacteria
    {
        id_antib = false;
        Debug.Log("id_antib:" + id_antib);
    }
    //********* Termino funciones de detección  de marcadores **************

}
