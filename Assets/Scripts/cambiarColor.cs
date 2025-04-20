using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class cambiarColor : MonoBehaviour
{
    public GameObject modelo3d;
    public GameObject cabello;

    public Material[] texturas;
    private int cont = 0;

    void Start()
    {

    }

    void Update()
    {

    }

    public void CambiarColor_BTN()
    {
        Color randomColor = new Color(Random.value, Random.value, Random.value);
        cabello.GetComponent<Renderer>().material.color = randomColor;
    }

    public void CambiarTextura_BTN()
    {
        cont = (cont + 1)%texturas.Length;
        modelo3d.GetComponent<Renderer>().material = texturas[cont];
    }

}
