using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomRotation : MonoBehaviour
{
    Vector3 rot;

    private void Start()
    {
        int rand = Random.Range(0, 7);
        switch (rand)
        {
            case 0:
                rot = new Vector3(1, 1, 1);
                break;
            case 1:
                rot = new Vector3(1, 1, -1);
                break;
            case 2:
                rot = new Vector3(1, -1, -1);
                break;
            case 3:
                rot = new Vector3(-1, -1, -1);
                break;
            case 4:
                rot = new Vector3(-1, 1, -1);
                break;
            case 5:
                rot = new Vector3(1, -1, 1);
                break;
            case 6:
                rot = new Vector3(-1, 1, 1);
                break;
            default:
                break;
        }
    }

    private void Update()
    {
        transform.Rotate(rot, UI_Manager.instance.rotSpeed);
    }
}
