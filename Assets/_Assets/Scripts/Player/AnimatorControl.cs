using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class AnimatorControl : MonoBehaviour
{
    private Animator animator;
    public CharracterJump CharracterJump;
    public WinPoint winPoint;
    public PlayerDash playerDash;
    public PlayerRespawn PlayerRespawn;
    
    private void Awake()
    {
        animator = GetComponent<Animator>();
        PlayerRespawn = GetComponentInParent<PlayerRespawn>();
    }

    private void Update()
    {
        AniWin();
        AniJumpAndDash();
        AniIdle();
        
    }

    void AniWin()
    {
        if(winPoint.IsWin)
        {
            animator.SetBool("Victory", true);
        }
    }

    void AniJumpAndDash()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0)&&!IsPointerOverUIElement())
        {
            animator.Play("Jump", -1, 0f);
        }

        else if (playerDash.IsDash)
        {
            animator.Play("Dash",0, 0f);
            playerDash.IsDash = false;
        }
    }

    private bool IsPointerOverUIElement()
    {
        if (Input.touchCount > 0)
        {
            return EventSystem.current.IsPointerOverGameObject(Input.GetTouch(0).fingerId);
        }
        return EventSystem.current.IsPointerOverGameObject();
    }

    void AniIdle()
    {
        if (CharracterJump.isGrounded)
        {
            animator.SetBool("Idle", true);
        }
        else animator.SetBool("Idle", false);
        if (PlayerRespawn.Respawned)
        {
            animator.Play("Idle");
        }

    }






}
