using UnityEngine;

public class Camera_Move : MonoBehaviour
{
    public Transform player;            // 玩家物件
    public float smoothSpeed = 0.125f;  // 鏡頭跟隨的平滑度
    public Vector3 offset;              // 鏡頭與玩家之間的偏移量

    private float shakeDuration;        // 畫面震動持續時間
    private float shakeMagnitude;       // 震動強度
    public float dampingSpeed = 1.0f;   // 震動衰減速度

    private Vector3 initialPosition;    // 相機的初始位置
    private float currentShakeTime;     // 當前震動剩餘時間

    // 邊界設定
    public float minX, maxX, minY, maxY;  // 限制相機移動範圍的最小和最大X、Y值

    void FixedUpdate()
    {
        // 垂直與水平方向跟隨玩家的邏輯
        Vector3 desiredPosition = new Vector3(player.position.x + offset.x, player.position.y + offset.y, transform.position.z);
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);

        // 如果有震動效果，應用 X 軸和 Y 軸的震動偏移
        if (shakeDuration > 0)
        {
            float shakeOffsetX = Random.Range(-shakeMagnitude, shakeMagnitude); // 隨機生成 X 軸震動
            float shakeOffsetY = Random.Range(-shakeMagnitude, shakeMagnitude); // 隨機生成 Y 軸震動

            smoothedPosition.x += shakeOffsetX;
            smoothedPosition.y += shakeOffsetY;

            // 減少震動時間
            shakeDuration -= Time.deltaTime * dampingSpeed;
        }
        else
        {
            shakeDuration = 0;
        }

        // 限制相機位置在設定範圍內
        smoothedPosition.x = Mathf.Clamp(smoothedPosition.x, minX, maxX);
        smoothedPosition.y = Mathf.Clamp(smoothedPosition.y, minY, maxY);

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
