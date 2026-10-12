using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;//重新加载场景时需要引入这个

public class playerLife : MonoBehaviour
{
    public ParticleSystem playerPs;
    private Animator anim;
    private Rigidbody2D rb;
    private AudioSource audioSource;

    [SerializeField] private AudioClip deathSound; // 死亡音效
    [SerializeField] private AudioClip deathSoundByEnemy; // 被敌人击中时的死亡音效

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        audioSource = GetComponent<AudioSource>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Spikes" || collision.gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {
            Death();
        }
    }
    private void Death()
    {
        audioSource.PlayOneShot(deathSound); // 播放死亡音效
        GetComponent<PlayerMovement>().enabled = false;
        rb.bodyType = RigidbodyType2D.Static; // 停止玩家的移动
        Destroy(playerPs, 1f);
        anim.SetTrigger("death");
        //在这里可以添加其他死亡逻辑，比如重新加载场景等
    }
    // private void EnemyDeath()
    // {
    //     audioSource.PlayOneShot(deathSoundByEnemy); // 播放被敌人击中时的死亡音效
    //     GetComponent<PlayerMovement>().enabled = false;
    //     rb.bodyType = RigidbodyType2D.Static; // 停止玩家的移动
    //     Destroy(playerPs, 1f);
    //     anim.SetTrigger("death");
    //     //在这里可以添加其他死亡逻辑，比如重新加载场景等
    // }

    //重新加载场景
    private void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "DieLine")
        {
            audioSource.PlayOneShot(deathSound); // 播放死亡音效
            Invoke("RestartLevel", 0.2f); // 延迟0.2秒后重新加载场景
        }
    }
}
