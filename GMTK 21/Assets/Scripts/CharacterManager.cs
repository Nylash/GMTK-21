using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CharacterManager : MonoBehaviour
{
    public float playerSpeed = 2.0f;
    public float turnSmoothTime = 0.1f;
    public int maxAction = 10;
    public int currentAction;
    public GameObject safeZonePrefab;
    public GameObject navMeshObject;
    public bool doingAction;
    public bool atEnd;

    private bool finished;
    private CharacterController controller;
    private float turnSmoothVelocity;
    private Animator anim;
    private bool firstUpdate = true;
    public bool defeat;

    public AudioSource twitSource;
    public AudioClip[] twitClip = new AudioClip[3];

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
        anim = GetComponentInChildren<Animator>();
        
    }

    void Update()
    {
        if (!defeat)
        {
            if (atEnd && AI_Manager.instance.atEnd && !finished)
            {
                finished = true;
                anim.SetBool("Move", false);
                AI_Manager.instance.AISource.PlayOneShot(AI_Manager.instance.winClip, .8f);
                SceneManager.LoadScene("Scene Victory");
            }
            else
            {
                if (firstUpdate)
                {
                    firstUpdate = false;
                    NavMeshManager.instance.ChangeBlock(Mathf.RoundToInt(transform.position.x / 10), Mathf.RoundToInt(transform.position.z / 10));
                }
                float horizontal = Input.GetAxisRaw("Horizontal");
                float vertical = Input.GetAxisRaw("Vertical");
                Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;

                if (direction.magnitude >= 0.1f && !doingAction)
                {
                    anim.SetBool("Move", true);
                    float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;
                    float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
                    transform.rotation = Quaternion.Euler(0f, angle, 0f);

                    controller.Move(direction * playerSpeed * Time.deltaTime);
                }
                else
                {
                    anim.SetBool("Move", false);
                }

                if (Input.GetKeyDown(KeyCode.E) && !doingAction)
                {
                    if (currentAction - 1 >= 0)
                    {
                        anim.SetTrigger("Action");
                        twitSource.PlayOneShot(twitClip[Random.Range(0, twitClip.Length)]);
                        doingAction = true;
                        currentAction--;
                    }
                }
            }
        }
        else
        {
            anim.SetBool("Move",false);
        }
    }

    public void ApplyChange()
    {
        NavMeshManager.instance.ChangeBlock(Mathf.RoundToInt(transform.position.x / 10), Mathf.RoundToInt(transform.position.z / 10));
        UI_Manager.instance.HideLast();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Finish"))
            atEnd = true;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Finish"))
            atEnd = false;
    }
}
