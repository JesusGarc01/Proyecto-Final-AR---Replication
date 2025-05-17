using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using Vuforia;

//Aquí se maneja toda la interfaz gráfica principal, menus, botones, imagenes, partículas de la UI inicial
public class UI_AR : MonoBehaviour
{

    //TODO: 


    public GameObject AR_Camara, UI_Camara;

    public GameObject menuAR_pred;

    public GameObject[] gmObj_UI_AR;    /*Lista de menus AR*/

    public int numMenu = 0;

    public GameObject[] UI_particles;


    public VuforiaBehaviour vuforiaBehaviour;

    public void Start()
    {

    }

    public void Update()
    {
        
    }


    //Control de botones y Camara AR
    public void Activa_AR(int nMenu)
    {
        AR_Camara.SetActive(true);

        UI_Camara.GetComponent<Camera>().targetDisplay = 1;
        UI_Camara.SetActive(false);

        vuforiaBehaviour.enabled = true;

        numMenu = nMenu;
        menuAR_pred.SetActive(true);

        gmObj_UI_AR[numMenu].SetActive(true);
        Debug.Log(numMenu);

        foreach(GameObject go in UI_particles)
        {
            go.SetActive(false);
        }
    }

    public void Desactiva_AR()
    {
        menuAR_pred.SetActive(false);

        gmObj_UI_AR[numMenu].SetActive(false);


        UI_Camara.SetActive(true);
        UI_Camara.GetComponent<Camera>().targetDisplay = 0;
        AR_Camara.SetActive(false);
        vuforiaBehaviour.enabled = false;

        foreach (GameObject go in UI_particles)
        {
            go.SetActive(true);
        }
    }
    //Fin Control de botones y Camara AR

}
