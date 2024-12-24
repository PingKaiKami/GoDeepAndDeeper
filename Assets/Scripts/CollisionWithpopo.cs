using UnityEngine;

public class CollisionWithpopo : MonoBehaviour
{
    private void Start()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Destroy(gameObject);
            ValueController.Instance.IncreaseOxygen(0.10f);
        }
    }
}
