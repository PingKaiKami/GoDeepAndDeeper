using UnityEngine;

public class FishMovement : MonoBehaviour
{
    public float speed = 0.65f; // 移動速度
    public float range = 5f; // 移動範圍（左右的最大距離）
    private Vector3 startPosition; // 初始位置
    private bool movingRight = true; // 當前移動方向

    void Start()
    {
        // 記錄初始位置
        startPosition = transform.position;
    }

    void Update()
    {
        // 計算當前方向的移動
        float moveDirection = movingRight ? 1 : -1;
        transform.position += new Vector3(moveDirection * speed * Time.deltaTime, 0, 0);

        // 檢查是否超出移動範圍
        if (movingRight && transform.position.x > startPosition.x + range)
        {
            TurnAround(false); // 向左轉
        }
        else if (!movingRight && transform.position.x < startPosition.x - range)
        {
            TurnAround(true); // 向右轉
        }
    }

    // 轉向的方法
    private void TurnAround(bool toRight)
    {
        movingRight = toRight;

        // 翻轉圖片方向
        Vector3 localScale = transform.localScale;
        localScale.x = Mathf.Abs(localScale.x) * (toRight ? 1 : -1);
        transform.localScale = localScale;
    }
}