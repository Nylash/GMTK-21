using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamerasMover : MonoBehaviour
{
    public Transform follow;
    public GameObject otherCam;
    public float speedDamp = 0.5f;
    float currentVelocity;

    void Update()
    {
        transform.position = new Vector3(Mathf.SmoothDamp(transform.position.x, follow.position.x, ref currentVelocity, speedDamp), transform.position.y, transform.position.z);

        otherCam.transform.position = new Vector3(-transform.position.x, otherCam.transform.position.y, otherCam.transform.position.z);
    }
}
