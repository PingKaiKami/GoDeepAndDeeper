using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionWithBubbles : MonoBehaviour
{
    private void Start()
    {

    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            // 使用單例模式，操作氧氣條
            if (OxygenController.Instance != null)
            {
                OxygenController.Instance.IncreaseOxygen(0.10f * Time.deltaTime);  // 增加氧氣條值
                HeartSliderController.Instance.IncreaseHeartRate(0.2f * Time.deltaTime);
            }
        }
    }
}
