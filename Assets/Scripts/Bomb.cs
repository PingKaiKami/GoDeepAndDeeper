using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{

    public float damage = 0.25f;
    public float heartRate = 0.4f;
    public float explosionForce = 10f; // 爆炸的力道
    public float shakeDuration = 1.0f;
    public float shakeMagnitude = 0.2f;

    private int movement;                // 移動方式：1=左右移動, 2=上下移動, 3=原地旋轉
    public float speed = 2.0f;           // 移動速度
    public float rotationSpeed = 100f;  // 原地旋轉速度
    private Vector2 direction;          // 移動方向

    public Rigidbody2D rb;              // 2D 剛體

    private void Start()
    {
        movement = Random.Range(1, 4); // 隨機設定移動模式
        rb = GetComponent<Rigidbody2D>();

        // 根據移動模式初始化方向
        if (movement == 1)
            direction = Random.Range(0,2) == 1 ? Vector2.right : Vector2.left; // 左右移動
        else if (movement == 2)
            direction = Vector2.down; // 上下移動
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
    }

    private void VerticalMove()
    {
        // 上下來回移動
        transform.Translate(direction * speed * Time.deltaTime);
    }

    private void Rotate()
    {
        // 原地旋轉
        transform.Rotate(Vector3.forward * rotationSpeed * Time.deltaTime);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Wall"))
        {
            // 碰到牆壁時反向移動
            direction *= -1;
        }
        else if (collision.gameObject.CompareTag("Player"))
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

            // 銷毀炸彈
            Destroy(gameObject);
        }
    }
}
