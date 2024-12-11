using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    public float damage = 0.25f;
    public float heartRate = 0.4f;
    public float explosionForce = 10f; // 爆炸的力道
    public float shakeDuration = 0.5f;
    public float shakeMagnitude = 0.2f;
    public int movement = 1; // 移動方式：1=左右移動, 2=上下移動, 3=原地旋轉
    public int firstDir = 1; // 1=右 or 上 // 2=左 or 下
    public float changeDirTime = 1f;
    public float speed = 2.0f;           // 移動速度
    public float rotationSpeed = 100f;  // 原地旋轉速度
    private Vector2 direction;          // 移動方向
    private Rigidbody2D rb;
    private Collider2D collider;
    private bool isCal = false;
    public bool isUsingCoroutine = false;
    public GameObject explosion;
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        collider = GetComponent<CircleCollider2D>();

        // 根據移動模式初始化方向
        if (movement == 1){
            if(firstDir == 1)
                direction = Vector2.right;
            else
                direction = Vector2.left;
                
        }
        else if (movement == 2){
            if(firstDir == 1){
                direction = Vector2.up;
            }
            else{
                direction = Vector2.down;
            }
        }
    }

    private void Update()
    {
        switch (movement)
        {
            case 1:
                HorizontalMove();
                break;
            case 2:
                VerticalMove();
                break;
            case 3:
                Rotate();
                break;
        }
    }

    private void HorizontalMove()
    {
        // 左右來回移動
        transform.Translate(direction * speed * Time.deltaTime);
        if(!isCal && isUsingCoroutine){
            StartCoroutine(CalVector());
            isCal = true;
        }
    }
    private void VerticalMove()
    {
        // 上下來回移動
        transform.Translate(direction * speed * Time.deltaTime);
        if(!isCal && isUsingCoroutine){
            StartCoroutine(CalVector());
            isCal = true;
        }
    }

    IEnumerator CalVector(){
        yield return new WaitForSeconds(changeDirTime);
        direction *= -1;
        isCal = false;
    }

    private void Rotate()
    {
        // 原地旋轉
        transform.Rotate(Vector3.forward * rotationSpeed * Time.deltaTime);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            // 碰到玩家時執行爆炸效果
            Rigidbody2D playerRb = collision.gameObject.GetComponent<Rigidbody2D>();
            if (playerRb != null)
            {
                Vector2 knockbackDirection = (collision.transform.position - transform.position).normalized;
                playerRb.AddForce(knockbackDirection * explosionForce, ForceMode2D.Impulse);
            }

            // 觸發相機震動效果
            Camera_Move cameraScript = Camera.main.GetComponent<Camera_Move>();
            if (cameraScript != null)
            {
                cameraScript.TriggerShake(shakeDuration, shakeMagnitude);
            }

            // 爆炸動畫
            GameObject temp = Instantiate(explosion, transform.position, quaternion.identity);
            temp.transform.localScale = gameObject.transform.localScale;
            StartCoroutine(DestroyAnimation(temp));

            // 減少最大氧氣量
            if (OxygenController.Instance != null)
            {
                OxygenController.Instance.DecreaseMaxOxygen(damage);
            }

            // 增加心率
            if (HeartSliderController.Instance != null)
            {
                HeartSliderController.Instance.IncreaseHeartRate(heartRate); 
            }

        }
    }

    IEnumerator DestroyAnimation(GameObject temp){
        collider.enabled = false;
        Color color = gameObject.GetComponent<SpriteRenderer>().color;
        color.a = 0;
        gameObject.GetComponent<SpriteRenderer>().color = color;
        yield return new WaitForSeconds(1);
        Destroy(temp);
        Destroy(gameObject);
    }
}
