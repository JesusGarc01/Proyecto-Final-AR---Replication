using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using Vuforia;

public class Lisog_Anim : MonoBehaviour
{
    public GameObject gmOb_fago; 
    public GameObject gmOb_bact; 
    public GameObject gmOb_bact_2;

    private Animator anim_bact, anim_fago, anim_bact_2;

    private GameObject skin_bact, skin_bact_2, skin_fago;

    public GameObject BTN_Play;
    public GameObject Particulas;

    //public GameObject gmObj_Aviso;
    //public GameObject gmObj_BTN_Continue;

    private float slowDownRate = 4f;
    private bool isSlowingDown = false;

    public Markers script_markers;

    public bool BTN_pressed, BTN_Restart, Inicio = false;
    public bool playing_Anim = false;
    public bool anim_Shown = false;

    private Coroutine cor_text;

    public TextMeshProUGUI advice;

    private void Start()
    {
        anim_bact = gmOb_bact.GetComponent<Animator>();
        anim_fago = gmOb_fago.GetComponent<Animator>();
        anim_bact_2 = gmOb_bact_2.GetComponent<Animator>();

        skin_bact = gmOb_bact.transform.GetChild(1).gameObject;
        skin_bact_2 = anim_bact_2.transform.GetChild(1).gameObject;

        skin_fago = gmOb_fago.transform.GetChild(1).gameObject;

        skin_bact_2.SetActive(false);

        Particulas.SetActive(false);

        BTN_pressed = false;
        BTN_Restart = false;
        Inicio = true;
    }
    private void Update()
    {

        //if (isSlowingDown && anim_fago.speed > 0 && anim_bact.speed > 0)
        //{
        //    anim_fago.speed -= slowDownRate * Time.deltaTime; // Ralentiza la animacion
        //    anim_bact.speed -= slowDownRate * Time.deltaTime; // Ralentiza la animacion
        //    if (anim_fago.speed <= 0 && anim_bact.speed <= 0)
        //    {
        //        anim_fago.speed = 0; // Detener la animacion
        //        anim_bact.speed = 0; // Detener la animacion
        //        isSlowingDown = false;
        //        Debug.Log("Animacion detenida. Pulsar boton para continuar"); // mostrar el cuadro de dialogo
        //    }
        //}

        if (!BTN_pressed)
        {
            BTN_Play.SetActive(true);
        }
        else
        {
            if(BTN_Play)
                BTN_Play.SetActive(false);
        }


    }

    public void StartAnimation()
    {
        anim_bact.SetBool("Start", true);
        anim_fago.SetBool("Start", true);
        anim_bact_2.SetBool("Start", false);
        BTN_pressed = true;
        playing_Anim = true;

        string txt = "Adsorcion";
        StartCor(txt);

    }

    public void ByeSkin_Fago()
    {
        skin_fago.SetActive(false);
    }

    public void Integracion()
    {
        string txt = "Integracion";
        
        StartCor(txt);
        anim_bact.speed = 0.5f;

        //StartCor(txt);
        //Debug.Log("advice: " + advice[1]);
        
    }

    public void ReplBact()
    {
        string txt = "Replicacion de bacteria";
        
        skin_bact_2.SetActive(true);
        anim_bact_2.SetBool("Start", true);

        StartCor(txt);
    }

    public void Ciclo_Litico()
    {
        string txt = "Ciclo Litico";
        skin_bact.SetActive(false);

        StartCor(txt);
    }

    public void Replicacion()
    {
        StartParts();
        string txt = "Replicacion";

        StartCor(txt);
    }

    public void StartCor(string txt)
    {
        if(cor_text != null)
            StopCoroutine(cor_text);

        cor_text = StartCoroutine(TextAnimation(advice, txt));
    }

    public IEnumerator TextAnimation(TextMeshProUGUI nText, string newText)    //Animacion de texto
    {
        TextMeshProUGUI nwT = nText.GetComponent<TextMeshProUGUI>();
        nwT.text = "";
        yield return new WaitForSeconds(0.1f);
        foreach (char c in newText)
        {
            nwT.text += c;
            yield return new WaitForSeconds(0.1f);
        }

        Debug.Log("texto: " + nwT);

    }

    
    public void Stop_Anim()
    {

        anim_bact.speed = 0;
        anim_bact_2.speed = 0;
        anim_fago.speed = 0;

        BTN_pressed = false;
        BTN_Restart = true;
    }

    public void RestartAnim()           //Boton de Play al iniciar AR o terminar Anim
    {
        if(!BTN_pressed && BTN_Restart)
        {
            FinishAnim();
            StartAnimation();
        }
        else
        {
            StartAnimation();
        }

    }
    
    public void FinishAnim()
    {

        anim_fago.Play("Armtr_Fago_Anim_Fago", 0, 0f);
        anim_bact.Play("Bact_Lis_1", 0, 0f);
        anim_bact_2.Play("Bact_Lis_2", 0, 0f);

        anim_bact.SetBool("Start", false);
        anim_bact_2.SetBool("Start", false);
        anim_fago.SetBool("Start", false);

        skin_bact.SetActive(true);
        skin_fago.SetActive(true);
        skin_bact_2.SetActive(false);


        if (cor_text != null)
        {
            StopCoroutine(cor_text);
        }

        advice.text = "Lisogenico";
        Particulas.SetActive(false);

        isSlowingDown = false;
        playing_Anim = false;

        BTN_pressed = false;
        BTN_Restart = false;
        //Inicio = true;
        //BTN_Play.SetActive(false);

        anim_fago.speed = 1;
        anim_bact.speed = 1;
        anim_bact_2.speed = 1;
        //gmObj_BTN_Continue.SetActive(false);
        //gmObj_Aviso.SetActive(false);

        
    }

    public void StartParts()
    {
        Particulas.SetActive(true);
    }

    public void NotShown()
    {
        if (playing_Anim)
        {
            anim_fago.speed = 0;
            anim_bact.speed = 0;
            anim_bact_2.speed = 0;
            anim_Shown = false;
        }
    }
    public void Shown()
    {
        if (playing_Anim && anim_fago.speed == 0 && !anim_Shown)
        {
            anim_fago.speed = 1;
            anim_bact.speed = 1;
            anim_bact_2.speed = 1;
            anim_Shown = true;
        }

    }

}
