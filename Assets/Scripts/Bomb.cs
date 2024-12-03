using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Bomb : MonoBehaviour
{
    public float explosionForce = 10f; // 爆炸的力道
    public float shakeDuration = 1.0f;
    public float shakeMagnitude = 0.2f;

    private int movement;          // 移動方式：1=左右移動, 2=垂直掉落, 3=原地旋轉
    public float speed = 2.0f;        // 移動速度
    public float rotationSpeed = 100f; // 原地旋轉速度
    private Vector2 direction = Vector2.right; // 初始方向（左右移動）

    public Rigidbody2D rb;           // 2D 剛體

    private void Start()
    {
        movement = Random.Range(1,4);//設定移動模式
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        switch (movement){
            case 1:
                HorizontalMove();
                break;
            case 2:
                Drop();
                break;
            case 3:
                Rotate();
                break;
        }
    }

    private void HorizontalMove()
    {
        // 移動炸彈左右來回
        transform.Translate(direction * speed * Time.deltaTime);
    }

    private void Drop(){
        rb.gravityScale = 1;    // 啟用重力效果
    }

    private void Rotate()
    {
        transform.Rotate(Vector3.forward * rotationSpeed * Time.deltaTime);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Wall"))
        {
            direction *= -1; // 反向移動
        }
        if (collision.gameObject.CompareTag("Player")) // 確認碰到的是玩家
        {
            // 計算擊退方向
            Rigidbody2D playerRb = collision.gameObject.GetComponent<Rigidbody2D>();

            Vector2 knockbackDirection = (collision.transform.position - transform.position).normalized;
            playerRb.AddForce(knockbackDirection * explosionForce, ForceMode2D.Impulse);

            // 播放爆炸動畫

            Camera_VeticalMove cameraScript = Camera.main.GetComponent<Camera_VeticalMove>();
            if (cameraScript != null)
            {
                cameraScript.TriggerShake(1.5f, 0.2f);
            }


            // 銷毀炸彈
            Destroy(gameObject);
        } 
    }
}
