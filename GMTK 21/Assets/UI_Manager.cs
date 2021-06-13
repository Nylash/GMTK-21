using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Manager : MonoBehaviour
{
    public static UI_Manager instance;
    public float rotSpeed = .5f;
    public MeshRenderer[] blocks = new MeshRenderer[10];

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);
    }

    public void Refill()
    {
        for (int i = 0; i < blocks.Length; i++)
        {
            if (!blocks[i].enabled)
            {
                blocks[i].enabled = true;
                if(i+1 < blocks.Length)
                    blocks[i+1].enabled = true;
                if(i+2 < blocks.Length)
                    blocks[i+2].enabled = true;
                return;
            }
        }
    }

    public void HideLast()
    {
        for (int i = 9; i > -1; i--)
        {
            if (blocks[i].enabled)
            {
                blocks[i].enabled = false;
                return;
            }
        }
    }
}
