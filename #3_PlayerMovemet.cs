using System.Collections;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float jumpForce = 11f; //11f
    public GameManager gameManager;
    public AudioSource bgmAudioSource;

    private Rigidbody2D rb;
    private bool isGrounded = false;
    private bool isJumping = false;
    private bool isDead = false;

    public Transform groundCheck;
    public float groundCheckRadius = 0.5f;
    public LayerMask groundLayer;
    bool isFalling = false;

    
    float coyoteTimeCounter = 0f;

    public float slowMotionDistance = 8f;
    bool isSlowMotion = false;



    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.constraints = RigidbodyConstraints2D.FreezeRotation;
    }

    void Update()
    {
        
        if (isDead) return;
        groundCheck.position = new Vector2(transform.position.x, transform.position.y - 0.5f);
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);

       // 낙하속도 
        if (rb.linearVelocity.y > -4f)
            rb.gravityScale = 5f; //4f
        else
        {
            rb.gravityScale = 0f;
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, -12f); //10f
            isFalling = true;
        }



        if ((Input.GetKey(KeyCode.Space) || Input.touchCount>0) && isGrounded && !isJumping)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
            isJumping = true;
            coyoteTimeCounter = 0f;
            rb.constraints = RigidbodyConstraints2D.None;
            StartCoroutine(RotatePlayer());
        }

        if (isGrounded && rb.linearVelocity.y <= 0)
        {
            isJumping = false;
            isFalling = false;
        }

        Collider2D hit = Physics2D.OverlapBox(
            transform.position,
            new Vector2(0.9f, 0.9f),
            0f,
            LayerMask.GetMask("obstacle")
        );
        Collider2D side = Physics2D.OverlapBox(
            new Vector2(transform.position.x + 0.55f, transform.position.y),
            new Vector2(0.1f, 0.7f),
            0f,
            LayerMask.GetMask("platform")
        );
       
        Collider2D end = Physics2D.OverlapBox(
            transform.position,
            new Vector2(0.9f, 0.9f),0f,
            LayerMask.GetMask("EndMark")
            );
        
        if(end!=null)
        {
            gameManager.GameEnd();
        }
        
        if (side != null || hit != null)
        {
            isDead = true;

            bgmAudioSource.Stop();
            gameManager.GameOver();
        }
        // EndMark 감지
        Collider2D endRange = Physics2D.OverlapBox(
            
            new Vector2(transform.position.x + slowMotionDistance, transform.position.y), // 앞쪽으로 이동
            new Vector2(1f, 0.9f),
            0f,
            LayerMask.GetMask("EndMark")
        );

        if (endRange != null && !isSlowMotion)
        {
            isSlowMotion = true;
            Time.timeScale = 0.2f; // 슬로우모션
            Time.fixedDeltaTime = 0.02f * Time.timeScale;

            // 자동 점프
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
            isJumping = true;
            rb.constraints = RigidbodyConstraints2D.None;
            StartCoroutine(RotatePlayer());
        }

    }

    IEnumerator RotatePlayer()
    {
        float rotated = 0f;
        float rotateSpeed = 200f;

        while (rotated < 90f)
        {
            float step = rotateSpeed * Time.deltaTime;
            if (rotated + step > 90f) step = 90f - rotated;
            transform.Rotate(0f, 0f, -step);
            rotated += step;
            yield return null;
        }
        float z = Mathf.Round(transform.eulerAngles.z / 90f) * 90f;
        transform.eulerAngles = new Vector3(0f, 0f, z);
        rb.constraints = RigidbodyConstraints2D.FreezeRotation;
    }
}
