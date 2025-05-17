using UnityEngine;

public class Anim_Antib : MonoBehaviour
{
    public Antib_Anim script_Antib;

    public void StopAnim()
    {
        script_Antib.Stop_Anim();
    }

    public void Union()
    {
        script_Antib.Union();
    }

    public void Inhibicion()
    {
        script_Antib.Inhibicion();
    }

    public void Debilit()
    {
        script_Antib.Debilitamiento();
    }

    public void Muerte()
    {
        script_Antib.muerte();
    }

    public void StartParticles()
    {
        script_Antib.StartParticleAntib();
    }
}
