using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vuforia;
public class bswitch : MonoBehaviour
{

    public GameObject model3d;
    public ObserverBehaviour[] Targets;
    public int currentTarget;
    public float speed = 1.5f;
    private bool isMoving = false;


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
        ObserverBehaviour target = GetNextDetectedTarget();
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
            journey += Time.deltaTime*speed;
            model3d.transform.position = Vector3.Lerp(startPos, endPos, journey);
            yield return null;
        }

        currentTarget=(currentTarget+1)%Targets.Length;
        isMoving = false;

    }

    private ObserverBehaviour GetNextDetectedTarget()
    {
        foreach (ObserverBehaviour target in Targets)
        {
            if(target != null && (target.TargetStatus.Status == Status.TRACKED || target.TargetStatus.Status == Status.EXTENDED_TRACKED))
            {
                return target;
            }
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
