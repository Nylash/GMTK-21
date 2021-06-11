using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SafeZoneScaling : MonoBehaviour
{
    public float targetScale;
    public float timeToLerp = .01f;

    private bool scaleFinished;


    private void Update()
    {
        if(targetScale - transform.localScale.x > .1f)
        {
            transform.localScale = Vector3.Lerp(transform.localScale, new Vector3(targetScale, transform.localScale.y, targetScale), timeToLerp);
            AI_Manager.instance.UpdatePath();
        }
        else if(!scaleFinished)
        {
            scaleFinished = true;
            AI_Manager.instance.UpdatePath(true);
        }
    }
}
