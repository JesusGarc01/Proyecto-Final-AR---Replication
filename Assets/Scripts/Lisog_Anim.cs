using UnityEngine;

public class Lisog_Anim : MonoBehaviour
{
    public GameObject gmOb_fago; 
    public Animator anim_bact;
    public GameObject gmOb_bact_2;

    private Animator anim_fago, anim_bact_2;

    private GameObject skin_bact, skin_bact_2, skin_fago;

    //public GameObject gmObj_Aviso;
    //public GameObject gmObj_BTN_Continue;

    private float slowDownRate = 4f;
    private bool isSlowingDown = false;

    public Markers script_markers;

    public bool BTN_pressed = false;
    public bool playing_Anim = false;
    public bool anim_Shown = false;

    private void Start()
    {
        anim_bact = GetComponent<Animator>();
        anim_fago = gmOb_fago.GetComponent<Animator>();
        anim_bact_2 = gmOb_bact_2.GetComponent<Animator>();

        skin_bact = transform.GetChild(1).gameObject;
        skin_bact_2 = anim_bact_2.transform.GetChild(1).gameObject;

        skin_fago = gmOb_fago.transform.GetChild(1).gameObject;

        skin_bact_2.SetActive(false);
    }
    private void Update()
    {

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
        anim_bact_2.SetBool("Start", false);
        BTN_pressed = true;
        playing_Anim = true;
    }

    public void StartSecond()
    {
        skin_bact_2.SetActive(true);
        
        anim_bact_2.SetBool("Start", true);
    }

    public void ByeSkin_Bact()
    {
        skin_bact.SetActive(false);
    }

    public void ByeSkin_Fago()
    {
        skin_fago.SetActive(false);
    }

    public void Ralentizar()
    {
        isSlowingDown = true;
        playing_Anim = false;
        //gmObj_BTN_Continue.SetActive(true);
        //gmObj_Aviso.SetActive(true);
    }

    public void ContinueAnim()
    {
        isSlowingDown = false;
        anim_fago.speed = 1;
        anim_bact.speed = 1;

        playing_Anim = true;
        //gmObj_BTN_Continue.SetActive(false);
        //gmObj_Aviso.SetActive(false);
    }

    public void FinishAnim()
    {
        BTN_pressed = false;
        anim_fago.Play("Armtr_Fago_Anim_Fago", 0, 0f);
        anim_bact.Play("Bact_Lis_1", 0, 0f);
        anim_bact_2.Play("Bact_Lis_2", 0, 0f);
        anim_bact.SetBool("Start", false);
        anim_bact_2.SetBool("Start", false);
        anim_fago.SetBool("Start", false);

        skin_bact.SetActive(true);
        skin_fago.SetActive(true);
        skin_bact_2.SetActive(false);
        

        isSlowingDown = false;
        playing_Anim = false;
        anim_fago.speed = 1;
        anim_bact.speed = 1;
        //gmObj_BTN_Continue.SetActive(false);
        //gmObj_Aviso.SetActive(false);
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
