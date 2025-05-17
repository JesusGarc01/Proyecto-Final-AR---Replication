using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using Vuforia;
public class Antib_Anim : MonoBehaviour
{
    public GameObject gmOb_antib; 
    public GameObject gmOb_antib_part; 
    public GameObject gmOb_bact;
    public GameObject gmOb_bact_2;

    private Animator anim_antib, anim_bact, anim_bact_2, anim_antib_part;

    private GameObject skin_bact, skin_bact_2, skin_antib, skin_antib_2, skin_antib_part;

    public GameObject BTN_Play;

    public GameObject ptcl_Bact, ptcl_Antib;

    //public GameObject gmObj_Aviso;
    //public GameObject gmObj_BTN_Continue;

    private float slowDownRate = 4f;
    private bool isSlowingDown = false;

    public Markers script_markers;

    public bool BTN_pressed, BTN_Restart = false;
    public bool playing_Anim = false;
    public bool anim_Shown = false;

    private Coroutine cor_text;

    public TextMeshProUGUI advice;

    private void Start()
    {
        anim_antib = gmOb_antib.GetComponent<Animator>();
        anim_antib_part = gmOb_antib_part.GetComponent<Animator>();
        anim_bact = gmOb_bact.GetComponent<Animator>();
        anim_bact_2 = gmOb_bact_2.GetComponent<Animator>();

        
        skin_antib = gmOb_antib.transform.GetChild(0).gameObject;
        skin_antib_2 = gmOb_antib.transform.GetChild(1).gameObject;
        
        skin_bact = gmOb_bact.transform.GetChild(1).gameObject;
        skin_bact_2 = gmOb_bact_2.transform.GetChild(1).gameObject;

        skin_antib_part = gmOb_antib_part.transform.GetChild(0).gameObject;

        skin_antib_part.SetActive(false);
        skin_bact.SetActive(false);
        skin_bact_2.SetActive(false);

        ptcl_Antib.SetActive(false);
        ptcl_Bact.SetActive(false);
    }
    private void Update()
    {
        if (!BTN_pressed)
        {
            BTN_Play.SetActive(true);
        }
        else
        {
            if (BTN_Play)
                BTN_Play.SetActive(false);
        }
    }

    public void StartAnimation()
    {
        anim_antib.SetBool("Start", true);

        BTN_pressed = true;
        playing_Anim = true;

        string txt = "Liberacion / Disolucion";
        StartCor(txt);
    }

    public void Stop_Anim()
    {

        anim_bact.speed = 0;
        anim_bact_2.speed = 0;
        anim_antib.speed = 0;
        anim_antib_part.speed = 0;

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
        BTN_pressed = false;

        anim_antib.Play("Antib", 0, 0f);
        anim_antib_part.Play("Antib_Part", 0, 0f);
        anim_bact.Play("Bact_Lis_1", 0, 0f);
        anim_bact_2.Play("Bact_Lis_2", 0, 0f);

        anim_antib.SetBool("Start", false);
        anim_antib_part.SetBool("Start", false);
        anim_bact.SetBool("Start", false);
        anim_bact_2.SetBool("Start", false);
        

        skin_antib.SetActive(true);
        skin_antib_2.SetActive(true);
        skin_antib_part.SetActive(false);
        skin_bact.SetActive(false);
        skin_bact_2.SetActive(false);

        ptcl_Antib.SetActive(false);
        ptcl_Bact.SetActive(false);

        if (cor_text != null)
        {
            StopCoroutine(cor_text);
        }

        advice.text = "Mecanismo de accion";

        isSlowingDown = false;
        playing_Anim = false;

        BTN_pressed = false;
        BTN_Restart = false;
        //Inicio = true;
        //BTN_Play.SetActive(false);

        anim_antib.speed = 1;
        anim_antib_part.speed = 1;
        anim_bact.speed = 1;
        anim_bact_2.speed = 1;


        isSlowingDown = false;
        playing_Anim = false;
    }

    public void Union()
    {
        skin_antib.SetActive(false);
        skin_antib_2.SetActive(false);

        skin_bact.SetActive(true);
        skin_bact_2.SetActive(true);
        skin_antib_part.SetActive(true);

        anim_bact.SetBool("Start", true);
        anim_bact_2.SetBool("Start", true);
        anim_antib_part.SetBool("Start", true);

        ptcl_Antib.SetActive(false);

        string txt = "Union";
        StartCor(txt);
    }

    public void Inhibicion()
    {
        skin_antib_part.SetActive(false);
        string txt = "Inhibicion";
        StartCor(txt);
    }

    public void Debilitamiento()
    {
        skin_bact_2.SetActive(false);
        string txt = "Debilitamiento";
        StartCor(txt);
    }

    public void muerte()
    {
        skin_bact.SetActive(false);
        ptcl_Bact.SetActive(true);
        string txt = "Muerte";
        StartCor(txt);
    }

    public void Ralentizar()
    {
        isSlowingDown = true;
        playing_Anim = false;
        //gmObj_BTN_Continue.SetActive(true);
        //gmObj_Aviso.SetActive(true);
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


    public void StartParticleAntib()
    {
        ptcl_Antib.SetActive(true);
    }

    public void NotShown()
    {
        if (playing_Anim)
        {
            anim_antib.speed = 0;
            anim_bact.speed = 0;
            anim_bact_2.speed = 0;
            anim_antib_part.speed = 0;
            anim_Shown = false;
        }


    }
    public void Shown()
    {
        if (playing_Anim && anim_antib.speed == 0 && !anim_Shown)
        {
            anim_antib.speed = 1;
            anim_bact.speed = 1;
            anim_bact_2.speed = 1;
            anim_antib_part.speed = 1;
            anim_Shown = true;
        }

    }

}
