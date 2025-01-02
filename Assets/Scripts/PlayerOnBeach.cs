using UnityEngine;
using UnityEngine.UI;  // 引用 UI 命名空間
using TMPro;
using System.Collections;


public class PlayerOnBeach : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float rightBoundary = 10f;
    public float leftBoundary = -10f;
    public Camera_Move cameraMove; // 將 Camera_Move 腳本拖放到這裡

    private Vector2 movement;
    private Vector2 previousMovement = Vector2.zero;
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
    public GameObject uiHeart;
    public GameObject uiDepthDisplay;
    private bool inSpecialArea = false;

    public ParticleSystem smokeEffect;
    public ParticleSystem bubbleEffect;
    private bool hasMapBeenClosed = false; // 追蹤藏寶圖是否被收起
    public GameObject airWall;
    private AudioManager audioManager;
    private bool isPlayingFootstepSound = false;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        audioManager = GameObject.FindGameObjectWithTag("AudioManager").GetComponent<AudioManager>();

        uiHeart.SetActive(false);
        uiDepthDisplay.SetActive(false);
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
        if(bubbleEffect != null && bubbleEffect.isPlaying){
            bubbleEffect.Stop();
        }
        if (audioManager == null)
        {
            Debug.LogError("AudioManager is not assigned or found in the scene!");
        }
    }

    void Update()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        movement = new Vector2(horizontal, 0);

        bool isMoving = movement.sqrMagnitude > inputThreshold * inputThreshold;

        animator.SetBool("Run", isMoving);

        if (horizontal < 0 || isJumping)
        {
            spriteRenderer.flipX = true;
        }
        else
        {
            spriteRenderer.flipX = false;
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

        CheckSpecialPosition();

    }
    void FixedUpdate()
    {
        // 避免小移動值導致無效移動
        if (movement.magnitude > 0.01f && !isJumping)
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
                audioManager.Play(10, audioManager.sdFootBeach, true); // 讓腳步聲循環播放
            }
        }
        else
        {
            // 停止腳步聲
            if (isPlayingFootstepSound && previousMovement.magnitude <= 0.01f)
            {
                isPlayingFootstepSound = false;
                audioManager.Stop(10);
            }
        }
        previousMovement = movement;
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
        audioManager.Play(11, audioManager.sdMap);
    }

    void CloseMap()
    {
        // 隱藏大地圖 UI
        mapUI.SetActive(false);
        hasMapBeenClosed = true;

        // 隱藏文字
        mapLabelText.gameObject.SetActive(false);
        audioManager.Play(16, audioManager.sdMapClose);
    }

    bool isTrigger = false;
    void CheckSpecialPosition()
    {
        float distance = Vector2.Distance(transform.position, specialPosition);

        // 進入特殊區域
        if (hasMapBeenClosed)
        {
            if (!inSpecialArea && distance <= specialPositionRadius)
            {
                inSpecialArea = true;
                if(!isTrigger){
                    StartCoroutine(JumpIntoWater());
                    isTrigger = true;
                }
            }
        }
    }
    private bool isJumping = false;
    IEnumerator JumpIntoWater(){
        isJumping = true;
        animator.SetBool("Jump", true);
        float time = 0f;
        while(true){
            if(time <= 1){
                transform.position += new Vector3(-0.02f,0.02f,0);
            }
            else if(time <= 2.5f){
                transform.position += new Vector3(-0.02f,-0.02f,0);
            }
            else{
                Transformation();
                break;
            }
            time += 0.01f;
            yield return new WaitForSeconds(0.01f);
        }
        yield return null;
    }

    //change character
    void Transformation()
    {
        PlayEffect();  // 播放煙霧效果
        SwitchToPlayer2();
        audioManager.Stop(10);
    }

    //smoke effect

    void PlayEffect()
    {
        if (!smokeEffect.isPlaying)
        {
            smokeEffect.Play();
        }
        if(!bubbleEffect.isPlaying){
            bubbleEffect.Play();
        }
        audioManager.Play(12, audioManager.smoke);
    }

    // 切換到第二個角色
    void SwitchToPlayer2()
    {
        player1.SetActive(false);
        player2.SetActive(true);
        // 直接修改相機的公開變數
        cameraMove.player = player2.transform;
        cameraMove.offset.y = 0;
        cameraMove.minX = -7.5f;
        cameraMove.maxX = 7.5f;
        cameraMove.minY = -1419.5f;
        cameraMove.maxY = 1000;
        uiHeart.SetActive(true);
        uiDepthDisplay.SetActive(true);
        audioManager.Play(13, audioManager.sdIntoWater);
    }

    // 切換回第一個角色
    /*void SwitchToPlayer1()
    {
        player1.SetActive(true);
        player2.SetActive(false);
    }*/

    //for debug
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
