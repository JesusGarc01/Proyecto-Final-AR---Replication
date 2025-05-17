using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using Vuforia;

//Aquí se maneja toda la interfaz gráfica principal, menus, botones, imagenes, partículas de la UI inicial
public class UI_Manager : MonoBehaviour
{

    //TODO: Agregar variable para detectar el display en el que me encuentro (UI_Menu Principal)
    //      y poder desactivar display de camara AR (evitar traslape de displays)


    public GameObject AR_Camara;
    public GameObject UI_Camara;

    private GameObject Menu_Inicial, Menu_Final;

    public GameObject img_BTN_Inicio;
    public GameObject img_BTN_QR;
    private Vector3 speedRot = new Vector3(0,0,30f);
    private bool rot_isActive;

    public TMP_Text texto;

    public VuforiaBehaviour vuforiaBehaviour;

    public void Start()
    {
        AR_Camara.SetActive(false);
        UI_Camara.SetActive(true);      //Al iniciar la camara AR se desactiva

        vuforiaBehaviour.enabled = false;   //Inactiva el reconocimiento de marcadores

        rot_isActive = true;    //Rotacion 

        NewText(texto);
    }
    public void Update()
    {
        if(rot_isActive)
        {
            img_BTN_Inicio.transform.Rotate(speedRot * Time.deltaTime);
            img_BTN_QR.transform.Rotate(-speedRot * Time.deltaTime);
        }
    }

    public void btn_Salir()     //Salir de la aplicacion
    {
        Application.Quit();
    }


    //Control de botones Submenu
    public void Menu(GameObject menu)
    {
        Menu_Inicial = menu;

        Menu_Inicial.SetActive(false);
        Menu_Final.SetActive(true);
    }

    public void SubMenu(GameObject subMenu)
    {
        Menu_Final = subMenu;
        rot_isActive = false;
    }

    public void ReturnToMenu()
    {
        Menu_Inicial.SetActive(true);
        Menu_Final.SetActive(false);
    }

    public void ReturnToMenuPrincipal(GameObject menuActual)
    {
        Menu_Final = menuActual;
        Menu_Inicial = transform.GetChild(1).gameObject;

        Menu_Inicial.SetActive(true);
        Menu_Final.SetActive(false);

        rot_isActive = true;
    }
    //Fin Control de botones Submenu


    //Control de botones y Camara AR
    //public void AR_Menu(GameObject menuDestino)
    //{
    //    AR_Camara.SetActive(true);
    //    UI_Camara.SetActive(false);

    //    vuforiaBehaviour.enabled = true;

    //    menuDestino.SetActive(true);
    //}

    //public void AR_ReturnToUI(GameObject menuInicial)
    //{
    //    UI_Camara.SetActive(true);
    //    AR_Camara.SetActive(false);

    //    vuforiaBehaviour.enabled = false;

    //    menuInicial.SetActive(false);
    //}
    //Fin Control de botones y Camara AR


    //Animacion de textos UI
    public void NewText(TMP_Text nText)     //Inicia Corrutina de animacion de texto
    {
        StartCoroutine(TextAnimation(nText));
    }
    public IEnumerator TextAnimation(TMP_Text nText)    //Animacion de texto
    {
        string text1 = nText.text;
        nText.text = "";
        yield return new WaitForSeconds(0.2f);
        foreach (char c in text1)
        {
            nText.text += c;
            yield return new WaitForSeconds(0.1f);
        }

        yield return null;
        StartCoroutine(ParpadeoText(nText, text1));
        
        
    }

    public IEnumerator ParpadeoText(TMP_Text nText, string text)
    {
        bool isCursorVisible = false;
        while (true)
        {
            if (!isCursorVisible)
            {
                nText.text += "_";
                isCursorVisible = true;
            }
            else { 
                nText.text = text;
                isCursorVisible = false;
            }
            yield return new WaitForSeconds(0.5f);
        }
        
    }

}
