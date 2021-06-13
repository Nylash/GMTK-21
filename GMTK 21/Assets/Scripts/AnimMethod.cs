using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimMethod : MonoBehaviour
{
    public AudioSource left;
    public AudioSource right;
    public AudioClip leftClip;
    public AudioClip rightClip;

    void EndAction()
    {
        CharacterManager.instance.doingAction = false;
    }

    void LeftSound()
    {
        left.PlayOneShot(leftClip);
    }

    void RightSound()
    {
        right.PlayOneShot(rightClip);
    }

    void DoChange()
    {
        CharacterManager.instance.ApplyChange();
    }
}
