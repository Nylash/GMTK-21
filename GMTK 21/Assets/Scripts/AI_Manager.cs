using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AI_Manager : MonoBehaviour
{
    public static AI_Manager instance;

    public int frameBetweenUpdate = 10;
    public GameObject target;
    public NavMeshAgent na;
    private Animator anim;

    public AudioSource AISource;
    public AudioClip reffilClip;
    public AudioClip gameOverClip;
    public AudioClip winClip;

    public bool atEnd;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);

        na = GetComponent<NavMeshAgent>();
        anim = GetComponentInChildren<Animator>();
    }

    private void Update()
    {
        if (na.velocity.magnitude > 0)
            anim.SetBool("Move", true);
        else
            anim.SetBool("Move", false);
    }

    public void UpdatePath()
    {
        NavMeshPath newPath = new NavMeshPath();
        na.CalculatePath(target.transform.position, newPath);
        na.path = newPath;
    }

    private void OnTriggerEnter(Collider other)
    {
        switch (other.tag)
        {
            case "DeadZone":
                AISource.PlayOneShot(gameOverClip, .3f);
                break;
            case "Refill":
                CharacterManager.instance.currentAction += 3;
                if (CharacterManager.instance.currentAction > CharacterManager.instance.maxAction)
                    CharacterManager.instance.currentAction = CharacterManager.instance.maxAction;
                Destroy(other.gameObject);
                AISource.PlayOneShot(reffilClip, .3f);
                UI_Manager.instance.Refill();
                break;
            case "Finish":
                na.isStopped = true;
                atEnd = true;
                na.velocity = Vector3.zero;
                break;
            default:
                break;
        }
    }
}
