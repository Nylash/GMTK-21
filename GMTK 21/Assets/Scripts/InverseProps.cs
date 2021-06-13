using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InverseProps : MonoBehaviour
{
    public Material materialRed;

    public GameObject[] props;
    GameObject currentProps;

    private void Start()
    {
        props = GameObject.FindGameObjectsWithTag("Props");
        foreach (GameObject item in props)
        {
            currentProps = Instantiate(item, new Vector3(-item.transform.position.x, item.transform.position.y, item.transform.position.z), Quaternion.identity, gameObject.transform);
            print(currentProps);
            if (currentProps.GetComponent<MeshRenderer>())
                currentProps.GetComponent<MeshRenderer>().material = materialRed;
        }
    }
}
