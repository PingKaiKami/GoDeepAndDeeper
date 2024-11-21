using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
/*
 * BUGS
 * 左右移動碰撞到氣泡會反向
 * 
 * 
 */
public class LanternFish : MonoBehaviour
{
    public Transform player;       // 玩家的位置
    
    private Animator animator;

    private bool isChasing = false;
    private bool isPreparing = false;
    private bool isResting = false;

    private const float MIN_SPEED = 1f;
    private const float MAX_SPEED = 5f;
    private const float ACCLERATION = 1f;
    private const float DETECTION_RANGE = 4f;
    private const float ESCAPE_RANGE = 5f;
    private float speed;

    private Vector2 direction = Vector2.right;

    void Start()
    {
        speed = MIN_SPEED;
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector2.Distance(transform.position, player.position) < DETECTION_RANGE && !isChasing && !isPreparing)//在玩家視野範圍內
        {
            StartCoroutine(PrepareToChase());
        }
        if (Vector2.Distance(transform.position, player.position) >= ESCAPE_RANGE)
        {
            isChasing = false;
            speed = MIN_SPEED;
            //播放一般動畫
        }
        Move();
    }

    IEnumerator  PrepareToChase()
    {
        isPreparing = true; // 設定為準備狀態，避免重複觸發

        // 播放準備動畫
        // animator.SetTrigger("Prepare");
        yield return new WaitForSeconds(2.0f);

        // 切換到追擊模式
        isChasing = true;
        isPreparing = false;

        // 播放追擊動畫
        // animator.SetTrigger("Chase");
    }

    IEnumerator RestAfterAttack()
    {
        // 切換到休息狀態
        isResting = true;
        isChasing = false;

        // 播放休息動畫
        // animator.SetTrigger("Rest");

        // 等待5秒
        yield return new WaitForSeconds(5.0f);

        // 恢復追擊狀態
        isResting = false;
        isChasing = false;
        speed = MIN_SPEED;

        // 切換回一般動畫
        // animator.SetTrigger("");
    }


    protected private void Move()
    {
        if (isChasing)
        {
            Vector3 direction = (player.position - transform.position).normalized;
            transform.position += direction * speed * Time.deltaTime;
            speed += ACCLERATION * Time.deltaTime;
            if (speed > MAX_SPEED) speed = MAX_SPEED;
            Face(direction);
        }
        else if (isPreparing || isResting)
        {
            //do nothing
        }
        else
        {
            transform.Translate(direction * speed * Time.deltaTime);
            Face(direction);
        }
    }

    void Face(Vector2 direction)
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

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            OxygenSliderController.Instance.DecreaseOxygen(0.4f);
            StartCoroutine(RestAfterAttack());
        }
        else
        {
            direction *= -1;
        }
        if (direction.x > 0)
        {
            transform.localScale = new Vector3(1, 1, 1); // 面向右
        }
        else
        {
            transform.localScale = new Vector3(-1, 1, 1); // 面向左
        }
    }
}
