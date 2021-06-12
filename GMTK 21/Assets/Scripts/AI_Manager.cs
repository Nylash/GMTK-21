using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AI_Manager : MonoBehaviour
{
    public static AI_Manager instance;

    public int frameBetweenUpdate = 10;
    public GameObject target;
    public NavMeshAgent na;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);

        na = GetComponent<NavMeshAgent>();
    }

    public void UpdatePath(bool forced = false)
    {
        NavMeshPath newPath = new NavMeshPath();
        na.CalculatePath(target.transform.position, newPath);
        na.path = newPath;
    }
}
