using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorControl : MonoBehaviour
{
    private Animator animator;
    public CharracterJump CharracterJump;
   
    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            
            animator.Play("Jump",-1,0f);
        }

        if (CharracterJump.isGrounded)
        {
            animator.SetBool("Idle",true);
        }
        else animator.SetBool("Idle", false);

    }


    


}
