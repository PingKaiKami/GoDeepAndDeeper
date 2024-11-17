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
            if (OxygenSliderController.Instance != null)
            {
                OxygenSliderController.Instance.IncreaseOxygen(0.25f);  // 增加氧氣條值
            }
        }
    }
}