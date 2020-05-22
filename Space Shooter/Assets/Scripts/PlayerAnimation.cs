using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    private Animator _anim;


    void Start()
    {
        _anim = GetComponent<Animator>();
    }


    public void MoveLeft()
    {
        _anim.SetBool("Turn Left", true);
        _anim.SetBool("Turn Right", false);
    }

    public void MoveRight()
    {
        _anim.SetBool("Turn Right", true);
        _anim.SetBool("Turn Left", false);
    }

    public void SetIdle()
    {
        _anim.SetBool("Turn Right", false);
        _anim.SetBool("Turn Left", false);
    }

    public void SetHorizontalAnimation(float axis)
    {
        if(axis == 0)
        {
            SetIdle();
        }
        else if(axis < 0)
        {
            MoveLeft();
        }
        else if(axis > 0)
        {
            MoveRight();
        }

    }

}
