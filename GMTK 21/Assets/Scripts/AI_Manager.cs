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

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);

        na = GetComponent<NavMeshAgent>();
    }

    public void UpdatePath(bool forced = false)
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
                print("perdu");
                break;
            case "Refill":
                CharacterManager.instance.currentAction += 3;
                if (CharacterManager.instance.currentAction > CharacterManager.instance.maxAction)
                    CharacterManager.instance.currentAction = CharacterManager.instance.maxAction;
                Destroy(other.gameObject);
                CharacterManager.instance.fillImage.fillAmount = (float)CharacterManager.instance.currentAction / 10;
                break;
            case "Finish":
                print("win");
                break;
            default:
                break;
        }
    }
}
