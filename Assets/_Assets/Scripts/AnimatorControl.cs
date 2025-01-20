using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorControl : MonoBehaviour
{
    private Animator animator;
    public CharracterJump CharracterJump;
    public WinPoint winPoint;
   
    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        AniWin();
        AniJump();
        AniIdle();
        
    }

    void AniWin()
    {
        if(winPoint.IsWin)
        {
            animator.SetBool("Victory", true);
        }
    }

    void AniJump()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {

            animator.Play("Jump", -1, 0f);
        }
    }

    void AniIdle()
    {
        if (CharracterJump.isGrounded)
        {
            animator.SetBool("Idle", true);
        }
        else animator.SetBool("Idle", false);
    }






}
