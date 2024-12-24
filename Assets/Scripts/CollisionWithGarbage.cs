using UnityEngine;

public class CollisionWithGarbage : MonoBehaviour
{
    private void Start()
    {

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Destroy(gameObject);
            ValueController.Instance.DecreaseMaxOxygen(0.25f);
            ValueController.Instance.DecreaseEnergy(0.25f);
        }
    }
}
