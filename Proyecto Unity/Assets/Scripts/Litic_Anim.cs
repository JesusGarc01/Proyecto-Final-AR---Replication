using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using Vuforia;

public class Litic_Anim : MonoBehaviour
{

    //TODO: Terminar funcion para desactivar unicamente modelos animados mas no gmObj completo

    public GameObject gmOb_fago;
    public GameObject gmOb_bact;
    private Animator anim_fago; 
    public Animator anim_bact;

    public GameObject gmObj_Aviso;
    public GameObject gmObj_BTN_Continue;
    public GameObject gmOb_Particulas;

    public GameObject skin_bact, skin_fago;

    public GameObject BTN_Play;

    public TextMeshProUGUI advice;

    private float slowDownRate = 4f;
    private bool isSlowingDown = false;

    public Markers script_markers;

    public bool BTN_pressed, BTN_Restart = false;
    public bool anim_Shown = false;
    public bool playing_Anim = false;

    private Coroutine cor_text;

    private void Start()
    {
        anim_bact = gmOb_bact.transform.GetComponent<Animator>();
        anim_fago = gmOb_fago.transform.GetComponent<Animator>();

        skin_bact = gmOb_bact.transform.GetChild(1).gameObject;
        skin_fago = gmOb_fago.transform.GetChild(1).gameObject;

        BTN_pressed = false;
        BTN_Restart = false;
    }
    private void Update()
    {
        if (!BTN_pressed)
        {
            BTN_Play.SetActive(true);
        }
        else
        {
            if(BTN_Play)
                BTN_Play.SetActive(false);
        }

        if (isSlowingDown && anim_fago.speed > 0 && anim_bact.speed > 0)
        {
            anim_fago.speed -= slowDownRate * Time.deltaTime; // Ralentiza la animacion
            anim_bact.speed -= slowDownRate * Time.deltaTime; // Ralentiza la animacion
            if (anim_fago.speed <= 0 && anim_bact.speed <= 0)
            {
                anim_fago.speed = 0; // Detener la animacion
                anim_bact.speed = 0; // Detener la animacion
                isSlowingDown = false;
                Debug.Log("Animacion detenida. Pulsar boton para continuar"); // mostrar el cuadro de dialogo
            }
        }
    }

    public void StartAnimation()
    {
        anim_bact.SetBool("Start", true);
        anim_fago.SetBool("Start", true);
        BTN_pressed = true;
        playing_Anim = true;
        
        string txt = "Adsorcion";
        StartCor(txt);

    }

    public void Ralentizar()
    {
        isSlowingDown = true;
        playing_Anim = false;
        gmObj_BTN_Continue.SetActive(true);
        gmObj_Aviso.SetActive(true);
    }

    public void ContinueAnim()
    {
        isSlowingDown = false;
        playing_Anim = true;
        anim_fago.speed = 1;
        anim_bact.speed = 1;

        gmObj_BTN_Continue.SetActive(false);
        //gmObj_Aviso.SetActive(false);
    }

    public void Stop_Anim()
    {

        anim_bact.speed = 0;
        anim_fago.speed = 0;

        BTN_pressed = false;
        BTN_Restart = true;
    }


    public void RestartAnim()           //Boton de Play al iniciar AR o terminar Anim
    {
        if (!BTN_pressed && BTN_Restart)
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
        anim_bact.Play("Anim_Bacteria_Lit", 0, 0f);

        anim_bact.SetBool("Start", false);
        anim_fago.SetBool("Start", false);


        gmOb_Particulas.SetActive(false);

        skin_bact.SetActive(true);
        skin_fago.SetActive(true);

        if (cor_text != null)
        {
            StopCoroutine(cor_text);
        }
        
        advice.text = "Litico";

        isSlowingDown = false;
        playing_Anim = false;
        BTN_pressed = false;
        BTN_Restart = false;

        anim_fago.speed = 1;
        anim_bact.speed = 1;

        gmObj_BTN_Continue.SetActive(false);
        //gmObj_Aviso.SetActive(false);
        
    }


    public void StartCor(string txt)
    {
        if (cor_text != null)
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

    public void NotShown()
    {
        if (playing_Anim)
        {
            anim_fago.speed = 0;
            anim_bact.speed = 0;
            anim_Shown = false;
        }
    }
    public void Shown()
    {
        if (playing_Anim && anim_fago.speed == 0 && !anim_Shown)
        {
            anim_fago.speed = 1;
            anim_bact.speed = 1;
            anim_Shown = true;
        }
            
    }

    public void Integracion()
    {
        skin_fago.SetActive(false);
        string txt = "Integracion";
        StartCor(txt);
    }

    public void Replicacion()
    {
        //skin_bact.SetActive(false);

        StartParticles();
        
        string txt = "Replicacion";
        StartCor(txt);
    }

    

    public void StartParticles()
    {
        //anim_bact.speed = 0;
        gmOb_Particulas.SetActive(true);
    }

}
