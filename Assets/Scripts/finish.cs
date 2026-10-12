using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class finish : MonoBehaviour
{
    private AudioSource audioSource;
    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //当玩家碰到终点时，播放音效并加载下一场景
        if (collision.CompareTag("Player"))
        {
            audioSource.Play();
            Invoke("LoadNextScene", 1f); // 延迟1秒后加载下一场景
        }
    }

    private void LoadNextScene()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int nextSceneIndex = currentSceneIndex + 1;

        // 检查是否有下一场景
        if (nextSceneIndex < SceneManager.sceneCountInBuildSettings)
        {
            SceneManager.LoadScene(nextSceneIndex);
        }
        else
        {
            Debug.Log("已经是最后一关了！");
            // 可以在这里添加游戏结束的逻辑，比如返回主菜单等
        }
    }
}
