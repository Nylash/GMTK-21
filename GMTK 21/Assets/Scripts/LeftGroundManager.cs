using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeftGroundManager : MonoBehaviour
{
    public static LeftGroundManager instance;

    public GameObject groundPrefab;
    public static int maxLenght = 50;
    public static int maxWidht = 25;
    public GameObject[,] tiles = new GameObject[maxLenght, maxWidht];

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);
    }

    private void Start()
    {
        for (int i = 0; i < maxLenght; i++)
        {
            for (int j = 0; j < maxWidht; j++)
            {
                if (tiles[i, j] == null)
                {
                    tiles[i, j] = Instantiate(groundPrefab, new Vector3(i * 10, 0, j * 10), Quaternion.identity, transform);
                }
            }
        }
    }
}
