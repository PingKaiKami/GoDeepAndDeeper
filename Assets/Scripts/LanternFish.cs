using System.Collections;
using UnityEngine;

public class LanternFish : MonoBehaviour
{
    [Header("Movement Parameters")]
    public bool canMove = false; // 是否能夠移動
    public float minSpeed = 1f; // 最小速度
    public float maxSpeed = 5f; // 最大速度
    public float acceleration = 1f; // 加速度
    public float detectionRange = 4f; // 偵測範圍
    public float attackRange = 2f; // 攻擊範圍
    public float escapeRange = 5f; // 逃跑範圍

    [Header("Dash Parameters")]
    public float dashDuration = 0.25f; // 衝刺完成時間（秒）
    public float dashMultiplier = 2f; // 衝刺距離的倍數

    [Header("Time Parameters")]
    public float prepareTime = 2f; // 準備時間（秒）
    public float restTime = 4f; // 休息時間（秒）

    public Transform player;
    private Animator animator;

    private bool isChasing = false;
    private bool isDashing = false;
    private bool isPreparing = false;
    private bool isResting = false;

    private float speed;
    private Vector2 direction = Vector2.right;

    private Vector3 dashStartPosition;
    private Vector3 dashTargetPosition;
    private float dashTimeElapsed = 0f;
    private AudioManager audioManager;

    void Start()
    {
        //player = GameObject.FindGameObjectWithTag("Player").transform;
        speed = minSpeed;
        animator = GetComponent<Animator>();
        audioManager = GameObject.FindGameObjectWithTag("AudioManager").GetComponent<AudioManager>();
    }

    void FixedUpdate()
    {
        if (canMove)
        {
            HandleMovement();
        }
    }

    private void HandleMovement()
    {
        // 偵測距離內開始追蹤
        if (Vector2.Distance((Vector2)transform.position, (Vector2)player.position) < detectionRange && !isChasing && !isDashing && !isPreparing && !isResting)
        {
            isChasing = true;
        }

        // 在攻擊範圍內開始準備衝刺
        if (Vector2.Distance((Vector2)transform.position, (Vector2)player.position) < attackRange && isChasing && !isDashing && !isPreparing && !isResting)
        {
            StartCoroutine(PrepareToDash());
        }

        // 超過逃跑範圍停止追蹤
        if (Vector2.Distance((Vector2)transform.position, (Vector2)player.position) > escapeRange && !isPreparing)
        {
            isChasing = false;
            speed = minSpeed;
        }

        // 移動
        Move();
    }

    private void Move()
    {
        if (isChasing)
        {
            Vector2 direction = ((Vector2)player.position - (Vector2)transform.position).normalized;
            transform.position += (Vector3)(direction * speed * Time.deltaTime);
            speed += acceleration * Time.deltaTime;
            if (speed > maxSpeed) speed = maxSpeed;
            Face(direction);
        }
        else if (isDashing)
        {
            Dash();
        }
        else if (isPreparing || isResting)
        {
            // 在準備或休息狀態下不進行移動
            return;
        }
        else
        {
            transform.Translate((Vector3)(direction * speed * Time.deltaTime));
            Face(direction);
        }
    }

    private void Dash()
    {
        dashTimeElapsed += Time.deltaTime;

        // 計算每幀的移動量
        transform.position = Vector2.Lerp(dashStartPosition, dashTargetPosition, (dashTimeElapsed)  / dashDuration);
        Face(dashTargetPosition);

        // 檢查是否已到達衝刺終點
        if (dashTimeElapsed >= dashDuration)
        {
            StartCoroutine(RestAfterAttack());
        }
    }

    private void Face(Vector2 direction)
    {
        if (direction.x > 0)
        {
            transform.localScale = new Vector3(1, 1, 1); // 面向右
        }
        else
        {
            transform.localScale = new Vector3(-1, 1, 1); // 面向左
        }
    }

    private IEnumerator PrepareToDash()
    {
        isPreparing = true;
        isChasing = false;

        // 暫時註解掉動畫部分
        // animator.SetTrigger("Prepare");
        yield return new WaitForSeconds(prepareTime);

        // 設置為衝刺狀態
        Face(direction); // debug
        isPreparing = false;
        isDashing = true;
        dashStartPosition = transform.position;
        dashTargetPosition = player.position;

        dashTimeElapsed = 0f;

        // 暫時註解掉動畫部分
        // animator.SetTrigger("Dash");
    }

    private IEnumerator RestAfterAttack()
    {
        isResting = true;
        isDashing = false;

        // 暫時註解掉動畫部分
        // animator.SetTrigger("Rest");
        yield return new WaitForSeconds(restTime);

        // 重置狀態
        isResting = false;
        isChasing = false;
        speed = minSpeed;

        // 暫時註解掉動畫部分
        // animator.SetTrigger("Idle");
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            ValueController.Instance.DecreaseOxygen(0.2f);
            StartCoroutine(RestAfterAttack());
            audioManager.Play(21, audioManager.sdharm);
        }
        else
        {
            direction *= -1; // 碰到其他物體時反向
            Face(direction);
        }
    }
}
