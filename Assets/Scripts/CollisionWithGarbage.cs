using UnityEngine;

public class CollisionWithGarbage : MonoBehaviour
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
                OxygenController.Instance.DecreaseMaxOxygen(0.25f);
            }
            if(EnergySliderController.Instance_Energy != null)
            {
                EnergySliderController.Instance_Energy.DecreaseEnergy(0.25f);
            }
        }
    }
}
