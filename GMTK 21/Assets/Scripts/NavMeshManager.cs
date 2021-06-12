using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class NavMeshManager : MonoBehaviour
{
    public static NavMeshManager instance;

    public GameObject safeZonePrefab;
    public GameObject deadZonePrefab;
    public static int maxLenght = 50;
    public static int maxWidht = 25;
    public GameObject[,] tiles = new GameObject[maxLenght, maxWidht];

    private NavMeshSurface nm;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);
    }

    void Start()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            tiles[-(int)transform.GetChild(i).position.x / 10, (int)transform.GetChild(i).position.z / 10] = transform.GetChild(i).gameObject;
        }
        for (int i = 0; i < maxLenght; i++)
        {
            for (int j = 0; j < maxWidht; j++)
            {
                if(tiles[i,j] == null)
                {
                    tiles[i,j] = Instantiate(deadZonePrefab, new Vector3(-i * 10, 0, j * 10), Quaternion.identity, transform);
                }
            }
        }

        nm = GetComponent<NavMeshSurface>();
        nm.BuildNavMesh();
        UpdateNavMesh();
    }

    public void UpdateNavMesh()
    {
        nm.UpdateNavMesh(nm.navMeshData);
        AI_Manager.instance.UpdatePath();
    }

    public IEnumerator ChangeBlock(int X, int Z)
    {
        if (tiles[X, Z].CompareTag("DeadZone"))
        {
            Destroy(tiles[X, Z]);
            tiles[X, Z] = Instantiate(safeZonePrefab, new Vector3(-X*10, 0, Z*10), Quaternion.identity, transform);
            UpdateNavMesh();
        }
        yield return new WaitForSeconds(2);
        ChangerNeighbors(X, Z);
    }

    public void ChangerNeighbors(int X, int Z)
    {
        for (int i = X-1; i < X+2; i++)
        {
            for (int j = Z-1; j < Z+2; j++)
            {
                if (i < 0 || j < 0 || j > maxWidht || i > maxLenght)
                    continue;
                if (tiles[i, j].CompareTag("DeadZone"))
                {
                    Destroy(tiles[i, j]);
                    tiles[i, j] = Instantiate(safeZonePrefab, new Vector3(-i * 10, 0, j * 10), Quaternion.identity, transform);
                }
            }
        }
        UpdateNavMesh();
    }
}