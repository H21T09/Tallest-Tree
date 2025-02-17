using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteSwitcher : MonoBehaviour
{
    public Sprite sprite1;   // Sprite thứ nhất
    public Sprite sprite2;   // Sprite thứ hai
    private SpriteRenderer spriteRenderer;

    public float switchInterval = 0.5f;  // Thời gian thay đổi giữa hai sprite
    public float timestart;
    private bool usingSprite1 = true;

    void Start()
    {
        // Lấy component SpriteRenderer
        spriteRenderer = GetComponent<SpriteRenderer>();

        // Đặt sprite ban đầu
        spriteRenderer.sprite = sprite1;

        // Bắt đầu thay đổi sprite liên tục
        InvokeRepeating("SwitchSprite", timestart, switchInterval);
    }

    void SwitchSprite()
    {
        if (usingSprite1)
        {
            spriteRenderer.sprite = sprite2;
        }
        else
        {
            spriteRenderer.sprite = sprite1;
        }

        usingSprite1 = !usingSprite1; // Đảo trạng thái
    }
}
