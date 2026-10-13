using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class startGame : MonoBehaviour
{
    AudioSource audioSource;
    public AudioClip sound; // 音效
    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }
    public void StartGame()
    {
        audioSource.PlayOneShot(sound);
        Invoke("LoadNextScene", 1f); // 延迟1秒后加载下一场景
    }

    private void LoadNextScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
