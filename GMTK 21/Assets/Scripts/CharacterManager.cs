using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterManager : MonoBehaviour
{
    public float playerSpeed = 2.0f;
    public float turnSmoothTime = 0.1f;
    public int maxAction = 10;
    public int currentAction;
    public GameObject safeZonePrefab;
    public GameObject navMeshObject;
    public Image fillImage;

    private CharacterController controller;
    private float turnSmoothVelocity;

    public static CharacterManager instance;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);
    }

    private void Start()
    {
        controller = GetComponent<CharacterController>();
        currentAction = maxAction;
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
            if(currentAction-1 >= 0)
            {
                currentAction--;
                StartCoroutine(NavMeshManager.instance.ChangeBlock(Mathf.RoundToInt(transform.position.x / 10), Mathf.RoundToInt(transform.position.z / 10)));
                fillImage.fillAmount = (float)currentAction / 10;
            }
        }
    }
}
