using UnityEngine;

public class Camera_VeticalMove : MonoBehaviour
{
    public Transform player;            // 玩家物件
    public float smoothSpeed = 0.125f;  // 鏡頭跟隨的平滑度
    public Vector3 offset;              // 鏡頭與玩家之間的偏移量

    private float shakeDuration;    // 畫面震動持續時間
    private float shakeMagnitude; // 震動強度
    public float dampingSpeed = 1.0f;   // 震動衰減速度

    private Vector3 initialPosition;    // 相機的初始位置
    private float currentShakeTime;     // 當前震動剩餘時間

    void FixedUpdate()
    {
        // 垂直跟隨玩家的邏輯
        Vector3 desiredPosition = new Vector3(transform.position.x, player.position.y + offset.y, transform.position.z);
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);

        // 如果有震動效果，應用僅限 Y 軸的震動偏移
        if (shakeDuration > 0)
        {
            float shakeOffsetY = Random.Range(-shakeMagnitude, shakeMagnitude); // 隨機生成 Y 軸震動
            smoothedPosition.y += shakeOffsetY;

            // 減少震動時間
            shakeDuration -= Time.deltaTime * dampingSpeed;
        }
        else
        {
            shakeDuration = 0;
        }

        // 更新相機的位置
        transform.position = smoothedPosition;
    }

    /// <summary>
    /// 觸發震動
    /// </summary>
    /// <param name="duration">震動持續時間</param>
    /// <param name="magnitude">震動強度</param>
    public void TriggerShake(float duration, float magnitude)
    {
        shakeDuration = duration;
        shakeMagnitude = magnitude;
    }
}
