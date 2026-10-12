using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    //引用
    Rigidbody2D rb;
    Animator anim;
    AudioSource audioSource;
    // GameObject ObjectCube;



    public ParticleSystem playerPs;


    public float playerSpeed = 5f;
    [Range(1, 10)]
    public float jumpSpeed = 5f;//跳跃速度
    public bool isGrounded;//是否在地面上
    public Transform groundCheck;//检测地面位置
    public LayerMask ground;//地面图层
    public float fallAddition = 3.5f;//下落加速度
    public float jumpAddition = 1.5f;//起跳加速度
    public int jumpCount = 2;//跳跃次数
    public AudioClip jumpSound; // 跳跃音效
    public AudioClip FlipSound; // 翻转音效

    private float moveX;
    private bool facingRight = true;
    private bool moveJump;//跳跃
    private bool jumpHold;//长按跳跃
    private bool isJump;//是否跳跃
    private bool wasGrounded;//上一物理帧是否在地面上

    private enum playerState { idle, run, jump, fall, doubleJump };//枚举->使用枚举的方式串联所有动画的切换 /idle：0 run：1 jump：2 fall：3 doubleJump：4
    [SerializeField] private Transform killPoint; // 用于玩家踩怪物致怪物死亡的检测点




    //一开始调用
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
        killPoint = transform.Find("killPoint"); // 初始化killPoint，查找名为 "KillPoint" 的子对象
    }

    // Update is called once per frame
    void Update()
    {
        moveX = Input.GetAxis("Horizontal"); //-1,0,1
        moveJump = Input.GetButtonDown("Jump");
        jumpHold = Input.GetButton("Jump");
        if (moveJump && jumpCount > 0)
        {
            // rb.velocity = Vector2.up * jumpSpeed;
            isJump = true;
            // jumpCount--;
            PPS();
        }
    }
    private void FixedUpdate()//物理
    {
        //圆形地面检测
        bool groundedNow = Physics2D.OverlapCircle(groundCheck.position, 0.2f, ground) != null;

        // 只在从空中落到地面的瞬间恢复跳跃次数，避免起跳后检测圆仍接触地面而重复重置
        if (groundedNow && !wasGrounded)
        {
            jumpCount = 2;
        }

        isGrounded = groundedNow;
        wasGrounded = groundedNow;
        Move();
        Jump();
        // IsRunning();
        // IsJumping();
        AnimationState();
        Enemy();
    }
    //移动
    private void Move()
    {
        // anim.SetFloat("speed", Mathf.Abs(moveX));
        rb.velocity = new Vector2(moveX * playerSpeed, rb.velocity.y);
        // if (moveX != 0)
        // {
        //     transform.localScale = new Vector3(moveX, 1, 1);
        // }
        if (moveX > 0 && !facingRight)
        {
            Flip();
        }
        else if (moveX < 0 && facingRight)
        {
            Flip();
        }
    }
    //翻转
    private void Flip()
    {
        // ObjectCube = GameObject.FindGameObjectWithTag('Grassland');
        audioSource.PlayOneShot(FlipSound); // 播放翻转音效
        PPS();
        facingRight = !facingRight;
        transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
    }

    //跳跃
    private void Jump()
    {
        if (isJump)
        {
            audioSource.PlayOneShot(jumpSound); // 播放跳跃音效
            rb.AddForce(Vector2.up * jumpSpeed, ForceMode2D.Impulse);
            jumpCount--;
            isJump = false;
        }
        if (rb.velocity.y < 0)
        {
            // rb.velocity += Vector2.up * Physics2D.gravity.y * (fallAddition - 1) * Time.fixedDeltaTime;
            rb.gravityScale = fallAddition;
        }
        else if (rb.velocity.y > 0 && !jumpHold)
        {
            // rb.velocity += Vector2.up * Physics2D.gravity.y * (jumpAddition - 1) * Time.fixedDeltaTime;
            rb.gravityScale = jumpAddition;
        }
        else
        {
            rb.gravityScale = 1f;
        }
    }

    //灰尘粒子
    void PPS()
    {
        playerPs.Play();
    }
    void IsRunning()
    {
        anim.SetBool("running", moveX != 0);
        // if (Mathf.Abs(moveX) > 0)
        // {
        //anim.SetBool("running", true);
        // }
        // else
        // {
        //     anim.SetBool("running", false);
        // }
    }
    void IsJumping()
    {
        anim.SetBool("jumping", !isGrounded);
    }

    //枚举实现动画切换
    void AnimationState()
    {
        playerState state;
        if (moveX != 0)
        {
            state = playerState.run;
        }
        else
        {
            state = playerState.idle;
        }
        if (rb.velocity.y > 0.1f)
        {
            state = playerState.jump;
        }
        else if (rb.velocity.y < -0.1f)
        {
            state = playerState.fall;
        }
        //二段跳
        if (moveJump || jumpHold && rb.velocity.y > 0.1f)
        {
            state = playerState.doubleJump;
        }
        anim.SetInteger("state", (int)state);
    }

    private void Enemy()
    {
        // Collider2D enemyCollider = Physics2D.OverlapCircle(killPoint.position, 0.1f, LayerMask.GetMask("Enemy"));//(原点位置，半径，图层)
        Collider2D enemyCollider = Physics2D.OverlapBox(killPoint.position, new Vector2(0.3f, 0.6f), 0f, LayerMask.GetMask("Enemy"));//(原点位置，大小，角度，图层)
        if (enemyCollider == null)
        {
            return;
        }
        Destroy(enemyCollider.gameObject);
        rb.velocity = new Vector2(rb.velocity.x, jumpSpeed / 1.5f); // 给玩家一个向上的反弹力

    }
}
