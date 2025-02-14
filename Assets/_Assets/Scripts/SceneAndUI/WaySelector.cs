using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class WaySelector : MonoBehaviour
{
    public List<Button> NumberWay;  // Danh sách các Button Way
    public Button playButton;       // Button Play
    private int selectedWay = -1;   // Lưu Way được chọn (-1 nghĩa là chưa chọn)

    void Start()
    {
        // Gán sự kiện cho mỗi Button Way
        for (int i = 0; i < NumberWay.Count; i++)
        {
            int index = i; // Lưu index để tránh lỗi delegate
            NumberWay[i].onClick.AddListener(() => SelectWay(index));
        }

        // Gán sự kiện cho Button Play
        playButton.onClick.AddListener(PlayGame);
    }

    void SelectWay(int wayIndex)
    {
        selectedWay = wayIndex + 1;
        Debug.Log("Selected Way: " + selectedWay);
    }

    void PlayGame()
    {
        if (selectedWay == -1)
        {
            Debug.Log("Chưa chọn đường đi!");
            return;
        }

        // Chuyển scene theo Way đã chọn (đặt tên scene phù hợp)
        string sceneName = "WayScene" + selectedWay; // Ví dụ: WayScene0, WayScene1...
        SceneManager.LoadScene(sceneName);
    }
}
