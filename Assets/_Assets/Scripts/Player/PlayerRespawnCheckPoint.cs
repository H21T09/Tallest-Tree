using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRespawnCheckPoint : PlayerRespawn
{
    public Transform CheckPoint;
    private void Awake()
    {
        Effect = GameObject.Find("Scene Trandition");
        Rigidbody2d = GetComponent<Rigidbody2D>();
        Collider2d = GetComponent<CircleCollider2D>();
        GameObject checkpointObject = GameObject.Find("CheckPoint");
        if (checkpointObject != null)
        {
            CheckPoint = checkpointObject.transform;
        }
    }

    private void Update()
    {
        if (transform.position.y > CheckPoint.position.y)
        {
            IsCheckPointed = true;
        }
    }

    protected override void Respawn()
    {
        base.Respawn();
        if (IsCheckPointed)
        {
            transform.position = CheckPoint.position + new Vector3(0, 0.8f, 0);
            Rigidbody2d.constraints = RigidbodyConstraints2D.FreezeAll;
        }
        else
        {
            transform.position = StartNest.position + new Vector3(0, 0.8f, 0);
            Rigidbody2d.constraints = RigidbodyConstraints2D.FreezeAll;
        }
    }
}
    
