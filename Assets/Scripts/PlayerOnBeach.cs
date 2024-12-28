using UnityEngine;
using UnityEngine.UI;  // 引用 UI 命名空間
using TMPro;


public class PlayerOnBeach : MonoBehaviour
{
    public float moveSpeed = 5f;         
    public float topBoundary = 5f;      
    public float bottomBoundary = -5f;  
    public float rightBoundary = 10f;   
    public float leftBoundary = -10f;  

    private Vector2 movement;           
    private Animator animator;          
    private SpriteRenderer spriteRenderer; 
    public Transform cameraTransform;  
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
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();

        if (cameraTransform == null)
        {
            cameraTransform = Camera.main.transform;
        }

        // 初始時隱藏大地圖
        mapUI.SetActive(false);
        // 初始時隱藏文字
        mapLabelText.gameObject.SetActive(false);
    }

    void Update()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        movement = new Vector2(horizontal, vertical);

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
    }

    void FixedUpdate()
    {
        Vector2 targetPosition = (Vector2)transform.position + movement.normalized * moveSpeed * Time.fixedDeltaTime;

        targetPosition.y = Mathf.Clamp(targetPosition.y, bottomBoundary, topBoundary);
        targetPosition.x = Mathf.Clamp(targetPosition.x, leftBoundary, rightBoundary);

        if (rb != null)
        {
            rb.MovePosition(targetPosition);
        }

        if (cameraTransform != null)
        {
            Vector3 targetCameraPosition = new Vector3(transform.position.x, cameraTransform.position.y, cameraTransform.position.z);
            cameraTransform.position = Vector3.Lerp(cameraTransform.position, targetCameraPosition, Time.fixedDeltaTime * cameraSmoothSpeed);
        }
    }

    void DetectNearbyItem()
    {
        Collider2D[] items = Physics2D.OverlapCircleAll(transform.position, pickupRange, itemLayer);

        if (items.Length > 0)
        {
            nearbyItem = items[0].transform;
            ShowPcikText();
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
        ShowMap();
    }
    void ShowPcikText()
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
    }

    void CloseMap()
    {
        // 隱藏大地圖 UI
        mapUI.SetActive(false);

        // 隱藏文字
        mapLabelText.gameObject.SetActive(false);
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, pickupRange);

        if (nearbyItem != null)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawLine(transform.position, nearbyItem.position);
        }
    }
}
