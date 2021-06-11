using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class NavMeshManager : MonoBehaviour
{
    public static NavMeshManager instance;

    public NavMeshSurface nm;

    private bool navMeshUdaptedThisFrame;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);
    }

    void Start()
    {
        nm = GetComponent<NavMeshSurface>();
        nm.BuildNavMesh();
    }

    
    void FixedUpdate()
    {
        navMeshUdaptedThisFrame = false;
    }

    public void UpdateNavMesh()
    {
        if (!navMeshUdaptedThisFrame)
        {
            navMeshUdaptedThisFrame = true;
            nm.UpdateNavMesh(nm.navMeshData);
        }
    }
}
