using UnityEngine;
using UnityEngine.UI;  // 引用 UI 命名空間
using TMPro;


public class WaterPlayerOnBeach : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float rightBoundary = 10f;
    public float leftBoundary = -10f;

    private Vector2 movement;
    private Animator animator;
    private SpriteRenderer spriteRenderer;
    private Rigidbody2D rb;

    public float cameraSmoothSpeed = 5f;
    private const float inputThreshold = 0.1f;

    private bool isTransforming = true;
    private float transformDelay = 4f;  // 變身延遲時間
    private float transformTimer = 0f;
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (isTransforming)
        {
            // 計時並進行變身
            transformTimer += Time.deltaTime;
            if (transformTimer >= transformDelay)
            {
                isTransforming = false;
            }
            return;
        }
        float horizontal = Input.GetAxisRaw("Horizontal");
        movement = new Vector2(horizontal, 0);

        if (horizontal > 0)
        {
            spriteRenderer.flipX = false;
        }
        else if (horizontal < 0)
        {
            spriteRenderer.flipX = true;
        }

    }

    void FixedUpdate()
    {
        if (isTransforming)
        {
            // 在變身過程中禁止移動，並將角色位置保持不變
            rb.velocity = Vector2.zero;
            return;
        }
        // 避免小移動值導致無效移動
        if (movement.sqrMagnitude > 0.01f)
        {
            Vector2 targetPosition = (Vector2)transform.position + movement.normalized * moveSpeed * Time.fixedDeltaTime;
            targetPosition.x = Mathf.Clamp(targetPosition.x, leftBoundary, rightBoundary);

            if (rb != null)
            {
                rb.MovePosition(targetPosition);
            }
        }
    }
}
