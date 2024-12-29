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
    public float acceleration = 10f;  // 加速度
    public float maxSpeed = 10f;     // 最大速度
    public float decelerationDistance = 1f;  // 開始減速的距離
    public float sprintMultiplier = 3f;   // 衝刺時的速度
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
        /*float horizontal = Input.GetAxisRaw("Horizontal");
        movement = new Vector2(horizontal, 0);

        if (horizontal > 0)
        {
            spriteRenderer.flipX = false;
        }
        else if (horizontal < 0)
        {
            spriteRenderer.flipX = true;
        }*/
        Move();
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
        /*if (movement.sqrMagnitude > 0.01f)
        {
            Vector2 targetPosition = (Vector2)transform.position + movement.normalized * moveSpeed * Time.fixedDeltaTime;
            targetPosition.x = Mathf.Clamp(targetPosition.x, leftBoundary, rightBoundary);

            if (rb != null)
            {
                rb.MovePosition(targetPosition);
            }
        }*/
    }
    protected private void Move()
    {
        // 檢測左鍵是否按下
        if (Input.GetMouseButton(0))
        {
            // 獲取鼠標的位置並將其轉換為世界座標
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePosition.z = 0f;  // 設定 z 軸，確保角色只在 2D 平面移動

            // 計算角色與鼠標之間的距離
            Vector2 direction = (mousePosition - transform.position).normalized;
            if(direction.x < 0){
                transform.localScale = new Vector3(-0.8f, 0.8f, 1);
            }
            else{
                transform.localScale = new Vector3(0.8f, 0.8f, 1);
            }
            float distance = Vector2.Distance(mousePosition, transform.position);

            // 如果距離足夠近，減速；否則加速
            float targetSpeed = (distance < decelerationDistance) ? Mathf.Lerp(0, maxSpeed, distance / decelerationDistance) : maxSpeed;
            animator.SetFloat("speed", targetSpeed);

            // 按下shift 使速度3倍 (體力充足) 同slidercontroller的增減規則
            if (Input.GetKey(KeyCode.LeftShift) && ValueController.Instance.GetEnergy() > 0.01f)
            {
                targetSpeed *= sprintMultiplier;
                
                AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);
                if(stateInfo.IsName("Player_idle") || stateInfo.IsName("Player_swimming"))
                    animator.SetTrigger("rush");
            }
            else 
            {
                animator.SetTrigger("stopRush");
            }
            // 計算目標速度並應用加速度
            Vector2 targetVelocity = direction * targetSpeed;
            rb.velocity = Vector2.MoveTowards(rb.velocity, targetVelocity, acceleration * Time.deltaTime);
        }
        else{
            animator.SetFloat("speed", 0);
        }
    }
}
