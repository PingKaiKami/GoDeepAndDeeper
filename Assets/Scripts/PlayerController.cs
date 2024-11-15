using UnityEditor.Rendering;
using UnityEditor.VersionControl;
using UnityEngine;
using UnityEngine.UI;

// 修復
// 修復體力歸零 心率不增加
// 心率為0會消耗氧
// 單按shift消耗氧氣
public class PlayerController : MonoBehaviour
{
    public float acceleration = 10f;  // 加速度
    public float maxSpeed = 10f;     // 最大速度
    public float decelerationDistance = 1f;  // 開始減速的距離
    public float sprintMultiplier = 3f;   // 衝刺時的速度
    public float energy = 1f; // 體力值
    protected private Rigidbody2D rb;

    private void Update()
    {
        Move();
    }

    protected private void Move()
    {
        // 檢測左鍵是否按下
        if (Input.GetMouseButton(0))
        {
            // 獲取鼠標的位置並將其轉換為世界座標
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePosition.z = 0f;  // 設定 z 軸，確保角色只在 2D 平面移動

            // 計算角色與鼠標之間的距離
            Vector2 direction = (mousePosition - transform.position).normalized;
            float distance = Vector2.Distance(mousePosition, transform.position);

            // 如果距離足夠近，減速；否則加速
            float targetSpeed = (distance < decelerationDistance) ? Mathf.Lerp(0, maxSpeed, distance / decelerationDistance) : maxSpeed;

            // 按下shift 使速度3倍 (體力充足) 同slidercontroller的增減規則
            if (Input.GetKey(KeyCode.LeftShift) && energy > 0)
            {
                targetSpeed *= sprintMultiplier;
                
                energy -= 0.1f * Time.deltaTime;
            }
            else 
            {
                energy += 0.05f *Time.deltaTime;
            }

            // 計算目標速度並應用加速度
            Vector2 targetVelocity = direction * targetSpeed;
            rb.velocity = Vector2.MoveTowards(rb.velocity, targetVelocity, acceleration * Time.deltaTime);

        }
    }
}
