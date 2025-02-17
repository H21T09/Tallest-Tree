using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Collections;

public class ButtonTeleportManager : MonoBehaviour
{
    public List<Button> teleportButtons;
    public List<Transform> teleportTargets;
    public Animator PlayerAni;
    public Transform player;

    void Start()
    {
        if (teleportButtons.Count != teleportTargets.Count)
        {
            Debug.LogError("Số lượng button và target không khớp!");
            return;
        }

        for (int i = 0; i < teleportButtons.Count; i++)
        {
            int index = i;
            teleportButtons[i].onClick.AddListener(() => StartCoroutine(TeleportPlayerWithDelay(index)));
        }
    }

    // Coroutine to teleport the player with a delay
    private IEnumerator TeleportPlayerWithDelay(int index)
    {
        PlayerAni.SetTrigger("Tele");
        yield return new WaitForSeconds(0.2f);  // Wait for 0.5 seconds

        if (index >= 0 && index < teleportTargets.Count)
        {
            // Lấy vị trí của target trong không gian thế giới
            Vector3 targetPosition = teleportTargets[index].position;

            RectTransform targetRectTransform = teleportTargets[index].GetComponent<RectTransform>();

            if (targetRectTransform != null)
            {
                Vector3 worldPos = targetRectTransform.position;
                player.position = worldPos + new Vector3(0, 0.8f, 0);
                
            }
            else
            {
                player.position = targetPosition;
            }
        }
    }
}
