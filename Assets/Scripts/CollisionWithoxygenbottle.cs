using UnityEngine;

public class CollisionWithoxygenbottle : MonoBehaviour
{
    private void Start()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Destroy(gameObject);
            ValueController.Instance.IncreaseOxygen(1.0f);
        }
    }
}
