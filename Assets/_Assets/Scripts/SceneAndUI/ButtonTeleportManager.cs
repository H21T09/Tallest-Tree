using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class ButtonTeleportManager : MonoBehaviour
{
    public List<Button> teleportButtons;
    public List<Transform> teleportTargets;
    public Animator PlayerAni;
    public Transform player;

    private const string LastTeleportKey = "LastTeleportIndex"; // Key lưu vị trí cuối cùng

    void Start()
    {
        if (teleportButtons.Count != teleportTargets.Count)
        {
            Debug.LogError("Số lượng button và target không khớp!");
            return;
        }

        // Load vị trí player khi bắt đầu game
        LoadLastTeleportPosition();

        for (int i = 0; i < teleportButtons.Count; i++)
        {
            int index = i;
            teleportButtons[i].onClick.AddListener(() => StartCoroutine(TeleportPlayerAndScroll(index)));
        }
    }

    private IEnumerator TeleportPlayerAndScroll(int index)
    {
        PlayerAni.SetTrigger("Tele");
        yield return new WaitForSeconds(0.2f);

        if (index >= 0 && index < teleportTargets.Count)
        {
            player.position = teleportTargets[index].position + new Vector3(0, 0.9f, 0);

            // Lưu lại index vị trí cuối cùng
            PlayerPrefs.SetInt(LastTeleportKey, index);
            PlayerPrefs.Save();
        }
    }

    void LoadLastTeleportPosition()
    {
        if (PlayerPrefs.HasKey(LastTeleportKey))
        {
            int lastIndex = PlayerPrefs.GetInt(LastTeleportKey);
            if (lastIndex >= 0 && lastIndex < teleportTargets.Count)
            {
                player.position = teleportTargets[lastIndex].position + new Vector3(0, 0.9f, 0);
            }
        }
    }

    public void TeleportToNextButton()
    {
        int currentIndex = PlayerPrefs.GetInt(LastTeleportKey, 0);
        int nextIndex = currentIndex + 1;

        if (nextIndex < teleportTargets.Count)
        {
            StartCoroutine(TeleportPlayerAndScroll(nextIndex));
        }
    }
}
