using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeftGroundManager : MonoBehaviour
{
    public static LeftGroundManager instance;

    public GameObject groundPrefab;
    public Material clayRock;
    public Material clayWood;
    public static int maxLenght = 75;
    public static int maxWidht = 25;
    public GameObject[,] tiles = new GameObject[maxLenght, maxWidht];

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
        for (int i = 0; i < maxLenght; i++)
        {
            for (int j = 0; j < maxWidht; j++)
            {
                if (tiles[i, j] == null)
                {
                    tiles[i, j] = Instantiate(groundPrefab, new Vector3(i * 10, 0, j * 10), RandomRotation(), transform);
                }
            }
        }
    }

    public void ChangeMaterial(int X, int Z,bool wood = false)
    {
        if(wood)
            tiles[X, Z].gameObject.GetComponent<MeshRenderer>().material = clayWood;
        else
            tiles[X, Z].gameObject.GetComponent<MeshRenderer>().material = clayRock;
    }

    Quaternion RandomRotation()
    {
        return Quaternion.Euler(rot[Random.Range(0, 4)]);
    }
}
