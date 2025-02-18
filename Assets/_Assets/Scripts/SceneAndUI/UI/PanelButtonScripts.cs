using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PanelButtonScripts : MonoBehaviour
{
    public List<Button> Menu;
    private int selected = 1;
    public GameObject Effect;
    public Animator Trandition;

    void Start()
    {
        // Gán sự kiện cho mỗi Button Way
        for (int i = 0; i < Menu.Count; i++)
        {
            int index = i; // Lưu index để tránh lỗi delegate
            Menu[i].onClick.AddListener(() => Select(index));
        }
    }

    void Select(int wayIndex)
    {
        selected = wayIndex + 1;
        NextMenu();
    }

    void NextMenu()
    {
        Effect.SetActive(true);
        Trandition.SetTrigger("End");
        StartCoroutine(WaitForAnimationAndLoadGame());
    }

    IEnumerator WaitForAnimationAndLoadGame()
    {

        yield return new WaitForSeconds(0.5f);

        LoadMenu();
    }

    void LoadMenu()
    {
        string sceneName = "MenuGame" + selected;
        SceneManager.LoadScene(sceneName);
    }
}
