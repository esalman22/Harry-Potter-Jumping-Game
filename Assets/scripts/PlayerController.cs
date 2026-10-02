using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed = 5f;
    public float jumpForce = 7f;

    [Header("Ground Check")]
    public Transform groundCheck;
    public float groundCheckRadius = 0.2f;
    public LayerMask groundLayer;

    private Rigidbody2D rb;
    private bool isGrounded;
    private float moveInput;

    [Header("Jump Limit")]
    public float maxJumpForce = 12f;

    public float HorizontalJumpFactor = 100f;

    [Header("Death")]
    public float fallLimitY = -13f;
    private bool isDead;

    private Vector3 startPos;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        startPos = transform.position;
    }

    void Update()
    {
        // ======== حركة يمين/شمال ========
        moveInput = Input.GetAxis("Horizontal");
        rb.linearVelocity = new Vector2(moveInput * moveSpeed, rb.linearVelocity.y);

        // ======== Ground Check ========
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);

        // ======== Flip Sprite ========
        if (moveInput > 0) transform.localScale = new Vector3(1, 1, 1);
        else if (moveInput < 0) transform.localScale = new Vector3(-1, 1, 1);

        // ======== القفز ========
        if (Input.GetButtonDown("Jump"))
        {
            if (isGrounded)
            {
                float totalJumpForce =
                    jumpForce + Mathf.Abs(rb.linearVelocity.x) * HorizontalJumpFactor;

                totalJumpForce = Mathf.Clamp(totalJumpForce, jumpForce, maxJumpForce);

                rb.linearVelocity = new Vector2(rb.linearVelocity.x, totalJumpForce);
            }

        }

        // موت بالسقوط
        if (transform.position.y < fallLimitY)
        {
            Die();
        }

    }

    void Die()
    {
        if (isDead) return;
        isDead = true;

        rb.linearVelocity = Vector2.zero;
        rb.bodyType = RigidbodyType2D.Static;

        GameManager.instance.GameOver();
    }

    public void ResetPlayer()
    {
        transform.position = startPos;
        // 👇 لو عندك Rigidbody
        Rigidbody rb = GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.linearVelocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
        }

    }
}
