using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class SinMovement : MonoBehaviour
{
    public GameObject[] objects; // 需要移動的物件
    public float amplitude = 6f; // 波幅（橫向擺動幅度）
    public float waveLength = 2f; // 波長
    public float speed = 1f; // 波的移動速度
    public float yOffset = 3f; // 垂直方向間距
    public bool isLeft = true;

    private float time;

    void Start()
    {
        time = 0f;
    }

    void Update()
    {
        time += Time.deltaTime * speed;

        for (int i = 0; i < objects.Length; i++)
        {
            if (objects[i] != null)
            {
                // 計算倒過來的正弦波位置
                float yPosition = -182 - i * yOffset; // 垂直固定間距
                float xPosition = amplitude * Mathf.Sin((2 * Mathf.PI / waveLength) * yPosition + time); // 橫向擺動
                if(isLeft)
                    xPosition -= 5;
                else
                    xPosition += 5;
                Vector3 newPosition = new Vector3(xPosition, yPosition, objects[i].transform.position.z);

                // 更新物件位置
                objects[i].transform.position = newPosition;
            }
        }
    }
}
