using UnityEngine;

public class Litic_Anim : MonoBehaviour
{

    //TODO: Terminar funcion para desactivar unicamente modelos animados mas no gmObj completo

    public GameObject gmOb_fago;
    private Animator anim_fago; 
    public Animator anim_bact;

    public GameObject gmObj_Aviso;
    public GameObject gmObj_BTN_Continue;

    public GameObject skin_bact, skin_fago;

    private float slowDownRate = 4f;
    private bool isSlowingDown = false;

    public Markers script_markers;

    public bool BTN_pressed = false;
    public bool anim_Shown = false;
    public bool playing_Anim = false;

    private void Start()
    {
        anim_bact = GetComponent<Animator>();
        anim_fago = gmOb_fago.transform.GetComponent<Animator>();

        skin_bact = transform.GetChild(1).gameObject;
        skin_fago = gmOb_fago.transform.GetChild(1).gameObject;
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
        BTN_pressed = true;
        playing_Anim = true;
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
        gmObj_Aviso.SetActive(false);
    }

    public void FinishAnim()
    {
        BTN_pressed = false;
        anim_fago.Play("Armtr_Fago_Anim_Fago", 0, 0f);
        anim_bact.Play("Anim_Bacteria_Lit", 0, 0f);
        anim_bact.SetBool("Start", false);
        anim_fago.SetBool("Start", false);
        

        isSlowingDown = false;
        playing_Anim = false;
        anim_fago.speed = 1;
        anim_bact.speed = 1;
        gmObj_BTN_Continue.SetActive(false);
        gmObj_Aviso.SetActive(false);
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

    public void ByeSkin_Bact()
    {
        skin_bact.SetActive(false);
    }

    public void ByeSkin_Fago()
    {
        skin_fago.SetActive(false);
    }

}
