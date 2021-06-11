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

    private bool pathUdaptedThisFrame;
    private int frameCounter;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);
    }

    void Start()
    {
        na = GetComponent<NavMeshAgent>();
        UpdatePath();
    }

    void Update()
    {
        if (frameCounter == 0)
        {
            pathUdaptedThisFrame = false;
            frameCounter = frameBetweenUpdate;
        }
        else
            frameCounter--;
        float fps = 1 / Time.unscaledDeltaTime;
        print(fps);
    }

    public void UpdatePath(bool forced = false)
    {
        if (!pathUdaptedThisFrame || forced)
        {
            pathUdaptedThisFrame = true;
            NavMeshManager.instance.UpdateNavMesh();
            NavMeshPath newPath = new NavMeshPath();
            na.CalculatePath(target.transform.position, newPath);
            na.path = newPath;
        }
    }
}
