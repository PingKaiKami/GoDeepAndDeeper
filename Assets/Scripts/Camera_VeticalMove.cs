using UnityEngine;

public class Camera_VeticalMove : MonoBehaviour
{
    public Transform player;  // 玩家物件
    public float smoothSpeed = 0.125f;  // 鏡頭跟隨的平滑度
    public Vector3 offset;  // 鏡頭與玩家之間的偏移量

    void LateUpdate()
    {
        // 獲取玩家的位置，保持 x 和 z 軸不變，僅垂直跟隨 Y 軸
        Vector3 desiredPosition = new Vector3(transform.position.x, player.position.y + offset.y, transform.position.z);
        
        // 進行平滑跟隨
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);

        // 更新相機的位置
        transform.position = smoothedPosition;
    }
}
