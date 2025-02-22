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
    public AudioClip soundEffect;
    private AudioSource audioSource;
    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
        audioSource.playOnAwake = false;
        audioSource.clip = soundEffect;
    }

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
        audioSource.Play();
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
