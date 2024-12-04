using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
/*
 * BUGS
 * 碰到道具會轉向
 * 
 */
public class LanternFish : MonoBehaviour
{
    [SerializeField] private bool canMove = false;
    private Transform player;
    
    private Animator animator;

    private bool isChasing = false;
    private bool isDashing = false;
    private bool isPreparing = false;
    private bool isResting = false;

    private const float MIN_SPEED = 1f;
    private const float MAX_SPEED = 5f;
    private const float ACCLERATION = 1f;
    private const float DETECTION_RANGE = 4f;
    private const float ATTACK_RANGE = 2f;
    private const float ESCAPE_RANGE = 5f;
    private float speed;

    private Vector2 direction = Vector2.right;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        speed = MIN_SPEED;
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if(canMove){
            if (Vector2.Distance(transform.position, player.position) < DETECTION_RANGE && !isChasing && !isPreparing && !isResting)
            {
                isChasing = true;
            }
            if (Vector2.Distance(transform.position, player.position) < ATTACK_RANGE && isChasing && !isPreparing && !isResting)
            {
                StartCoroutine(PrepareToDash());
            }
            if (Vector2.Distance(transform.position, player.position) > ESCAPE_RANGE && !isPreparing)
            {
                isChasing = false;
                speed = MIN_SPEED;
            }
            Move();
        }        
    }

    IEnumerator  PrepareToDash()
    {
        isPreparing = true; // set
        isChasing = false;
        // play animation
        // animator.SetTrigger("Prepare");
        yield return new WaitForSeconds(2.0f);

        // set
        isPreparing = false;
        isDashing = true;

        // play animation
        // animator.SetTrigger("Chase");
    }

    IEnumerator RestAfterAttack()
    {
        // set rest
        isResting = true;
        isDashing = false;

        // play animation
        // animator.SetTrigger("Rest");

        // resting
        yield return new WaitForSeconds(4.0f);

        // reset to default
        isResting = false;
        isChasing = false;
        speed = MIN_SPEED;

        // Play animation
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
        else if (isDashing)
        {
            // 衝刺邏輯
            Vector3 dashDirection = (player.position - transform.position).normalized;
            transform.position += dashDirection * MAX_SPEED * 2 * Time.deltaTime; // 衝刺速度為最大速度的兩倍
            Face(dashDirection); // 確保魚面向玩家
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
            transform.localScale = new Vector3(1, 1, 1); // Face Right
        }
        else
        {
            transform.localScale = new Vector3(-1, 1, 1); // Face Left
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            OxygenController.Instance.DecreaseOxygen(0.4f);
            StartCoroutine(RestAfterAttack());
        }
        else
        {
            direction *= -1;
        }
        if (direction.x > 0)
        {
            transform.localScale = new Vector3(1, 1, 1); // Face Right
        }
        else
        {
            transform.localScale = new Vector3(-1, 1, 1); // Face Left
        }
    }
}
