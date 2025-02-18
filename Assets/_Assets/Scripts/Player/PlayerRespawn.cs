using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class PlayerRespawn : MonoBehaviour
{
    public Transform StartNest;  // Điểm hồi sinh của người chơi
    public float respawnTime = 1f;  // Thời gian chờ trước khi hồi sinh
    public GameObject BodyPlayer;
    public CinemachineVirtualCamera Camera;
    public Rigidbody2D Rigidbody2d;

    public CircleCollider2D Collider2d;
    public bool IsCheckPointed;
    public bool Respawned;

    public int DieCount;
    public GameObject Effect;
    public Animator transition;

    private void Awake()
    {
        Effect = GameObject.Find("Scene Trandition");
        Rigidbody2d = GetComponent<Rigidbody2D>();
        Collider2d = GetComponent<CircleCollider2D>();
    }

    private void Start()
    {
        Invoke("OffEffect", 0.5f);
    }

    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Sawblade"))  
        {
            Die();
            Collider2d.enabled = false;
            Respawned = true;
        }
    }

    // Phương thức xử lý khi người chơi chết
    private void Die()
    {
        Invoke("Respawn", respawnTime);
        StartCoroutine(Loading());
    }

    IEnumerator Loading()
    {
        Effect.SetActive(true);
        transition.SetTrigger("End");
        yield return new WaitForSeconds(0.5f);
        
    }

    // Phương thức hồi sinh người chơi
    protected virtual void Respawn()
    {
        // Di chuyển người chơi về điểm hồi sinh
        transition.SetTrigger("Start");
        Invoke("OffEffect", 0.5f);
        Respawned = false;
        DieCount++;
        Collider2d.enabled = true;
        if (DieCount == 3) return;
       
        transform.position = StartNest.position + new Vector3(0, 0.8f, 0);
        Rigidbody2d.constraints = RigidbodyConstraints2D.FreezeAll;
        
        
        BodyPlayer.SetActive(true);
        Camera.enabled= true;
        
    }

    void OffEffect()
    {
        Effect.SetActive(false);
    }

    

    

}
