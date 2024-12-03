using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
/*
 * BUGS
 * ���k���ʸI�����w�|�ϦV
 * 
 * 
 */
public class LanternFish : MonoBehaviour
{
    public Transform player;       // ���a����m
    
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
        if (Vector2.Distance(transform.position, player.position) < DETECTION_RANGE && !isChasing && !isPreparing)//�b���a�����d��
        {
            StartCoroutine(PrepareToChase());
        }
        if (Vector2.Distance(transform.position, player.position) >= ESCAPE_RANGE)
        {
            isChasing = false;
            speed = MIN_SPEED;
            //����@��ʵe
        }
        Move();
    }

    IEnumerator  PrepareToChase()
    {
        isPreparing = true; // �]�w���ǳƪ��A�A�קK����Ĳ�o

        // ����ǳưʵe
        // animator.SetTrigger("Prepare");
        yield return new WaitForSeconds(2.0f);

        // ������l���Ҧ�
        isChasing = true;
        isPreparing = false;

        // ����l���ʵe
        // animator.SetTrigger("Chase");
    }

    IEnumerator RestAfterAttack()
    {
        // ������𮧪��A
        isResting = true;
        isChasing = false;

        // ����𮧰ʵe
        // animator.SetTrigger("Rest");

        // ����5��
        yield return new WaitForSeconds(5.0f);

        // ��_�l�����A
        isResting = false;
        isChasing = false;
        speed = MIN_SPEED;

        // �����^�@��ʵe
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
            transform.localScale = new Vector3(1, 1, 1); // ���V�k
        }
        else
        {
            transform.localScale = new Vector3(-1, 1, 1); // ���V��
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
            transform.localScale = new Vector3(1, 1, 1); // ���V�k
        }
        else
        {
            transform.localScale = new Vector3(-1, 1, 1); // ���V��
        }
    }
}
