using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class fruits : MonoBehaviour
{
    AudioSource audioSource;
    private int fruitValue = 0; // 水果的分数值
    private int coinsValue = 0;
    [SerializeField] private Text fruitText;
    [SerializeField] private Text coinsText;
    [SerializeField] AudioClip coinsSound; // 硬币音效
    [SerializeField] AudioClip fruitsSound; // 水果音效

    private string fruitTextPrefix;
    private string coinsTextPrefix;
    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        fruitTextPrefix = fruitText.text;
        coinsTextPrefix = coinsText.text;
        UpdateFruitText();
        UpdateCoinsText();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //当碰到的物体的标签是Fruits时，销毁水果
        if (collision.gameObject.CompareTag("Fruits"))
        {
            audioSource.PlayOneShot(fruitsSound); // 播放水果音效
            Destroy(collision.gameObject);
            fruitValue++;
            UpdateFruitText();
        }
        //当碰到的物体的标签是Coins时，销毁硬币
        else if (collision.gameObject.CompareTag("Coins"))
        {
            audioSource.PlayOneShot(coinsSound); // 播放硬币音效
            Destroy(collision.gameObject);
            coinsValue++;
            UpdateCoinsText();
        }
    }
    private void UpdateFruitText()
    {
        fruitText.text = $"{fruitTextPrefix} {fruitValue}";
    }

    private void UpdateCoinsText()
    {
        coinsText.text = $"{coinsTextPrefix} {coinsValue}";
    }
}
