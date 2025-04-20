using UnityEngine;
using Vuforia;

public class UI_Manager : MonoBehaviour
{

    public GameObject AR_Camara;
    public GameObject UI_Camara;

    public VuforiaBehaviour vuforiaBehaviour;

    public void Start()
    {
        AR_Camara.SetActive(false);
        UI_Camara.SetActive(true);

        vuforiaBehaviour.enabled = false;
    }
    public void Update()
    {
        
    }

    public void Activa()
    {
        AR_Camara.SetActive(true);
        UI_Camara.SetActive(false);

        vuforiaBehaviour.enabled = true;
    }

}
