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

    public int DieCount;
    public GameObject Effect;
    public Animator transition;

    private void Start()
    {
        Invoke("OffEffect", 1f);
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Sawblade"))  
        {
            Die();
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
        yield return new WaitForSeconds(1f);
        
    }

    // Phương thức hồi sinh người chơi
    private void Respawn()
    {
        // Di chuyển người chơi về điểm hồi sinh
        transition.SetTrigger("Start");
        Invoke("OffEffect", 1f);
        DieCount++;
        if (DieCount == 3) return;

        transform.position = StartNest.position + new Vector3(0, 1f, 0);
        BodyPlayer.SetActive(true);
        Camera.enabled= true;
        
    }

    void OffEffect()
    {
        Effect.SetActive(false);
    }
}
