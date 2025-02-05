using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRespawn : MonoBehaviour
{
    public Vector2 spawnPoint;  // Điểm hồi sinh của người chơi
    public float respawnTime = 1f;  // Thời gian chờ trước khi hồi sinh
    public GameObject BodyPlayer;
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
    }

    // Phương thức hồi sinh người chơi
    private void Respawn()
    {
        // Di chuyển người chơi về điểm hồi sinh
        transform.position = spawnPoint;
        BodyPlayer.SetActive(true);
    }
}
