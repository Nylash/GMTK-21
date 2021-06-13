using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimMethod : MonoBehaviour
{
    void EndAction()
    {
        CharacterManager.instance.doingAction = false;
    }
}
