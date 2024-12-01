using UnityEngine;

public class CollisionWithpopo : MonoBehaviour
{
    private void Start()
    {
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Destroy(gameObject);

            // 使用單例模式，操作氧氣條
            if (OxygenController.Instance != null)
            {
                OxygenController.Instance.IncreaseOxygen(0.10f);  // 增加氧氣條值
            }
        }
    }
}
