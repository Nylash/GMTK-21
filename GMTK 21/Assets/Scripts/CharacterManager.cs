using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterManager : MonoBehaviour
{
    public float playerSpeed = 2.0f;
    public float turnSmoothTime = 0.1f;
    public GameObject safeZonePrefab;
    public GameObject navMeshObject;

    private CharacterController controller;
    private float turnSmoothVelocity;

    private void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;

        if(direction.magnitude >= 0.1f)
        {
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);

            controller.Move(direction * playerSpeed * Time.deltaTime);
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            StartCoroutine(NavMeshManager.instance.ChangeBlock(Mathf.RoundToInt(transform.position.x / 10), Mathf.RoundToInt(transform.position.z / 10)));
        }
    }
}
