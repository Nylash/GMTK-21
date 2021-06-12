using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class NavMeshManager : MonoBehaviour
{
    public static NavMeshManager instance;

    public GameObject safeZonePrefab;
    public GameObject deadZonePrefab;
    public GameObject grassPropsPrefab;
    public static int maxLenght = 57;
    public static int maxWidht = 25;
    public GameObject[,] tiles = new GameObject[maxLenght, maxWidht];

    private NavMeshSurface nm;
    private Vector3[] rot = new Vector3[4];

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);
        rot[0] = new Vector3(0, 0, 0);
        rot[1] = new Vector3(90, 0, 0);
        rot[2] = new Vector3(0, 90, 0);
        rot[3] = new Vector3(0, 0, 90);
    }

    void Start()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            tiles[-(int)transform.GetChild(i).position.x / 10, (int)transform.GetChild(i).position.z / 10] = transform.GetChild(i).gameObject;
            LeftGroundManager.instance.ChangeMaterial(-(int)transform.GetChild(i).position.x / 10, (int)transform.GetChild(i).position.z / 10);
        }
        foreach (GameObject item in tiles)
        {
            if (item != null)
            {
                item.transform.rotation = RandomRotation();
                Instantiate(grassPropsPrefab, item.transform.position, Quaternion.identity);
            }
        }
        for (int i = 0; i < maxLenght; i++)
        {
            for (int j = 0; j < maxWidht; j++)
            {
                if(tiles[i,j] == null)
                {
                    tiles[i,j] = Instantiate(deadZonePrefab, new Vector3(-i * 10, 0, j * 10), RandomRotation(), transform);
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
            LeftGroundManager.instance.ChangeMaterial(X, Z);
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
                    tiles[i, j] = Instantiate(safeZonePrefab, new Vector3(-i * 10, 0, j * 10), RandomRotation(), transform);
                    LeftGroundManager.instance.ChangeMaterial(i, j);
                }
            }
        }
        UpdateNavMesh();
    }

    Quaternion RandomRotation()
    {
        return Quaternion.Euler(rot[Random.Range(0, 4)]);
    }
}
