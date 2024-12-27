using UnityEngine;

public class PlayerOnBeach : MonoBehaviour
{
    public float moveSpeed = 5f;       // 移動速度
    public float topBoundary = 5f;    // 上邊界
    public float bottomBoundary = -5f; // 下邊界
    public float rightBoundary = 10f; // 右邊界
    public float leftBoundary = -10f; // 左邊界（可自由延伸）

    private Vector2 movement;         // 儲存玩家的移動方向
    //private Animator animator;        // Animator 元件
    private SpriteRenderer spriteRenderer; // SpriteRenderer 元件
    public Transform cameraTransform; // 攝影機的 Transform 元件

    void Start()
    {
        // 獲取 Animator 和 SpriteRenderer 元件
        /*animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        if (animator == null)
        {
            Debug.LogWarning("Animator 未附加於物件！");
        }*/
        if (spriteRenderer == null)
        {
            Debug.LogWarning("SpriteRenderer 未附加於物件！");
        }

        // 找到主攝影機
        if (cameraTransform == null)
        {
            cameraTransform = Camera.main.transform;
        }
    }

    void Update()
    {
        // 接收玩家的輸入
        movement.x = Input.GetAxisRaw("Horizontal"); // A/D 或 左/右
        movement.y = Input.GetAxisRaw("Vertical");   // W/S 或 上/下

        // 控制角色面向
        if (spriteRenderer != null)
        {
            if (movement.x > 0) // 向右移動
            {
                spriteRenderer.flipX = false; // 恢復原本方向
            }
            else if (movement.x < 0) // 向左移動
            {
                spriteRenderer.flipX = true; // 水平翻轉
            }
        }
    }

    void FixedUpdate()
    {
        // 獲取當前位置
        Vector2 newPosition = (Vector2)transform.position + movement.normalized * moveSpeed * Time.fixedDeltaTime;

        // 檢查邊界
        newPosition.y = Mathf.Clamp(newPosition.y, bottomBoundary, topBoundary); // 限制上下
        newPosition.x = Mathf.Max(newPosition.x, leftBoundary);                 // 限制左，但不限制往左延伸
        newPosition.x = Mathf.Min(newPosition.x, rightBoundary);                // 限制右

        // 更新位置
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.MovePosition(newPosition);
        }

        // 更新攝影機位置
        if (cameraTransform != null)
        {
            Vector3 cameraPosition = cameraTransform.position;
            cameraPosition.x = Mathf.Max(transform.position.x, leftBoundary); // 攝影機向左延伸
            cameraTransform.position = new Vector3(cameraPosition.x, cameraPosition.y, cameraPosition.z);
        }
    }
}
