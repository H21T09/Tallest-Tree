using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;

public class ButtonTeleportManager : MonoBehaviour
{
    public List<Button> teleportButtons;
    public List<Transform> teleportTargets;
    public Animator PlayerAni;
    public Transform player;

    private const string LastTeleportKey = "LastTeleportIndex"; // Key lưu vị trí cuối cùng
    private const string FromGameSceneKey = "FromGameScene"; // Key để kiểm tra có quay từ scene game không
    private const string Check = "CheckLast"; 

    void Start()
    {
        if (teleportButtons.Count != teleportTargets.Count)
        {
            Debug.LogError("Số lượng button và target không khớp!");
            return;
        }

        // Kiểm tra nếu quay về từ scene game
        if (PlayerPrefs.GetInt(FromGameSceneKey, 0) == 1)
        {
            int index = PlayerPrefs.GetInt(LastTeleportKey);
            if (index == 0) index++;
            player.position = teleportTargets[index -1].position + new Vector3(0, 0.9f, 0);
            StartCoroutine(DelayedLoadLastTeleportPosition(1f)); // Delay 5 giây
            PlayerPrefs.SetInt(FromGameSceneKey, 0); // Reset lại trạng thái
            PlayerPrefs.Save();
        }
        else
        {
            LoadLastTeleportPosition(); // Cập nhật ngay lập tức khi ở scene menu
        }

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

    private IEnumerator DelayedLoadLastTeleportPosition(float delay)
    {
        
        yield return new WaitForSeconds(delay);
        PlayerAni.SetTrigger("Tele");
        yield return new WaitForSeconds(0.2f);
        LoadLastTeleportPosition();
  
    }

    public void TeleportToNextButton()
    {
        int currentIndex = PlayerPrefs.GetInt(LastTeleportKey, 1);
        int nextIndex = currentIndex + 1;

        if (nextIndex < teleportTargets.Count)
        {
            StartCoroutine(TeleportPlayerAndScroll(nextIndex));
        }
    }

    public void DelayedTeleportToNextButton()
    {
        StartCoroutine(DelayTeleportCoroutine());
    }

    private IEnumerator DelayTeleportCoroutine()
    {
        yield return new WaitForSeconds(3f);
        TeleportToNextButton();
    }

    // Gọi hàm này trước khi chuyển scene từ game về menu
    public static void SetReturningFromGame()
    {
        
        PlayerPrefs.SetInt(FromGameSceneKey, 1);
        PlayerPrefs.Save();
    }
}
