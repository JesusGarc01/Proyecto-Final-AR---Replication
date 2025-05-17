using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vuforia;
using static UnityEngine.GraphicsBuffer;

public class bswitchrand : MonoBehaviour 
{
    public GameObject model3d;
    public ObserverBehaviour[] Targets;
    public float speed = 1.5f;
    private bool isMoving = false;
    private int targt = 0;

    public void switchMarker()
    {
        if (!isMoving)
        {
            StartCoroutine(moveModel());
        }
    }

    private IEnumerator moveModel()
    {
        isMoving = true;
        ObserverBehaviour target = randomTarget();
        if (target == null)
        {
            isMoving = false;
            yield break;
        }

        Vector3 startPos = model3d.transform.position;
        Vector3 endPos = target.transform.position;

        float journey = 0;

        while (journey <= 1f)
        {
            journey += Time.deltaTime * speed;
            model3d.transform.position = Vector3.Lerp(startPos, endPos, journey);
            yield return null;
        }

        isMoving = false;
    }

    private ObserverBehaviour randomTarget()
    {
        List<ObserverBehaviour> detectedTargets = new List<ObserverBehaviour>();

        foreach (ObserverBehaviour target in Targets)
        {
            if (target != null && (target.TargetStatus.Status == Status.TRACKED || target.TargetStatus.Status == Status.EXTENDED_TRACKED))
            {
                detectedTargets.Add(target);
            }
        }

        if (detectedTargets.Count > 0)
        {
            int randomIndex = Random.Range(0, detectedTargets.Count);

            while(randomIndex == targt)
            {
                randomIndex = Random.Range(0, detectedTargets.Count);

            }
            targt = randomIndex;
            return detectedTargets[randomIndex];
            
        }

        return null;
    }

    void Start()
    {
    
    }

    void Update()
    {
    
    }
}