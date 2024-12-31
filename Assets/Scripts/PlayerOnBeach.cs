using UnityEngine;
using UnityEngine.UI;  // 引用 UI 命名空間
using TMPro;


public class PlayerOnBeach : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float rightBoundary = 10f;
    public float leftBoundary = -10f;
    public Camera_Move cameraMove; // 將 Camera_Move 腳本拖放到這裡

    private Vector2 movement;
    private Animator animator;
    private SpriteRenderer spriteRenderer;
    private Rigidbody2D rb;

    public float cameraSmoothSpeed = 5f;
    private const float inputThreshold = 0.1f;

    public float pickupRange = 2f;
    public LayerMask itemLayer;
    private Transform nearbyItem;

    // UI 元件
    public GameObject mapUI;   // 這是顯示大地圖的 UI 物件
    public TextMeshProUGUI mapLabelText;  // 顯示文字的 UI 物件

    public TextMeshProUGUI smallMapLabelText;
    public Vector2 specialPosition;
    public float specialPositionRadius = 2f;

    public GameObject player1;
    public GameObject player2;
    public Canvas uiCanvas;
    private bool inSpecialArea = false;

    public ParticleSystem smokeEffect;
    private bool hasMapBeenPicked = false; // 追蹤藏寶圖是否被撿取
    public float boundValue;

    private bool isTransforming = false;
    private float transformDelay = 2f;  // 變身延遲時間
    private float transformTimer = 0f;
    public GameObject airWall;
    public AudioManager audioManager;
    private bool isPlayingFootstepSound = false;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();

        player1.SetActive(true);
        player2.SetActive(false);
        // 初始時隱藏大地圖
        mapUI.SetActive(false);
        // 初始時隱藏文字
        mapLabelText.gameObject.SetActive(false);

        if (smokeEffect != null && smokeEffect.isPlaying)
        {
            smokeEffect.Stop();
        }
        if (audioManager == null)
        {
            Debug.LogError("AudioManager is not assigned or found in the scene!");
        }
        audioManager.Play(0, "bgmMain", true);
    }

    void Update()
    {
        if (isTransforming)
        {
            // 計時並進行變身
            transformTimer += Time.deltaTime;
            if (transformTimer >= transformDelay)
            {
                CompleteTransformation();  // 變身完成
            }
            return;
        }
        float horizontal = Input.GetAxisRaw("Horizontal");
        movement = new Vector2(horizontal, 0);

        bool isMoving = movement.sqrMagnitude > inputThreshold * inputThreshold;

        animator.SetBool("Run", isMoving);

        if (horizontal > 0)
        {
            spriteRenderer.flipX = false;
        }
        else if (horizontal < 0)
        {
            spriteRenderer.flipX = true;
        }

        DetectNearbyItem();

        if (Input.GetKeyDown(KeyCode.E) && nearbyItem != null)
        {
            PickupItem(nearbyItem);
        }

        // 如果地圖已經顯示，按下空白鍵隱藏地圖
        if (mapUI.activeSelf && Input.GetKeyDown(KeyCode.Space))
        {
            CloseMap();
        }

        CheckSmokeEffect();

        CheckSpecialPosition();

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
            if (!isPlayingFootstepSound)
            {
                isPlayingFootstepSound = true;
                audioManager.Play(1, "sdFootBeach", true); // 讓腳步聲循環播放
            }
        }
        else
        {
            // 停止腳步聲
            if (isPlayingFootstepSound)
            {
                isPlayingFootstepSound = false;
                audioManager.Stop(1);
            }
        }
    }

    void DetectNearbyItem()
    {
        Collider2D[] items = Physics2D.OverlapCircleAll(transform.position, pickupRange, itemLayer);

        if (items.Length > 0)
        {
            nearbyItem = items[0].transform;
            ShowPickText();
        }
        else
        {
            nearbyItem = null;
            smallMapLabelText.gameObject.SetActive(false);
        }
    }

    void PickupItem(Transform item)
    {
        Destroy(item.gameObject);  // 撿起物品
        hasMapBeenPicked = true;
        // 撿起藏寶圖後顯示大地圖
        if (airWall != null)
        {
            airWall.SetActive(false);
        }
        ShowMap();
    }
    void ShowPickText()
    {
        smallMapLabelText.gameObject.SetActive(true);
        smallMapLabelText.text = "Press E to pick up";
    }
    void ShowMap()
    {
        // 顯示大地圖 UI
        mapUI.SetActive(true);

        // 顯示文字
        mapLabelText.gameObject.SetActive(true);
        mapLabelText.text = "Press Space to close Map";
        audioManager.Play(2,"sdMap", false);
    }

    void CloseMap()
    {
        // 隱藏大地圖 UI
        mapUI.SetActive(false);

        // 隱藏文字
        mapLabelText.gameObject.SetActive(false);
        audioManager.Play(2,"sdMap", false);
    }

    void CheckSpecialPosition()
    {
        float distance = Vector2.Distance(transform.position, specialPosition);

        // 進入特殊區域
        if (!inSpecialArea && distance <= specialPositionRadius)
        {
            inSpecialArea = true;
            StartTransformation();  // 開始變身
        }
        // 離開特殊區域
        else if (inSpecialArea && distance > specialPositionRadius)
        {
            inSpecialArea = false;
            StopSmokeEffect();
        }
    }

    void CheckSmokeEffect()
    {
        if (hasMapBeenPicked && transform.position.x <= boundValue)
        {
            // 當已經撿起藏寶圖，並且玩家的 x 位置大於或等於特殊位置 x 時，釋放煙霧
            PlaySmokeEffect();
        }
    }
    void StartTransformation()
    {
        isTransforming = true;
        Debug.Log(isTransforming);
        transformTimer = 0f;  // 重置計時器
        PlaySmokeEffect();  // 播放煙霧效果
        audioManager.Stop(1);
    }

    void CompleteTransformation()
    {
        // 完成變身
        SwitchToPlayer2();
        StopSmokeEffect();
        isTransforming = false;  // 允許角色移動
    }

    void PlaySmokeEffect()
    {
        if (smokeEffect != null && !smokeEffect.isPlaying)
        {
            smokeEffect.Play();
        }
        audioManager.Play(3, "smoke", false);
    }

    // 停止煙霧效果
    void StopSmokeEffect()
    {
        if (smokeEffect != null && smokeEffect.isPlaying)
        {
            smokeEffect.Stop();
        }
    }

    // 切換到第二個角色
    void SwitchToPlayer2()
    {
        player1.SetActive(false);
        player2.SetActive(true);
        /*// 直接修改相機的公開變數
        cameraMove.player = player2.transform;
        cameraMove.minX = -7.5f;
        cameraMove.maxX = 7.5f;
        cameraMove.minY = -2000;
        cameraMove.maxY = 1000;*/
        //uiCanvas.enabled = true;
        audioManager.Play(4, "sdIntoWater", false);
    }

    // 切換回第一個角色
    /*void SwitchToPlayer1()
    {
        player1.SetActive(true);
        player2.SetActive(false);
    }*/

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, pickupRange);

        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(specialPosition, specialPositionRadius);  // 顯示特殊區域範圍

        if (nearbyItem != null)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawLine(transform.position, nearbyItem.position);
        }
    }
}
